using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Shotake
{
    interface ISnakeCaptureTarget2
    {
        Transform GetTransform();

        Transform GetBegPosition();

        Transform GetEndPosition();
    }

    // only position version
    // 20220816
    class SnakeCaptureSolver2
    {
        struct Capture
        {
            public Vector3 Position;
        }

        Capture[] m_captures;
        float m_captureGap;
        int m_lastIndex;

        public struct Initializer
        {
            public float CaptureGap;
            public float MaxDistance;
            public Vector3 HeadPosition;
        }

        public SnakeCaptureSolver2(float captureGap, float maxDistance, Vector3 headPosition)
        {
            Assert.IsTrue(captureGap > 0);
            Assert.IsTrue(maxDistance / captureGap > 2);

            m_captureGap = captureGap;
            m_lastIndex = 0;

            int maxCapSize = Mathf.CeilToInt(maxDistance / captureGap);
            m_captures = new Capture[maxCapSize];
            for (int i = 0; i < maxCapSize; ++i)
            {
                m_captures[i].Position = headPosition;
            }
        }

        public void CaptureHead(ISnakeCaptureTarget2 head)
        {
            var trans = head?.GetTransform();
            if (trans)
            {
                var position = trans.position;
                var lastCap = m_captures[m_lastIndex];
                var d = (position - lastCap.Position).magnitude;

                int i = (m_lastIndex + 1) % m_captures.Length;
                while (d >= m_captureGap)
                {
                    m_captures[i].Position = Vector3.Lerp(lastCap.Position, position, m_captureGap / d);
                    //Rotation = Quaternion.Lerp(lastCap.Rotation, trans.rotation, m_captureGap / d),
                    
                    d -= m_captureGap;
                    m_lastIndex = i;
                    i = (i + 1) % m_captures.Length;
                }
            }
        }

        public void Solve(ISnakeCaptureTarget2 head, IEnumerable<ISnakeCaptureTarget2> tails)
        {
            if (head != null && tails != null && head.GetTransform() is Transform headTrans)
            {
                float headOffset = (headTrans.position - m_captures[m_lastIndex].Position).magnitude;
                float widthSum = (headTrans.position - head.GetEndPosition().position).magnitude;
                ISnakeCaptureTarget2 prev = head;

                foreach (ISnakeCaptureTarget2 curr in tails)
                {
                    if (curr.GetTransform() is Transform currTrans)
                    {
                        widthSum += (currTrans.position - curr.GetBegPosition().position).magnitude;
                        
                        int capIdx = (m_lastIndex - Mathf.CeilToInt(widthSum / m_captureGap) + m_captures.Length) % m_captures.Length;
                        int ncapIdx = (capIdx + 1) % m_captures.Length;
                        var newpos = Vector3.Lerp(m_captures[capIdx].Position, m_captures[ncapIdx].Position, headOffset/ m_captureGap);

                        // rot
                        var desireDirection = (prev.GetTransform().position + prev.GetEndPosition().position) / 2f - curr.GetEndPosition().position;
                        var desireRotation = Quaternion.LookRotation(desireDirection, Vector3.up);
                        currTrans.rotation = desireRotation;

                        // move
                        currTrans.position = newpos;

                        widthSum += (currTrans.position - curr.GetEndPosition().position).magnitude;
                        prev = curr;
                     }
                    
                }
            }
        }
        public void DrawDebug()
        {
            for (int i = 0; i < m_captures.Length - 1; ++i)
            {
                int beg = (m_lastIndex - i + m_captures.Length) % m_captures.Length;
                int end = (m_lastIndex - i - 1 + m_captures.Length) % m_captures.Length;
                Debug.DrawLine(m_captures[beg].Position, m_captures[end].Position);
            }
        }

        public void DrawDebugTails(ISnakeCaptureTarget2 head, IEnumerable<ISnakeCaptureTarget2> tails)
        {
            int i = 0;
            Debug.DrawLine(head.GetBegPosition().position, head.GetEndPosition().position, Color.green);

            foreach (ISnakeCaptureTarget2 target in tails)
            {
                i += 1;
                Debug.DrawLine(target.GetBegPosition().position, target.GetEndPosition().position, i % 2 == 0 ? Color.green : Color.yellow);
            }
        }
    }
}
