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
        public NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var player = PlayerController.Instance.GetPlayerObject();

            agent.SetDestination(player.transform.position);
        }
    }
}
