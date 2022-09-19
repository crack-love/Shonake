using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Shotake
{
    /// <summary>
    /// Player Actor
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    class SK_Player : MonoBehaviour
    {
        // has head, tail!

        public SnakeTail m_tailPrefab;
        public Transform m_raycastPosition;

        public NavMeshAgent NavMeshAgent => m_agent;

        public Vector3 RaycastPosition => m_raycastPosition.position;

        NavMeshAgent m_agent;

        private void Awake()
        {
            m_agent = GetComponent<NavMeshAgent>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other)
            {
                var feed = other.gameObject.GetComponent<FeedItem>();
                if (feed)
                {
                    var head = GetComponentInChildren<SnakeHead>();
                    var tail = Instantiate(m_tailPrefab);

                    head.AddTail(tail);
                    Destroy(feed.gameObject);
                }
            }
        }
    }
}
