using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

namespace Shotake
{
    class SnakeTail : SnakeComponent
    {
        LinkedListNode<SnakeCapture> m_captureNode;

        Vector3 m_lastPos;
        Vector3 m_lastRot;
        bool m_isLast;
        float m_magPassed;

        public bool HasCapture => m_captureNode != null;

        public LinkedListNode<SnakeCapture> CaptureNode => m_captureNode;


        private void Start()
        {
            m_lastPos = transform.position;
            m_lastRot = transform.eulerAngles;
        }

        public void SetLast(bool isLast)
        {
            m_isLast = true;
        }

        public void SetCurrentCaptureNode(LinkedListNode<SnakeCapture> node)
        {
            m_captureNode = node;
        }

        public void MoveNextCapture(float magnitude)
        {
            while (magnitude > 0 && m_captureNode != null)
            {
                SnakeCapture c = m_captureNode.Value;

                if (c.Position == transform.position)
                {
                    if (m_isLast)
                    {
                        c.Finished = true;
                    }

                    m_magPassed = 0;
                    m_lastPos = c.Position;
                    m_lastRot = c.Rotation;
                    m_captureNode = m_captureNode.Next;
                    continue;
                }

                // move
                Vector3 movement = c.Position - transform.position;
                Vector3 movementC = Vector3.ClampMagnitude(movement, magnitude);
                float actualMag = movementC.magnitude;
                magnitude -= actualMag;
                transform.position += movementC;
                
                // rot
                var magTotal = (m_lastPos - c.Position).magnitude;
                m_magPassed += actualMag;
                var actureAngle = Vector3.Lerp(m_lastRot, c.Rotation, m_magPassed / magTotal);
                //Debug.Log(m_lastRot + "/" + c.Rotation + "/" + m_magPassed + "/" + magTotal + "/" + actureAngle);
                transform.eulerAngles = actureAngle;
            }
        }

        public override float GetComponentWidth()
        {
            return 2f;
        }
    }
}
