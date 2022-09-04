using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shotake;
using UnityEngine;
using UnityEngine.AI;

namespace Assets
{
    /// <summary>
    /// ai, instance state, lifecycle
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    class Enemy : MonoBehaviour, IDamageTakable
    {
        public float m_hp = 10;
        public float m_speed = 5;
        public float m_rotSpeed = 120;
        public float m_pathFindingDelay = 1;

        NavMeshAgent m_agent;
        Rigidbody m_rigidbody;
        Coroutine m_pathfinding;

        private void Awake()
        {
            m_agent = GetComponent<NavMeshAgent>();
            m_agent.hideFlags = HideFlags.HideInInspector;
            m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.isKinematic = true;
            m_rigidbody.hideFlags = HideFlags.HideInInspector;
        }

        private void Start()
        {
            m_pathfinding = StartCoroutine(FindPath());
        }

        private void Update()
        {
            m_agent.speed = m_speed;
            m_agent.angularSpeed = m_rotSpeed;
        }

        IEnumerator FindPath()
        {
            while (gameObject)
            {
                if (m_agent)
                {
                    var p = GameManager.Instance.Player;
                    if (p)
                    {
                        m_agent.SetDestination(p.transform.position);
                    }
                }

                yield return new WaitForSeconds(m_pathFindingDelay);
            }
        }

        public void TakeDamage(GameObject src, GameObject instanter, float damage, int damageLayer)
        {
            m_hp = Mathf.Max(m_hp - damage, 0);
            if (m_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
