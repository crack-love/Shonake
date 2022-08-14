using UnityEngine;
using UnityEngine.AI;

namespace Shotake
{
    [RequireComponent(typeof(NavMeshAgent))]
    class PTPlayerNavmesh : MonoBehaviour
    {
        public NavMeshAgent agent;
        public float speed = 10;
        public float pathfindingDestinationOffset = 2;
        public Transform camHolder;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var joystick = UIObjectManager.Instance.GetObject<Joystick>("Joystick");
            Vector2 axis = joystick.GetAxis();
            if (axis == default)
            {
                agent.velocity = Vector2.zero;
            }
            else
            {
                agent.speed = speed;
                //agent.isStopped = false;
                var desireDir = new Vector3(axis.x, 0, axis.y).normalized;
                var desireSpeed = new Vector3(axis.x, 0, axis.y).magnitude * speed;
                agent.SetDestination(transform.position + desireDir * pathfindingDestinationOffset);
                agent.velocity = Vector3.ClampMagnitude(agent.desiredVelocity, desireSpeed);
            }

            camHolder.position = new Vector3(transform.position.x, camHolder.transform.position.y, transform.position.z);
        }
    }
}
