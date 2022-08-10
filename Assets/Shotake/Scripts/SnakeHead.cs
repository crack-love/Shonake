using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shotake
{
    class SnakeHead : SnakeBody
    {
        public GameObject tailPrefab;
        public List<SnakeTail> tails = new List<SnakeTail>();
        public Camera camera;

        void Awake()
        {
            m_lastPos = transform.position;
            camera.orthographicSize = 10 + tails.Count * .5f;
        }

        Vector3 m_lastPos;

        // capture position, rotation.
        // send to child
        // move child with delta

        private void Update()
        {
            float moveMag = (transform.position - m_lastPos).magnitude;
            m_lastPos = transform.position;

            if (moveMag > 0)
            {
                for (int i = 0; i < tails.Count; ++i)
                {
                    tails[i].QueueCapture(transform.position, transform.eulerAngles);
                    tails[i].Move(moveMag);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null) return;
            if (other.gameObject == false) return;

            var item = other.gameObject.GetComponent<FeedItem>();
            if (item != null)
            {
                var o = Instantiate(tailPrefab);
                if (tails.Count > 0)
                {
                    o.transform.position = tails[tails.Count - 1].SpawnPositionTransform.position;
                    o.transform.rotation = tails[tails.Count - 1].transform.rotation;
                    tails[tails.Count - 1].CopyCapture(o.GetComponent<SnakeTail>());
                }
                else
                {
                    o.transform.position = SpawnPositionTransform.position;
                    o.transform.rotation = transform.rotation;
                }
                tails.Add(o.GetComponent<SnakeTail>());

                Destroy(item.gameObject);

                camera.orthographicSize = 10 + tails.Count * .25f;
            }
        }
    }
}
