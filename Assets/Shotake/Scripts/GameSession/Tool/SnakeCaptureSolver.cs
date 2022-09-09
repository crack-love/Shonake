using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Shotake
{
    // only capture position version 20220816
    // capture head's position every frame
    // solve tails
    class SnakeCaptureSolver : MonoBehaviour
    {
        public float m_captureGap = 0.5f;
        public float m_maxDistance = 100;

        Vector3[] m_captures;
        int m_lastIndex;

        public void Initialize(Vector3 headPosition)
        {
            m_lastIndex = 0;

            int maxCapSize = Mathf.CeilToInt(m_maxDistance / m_captureGap);
            m_captures = new Vector3[maxCapSize];
            for (int i = 0; i < maxCapSize; ++i)
            {
                m_captures[i] = headPosition;
            }
        }

        public void CaptureHead(ISnakeCaptureTarget head)
        {
            var trans = head?.GetTransform();
            if (trans)
            {
                var position = trans.position;
                var lastCap = m_captures[m_lastIndex];
                var d = (position - lastCap).magnitude;

                int i = (m_lastIndex + 1) % m_captures.Length;
                while (d >= m_captureGap)
                {
                    m_captures[i] = Vector3.Lerp(lastCap, position, m_captureGap / d);
                    
                    d -= m_captureGap;
                    m_lastIndex = i;
                    i = (i + 1) % m_captures.Length;
                }
            }
        }

        public void Solve(ISnakeCaptureTarget head, IEnumerable<ISnakeCaptureTarget> tails)
        {
            if (head != null && tails != null && head.GetTransform() is Transform headTrans)
            {
                float headOffset = (headTrans.position - m_captures[m_lastIndex]).magnitude;
                float widthSum = (headTrans.position - head.GetEndPosition().position).magnitude - headOffset; //(headTrans.position - head.GetEndPosition().position).magnitude - headOffset;
                ISnakeCaptureTarget prev = head;

                // lerp(currcap, currcap+1, headoffset/gap)

                foreach (ISnakeCaptureTarget curr in tails)
                {
                    if (curr.GetTransform() is Transform currTrans)
                    {
                        widthSum += (currTrans.position - curr.GetBegPosition().position).magnitude;

                        float frag = m_captureGap - widthSum % m_captureGap;
                        int capIdx = (m_lastIndex - Mathf.CeilToInt(widthSum / m_captureGap) + m_captures.Length) % m_captures.Length;
                        int ncapIdx = (capIdx + 1) % m_captures.Length;
                        var newpos = Vector3.Lerp(m_captures[capIdx], m_captures[ncapIdx], frag / m_captureGap);

                        // rot
                        var desireDirection = (prev.GetTransform().position + prev.GetEndPosition().position) / 2f - curr.GetEndPosition().position;
                        var desireRotation = Quaternion.LookRotation(desireDirection, Vector3.up);
                        currTrans.rotation = desireRotation;

                        // move (rot first)
                        currTrans.position = newpos;

                        widthSum += (currTrans.position - curr.GetEndPosition().position).magnitude;
                        prev = curr;
                     }
                    
                }
            }
        }
        public void DrawDebugCaptures()
        {
            for (int i = 0; i < m_captures.Length - 1; ++i)
            {
                int beg = (m_lastIndex - i + m_captures.Length) % m_captures.Length;
                int end = (m_lastIndex - i - 1 + m_captures.Length) % m_captures.Length;
                Debug.DrawLine(m_captures[beg], m_captures[end]);
            }
        }

        public void DrawDebugTails(ISnakeCaptureTarget head, IEnumerable<ISnakeCaptureTarget> tails)
        {
            int i = 0;
            Debug.DrawLine(head.GetBegPosition().position, head.GetEndPosition().position, Color.green);

            foreach (ISnakeCaptureTarget target in tails)
            {
                i += 1;
                Debug.DrawLine(target.GetBegPosition().position, target.GetEndPosition().position, i % 2 == 0 ? Color.green : Color.yellow);
            }


        }
    }
}
