using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Shotake
{
    class SnakeTail : SnakeBody
    {
        struct Capture
        {
            public Vector3 Position;
            public Vector3 Euler;
        };

        Queue<Capture> q = new Queue<Capture>();

        private void Start()
        {
            lastPos = transform.position;
            lastRot = transform.eulerAngles;
        }

        public void QueueCapture(Vector3 position, Vector3 euler)
        {
            q.Enqueue(new Capture()
            {
                Position = position,
                Euler = euler,
            });
        }

        Vector3 lastRot;
        Vector3 lastPos;
        float captureMagTotal;
        float captureMagPassed;

        public void Move(float magnitute)
        {
            while (q.Count > 0 && magnitute > 0)
            {
                Capture c = q.Peek();
                if (c.Position == transform.position)
                {
                    lastPos = c.Position;
                    lastRot = c.Euler;
                    q.Dequeue();
                    continue;
                }

                // move
                Vector3 movement = c.Position - transform.position;
                Vector3 movementC = Vector3.ClampMagnitude(movement, magnitute);
                float actualMag = movementC.magnitude;
                magnitute -= actualMag;
                transform.position += movementC;

                // rot
                captureMagTotal = (lastPos - c.Position).magnitude;
                captureMagPassed += actualMag;
                transform.eulerAngles = Vector3.Lerp(lastRot, c.Euler, captureMagPassed / captureMagTotal);
            }
        }

        public void CopyCapture(SnakeTail dst)
        {
            dst.q = new Queue<Capture>(q);
        }
    }
}
