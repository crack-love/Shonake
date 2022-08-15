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
    class PTNavmeshEnemy : MonoBehaviour
    {
        public GameObject player;
        public NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (player)
            {
                agent.SetDestination(player.transform.position);
            }
        }
    }
}
