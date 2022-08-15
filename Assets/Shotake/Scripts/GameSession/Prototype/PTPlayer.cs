using UnityEngine;
using UnityCommon;
using UnityEngine.AI;
using System.Collections;

namespace Shotake
{
    [RequireComponent(typeof(NavMeshAgent))]
    //[RequireComponent(typeof(CharacterController))]
    class PTPlayer : MonobehaviourSingletone<PTPlayer>
    {
        public float speed;
        Joystick joystick;
        NavMeshAgent agent;

        private void Awake()
        {
            joystick = FindObjectOfType<Joystick>();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var axis = joystick.GetAxis();
            //axis *= speed * Time.deltaTime;
            axis.Normalize();
            axis *= speed;

            if (axis != default)
            {
                // 앞에 벽이 있으면 벽까지
                // 아래 
                Vector3 desireDst = transform.position;

                var rayorigin = transform.position + new Vector3(axis.x, 0, axis.y);
                Vector3 destination = rayorigin;
                //if (Physics.Raycast(rayorigin, Vector3.up, out var hit1, speed))
                //{
                //    destination = hit1.point;
                //    Debug.DrawRay(rayorigin, Vector3.up * speed, Color.red);
                //}
                //else if (Physics.Raycast(rayorigin, Vector3.down, out var hit2, speed))
                //{
                //    destination = hit2.point;
                //    Debug.DrawRay(rayorigin, Vector3.down * speed, Color.red);
                //}

                
                agent.SetDestination(destination);
                //agent.Move(new Vector3(axis.x, 0, axis.y));
                //agent.velocity = new Vector3(axis.x, 0, axis.y);

            }

            //var con = GetComponent<CharacterController>();
            //con.Move(new Vector3(axis.x, -1, axis.y));
        }
    }
}
