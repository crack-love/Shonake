using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets;
using UnityEngine;
using UnityEngine.AI;

namespace Shotake
{
    /// <summary>
    /// Player's body component has collider, render
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    class SK_Player : Player, IDamageTakable
    {
        public NavMeshAgent m_agent;
        public SK_PlayerState m_state;
        public float m_damagedInvincibleTime = 1f;
        public float m_remainInvincibleTime = 0f;

        public NavMeshAgent NavMeshAgent => m_agent;

        private void Update()
        {
            if (!m_state)
            {
                m_state = GameManager.Instance.PlayerState as SK_PlayerState;
            }

            var dt = TimeManager.Instance.DeltaTime;
            m_remainInvincibleTime = Mathf.Max(m_remainInvincibleTime - dt, 0);
        }

        public void TakeDamage(GameObject src, GameObject instanter, float damage, int damageLayer) // todo : add damagelayer to bump or enemy
        {
            if (m_remainInvincibleTime <= 0)
            {
                m_state.HP -= damage;
                m_remainInvincibleTime = m_damagedInvincibleTime;

                if (m_state.HP <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void Awake()
        {
            m_agent = GetComponent<NavMeshAgent>();
        }
    }
}
