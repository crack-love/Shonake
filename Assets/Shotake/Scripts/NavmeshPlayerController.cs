using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon;
using UnityEngine;
using UnityEngine.AI;

namespace Shotake
{
    [RequireComponent(typeof(NavMeshAgent))]
    class NavmeshPlayerController : PlayerController
    {
        //public Transform camHolder;
        public NavMeshAgent m_playerAgent;
        public float m_pathfindingDestinationOffset = 3;

        private void Update()
        {
            Vector2 axis2d = UIObjectManager.Instance.GetObject<Joystick>("Joystick").GetAxis();
            Vector3 axis = new Vector3(axis2d.x, 0, axis2d.y);

            if (axis == default)
            {
                m_playerAgent.velocity = Vector2.zero;
            }
            else
            {
                var desireDir = axis.normalized;
                //var desireSpeed = axis.magnitude * m_speed;
                m_playerAgent.SetDestination(transform.position + desireDir * m_pathfindingDestinationOffset);
                //m_playerAgent.velocity = Vector3.ClampMagnitude(agent.desiredVelocity, desireSpeed);
            }

            //camHolder.position = new Vector3(transform.position.x, camHolder.transform.position.y, transform.position.z);
        }
    }
}
