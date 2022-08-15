using System.Collections.Generic;
using UnityCommon;
using UnityEngine;

namespace Shotake
{
    interface ISnakeCaptureTarget
    {
        Transform GetTransform();

        float GetComponentWidth();
    }

    class SnakeCaptureSolver
    {
        struct Capture
        {
            public Vector3 Position;
            public Quaternion Rotation;
        }

        readonly List<Capture> m_captures; // circular
        readonly float m_captureGap;
        int m_lastCaptureIndex = 0;

        /// <summary>
        /// maxComponentCount includes head
        /// </summary>
        public SnakeCaptureSolver(float captureGap, float maxComponentWidth, float maxComponentCount, ISnakeCaptureTarget head)
        {
            m_captures = new List<Capture>();
            m_captureGap = captureGap;
            m_lastCaptureIndex = 0;

            var maxCaptureSize = Mathf.CeilToInt(maxComponentCount * maxComponentWidth / captureGap);
            m_captures.SetSize(maxCaptureSize, new Capture()
            {
                Position = head.GetTransform().position,
                Rotation = head.GetTransform().rotation,
            });
        }

        public void CaptureCurrent(ISnakeCaptureTarget head)
        {
            var trans = head?.GetTransform();
            if (trans)
            {
                var position = trans.position;
                var lastCap = m_captures[m_lastCaptureIndex];
                var d = (position - lastCap.Position).magnitude;

                int i = (m_lastCaptureIndex + 1) % m_captures.Count;
                while (d >= m_captureGap)
                {
                    m_captures[i] = new Capture()
                    {
                        Position = Vector3.Lerp(lastCap.Position, position, m_captureGap / d),
                        Rotation = Quaternion.Lerp(lastCap.Rotation, trans.rotation, m_captureGap / d),
                    };

                    d -= m_captureGap;
                    m_lastCaptureIndex = i;
                    i = (i + 1) % m_captures.Count;
                }
            }
        }

        public void SolveCurrent(ISnakeCaptureTarget head, IList<ISnakeCaptureTarget> tails)
        {
            if (head != null && tails != null)
            {
                var headTrans = head.GetTransform();
                if (headTrans)
                {
                    var headDelta = (headTrans.position - m_captures[m_lastCaptureIndex].Position).magnitude / m_captureGap;
                    var widthSum = head.GetComponentWidth();

                    for (int i = 0; i < tails.Count; ++i)
                    {
                        int cidx = (int)(m_lastCaptureIndex - (widthSum / m_captureGap) + m_captures.Count) % m_captures.Count;
                        int ncidx = (cidx + 1) % m_captures.Count;
                        var trans = tails[i].GetTransform();
                        if (trans)
                        {
                            var newpos = Vector3.Lerp(m_captures[cidx].Position, m_captures[ncidx].Position, headDelta);
                            var newrot = Quaternion.Lerp(m_captures[cidx].Rotation, m_captures[ncidx].Rotation, headDelta);
                            trans.SetPositionAndRotation(newpos, newrot);
                        }

                        widthSum += tails[i].GetComponentWidth();
                    }
                }
            }
        }

        public void DrawDebug()
        {
            for (int i = 0; i < m_captures.Count - 1; ++i)
            {
                int beg = (m_lastCaptureIndex - i + m_captures.Count) % m_captures.Count;
                int end = (m_lastCaptureIndex - i - 1 + m_captures.Count) % m_captures.Count;
                Debug.DrawLine(m_captures[beg].Position, m_captures[end].Position);
            }
        }
    }

}
