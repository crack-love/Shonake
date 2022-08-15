using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Shotake
{
    [RequireComponent(typeof(NavMeshAgent))]
    class SK_Player : Player
    {
        NavMeshAgent m_agent;

        public NavMeshAgent NavMeshAgent => m_agent;

        private void Awake()
        {
            m_agent = GetComponent<NavMeshAgent>();
        }
    }
}
