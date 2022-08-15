using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Shotake
{
    class SK_PlayerController : PlayerController
    {
        [SerializeField] float m_pathfindingDistance = 3;
        [SerializeField] float m_speed = 5;
        [SerializeField] float m_rotSpeedAtSlow = 0;
        [SerializeField] float m_rotSpeedAtFast = 180;

        Joystick m_joystick;
        SK_Player m_player;
        public float m_angle = 0;

        void Start()
        {
            m_joystick = UIManager.Instance.GetObjectByType<Joystick>();
            m_player = GameManager.Instance.Player as SK_Player;
            m_angle = m_player.transform.rotation.eulerAngles.y;
        }

        // update rotation from code, calc path from agent
        private void Update()
        {
            if (!m_player) m_player = GameManager.Instance.Player as SK_Player;
            if (!m_joystick) m_joystick = UIManager.Instance.GetObjectByType<Joystick>();

            if (m_player && m_joystick)
            {
                var agent = m_player.NavMeshAgent;
                if (agent)
                {
                    agent.updateRotation = false;
                    agent.updateUpAxis = false;

                    var axis = m_joystick.GetAxis();
                    if (axis == default)
                    {
                        agent.velocity = default;
                        agent.SetDestination(agent.transform.position);
                    }
                    else
                    {
                        var dt = TimeManager.Instance.GameDeltaTime;

                        // rotate
                        var axisMag = axis.magnitude;
                        var desireAngle = Vector2.SignedAngle(new Vector2(0, 1), axis);
                        var angleDiff = Mathf.DeltaAngle(m_angle, desireAngle);
                        var angleAccel = Mathf.Lerp(m_rotSpeedAtSlow, m_rotSpeedAtFast, axisMag) * dt;
                        angleDiff %= angleAccel;
                        m_angle = (m_angle + angleDiff + 360) % 360;
                        agent.transform.rotation = Quaternion.Euler(0, -m_angle, 0);

                        // move
                        agent.SetDestination(agent.transform.position + agent.transform.forward * m_pathfindingDistance);
                        agent.velocity = axis.magnitude * m_speed * agent.desiredVelocity.normalized;
                    }
                }
            }
        }

        private void UpdateNavmeshOnlyVersion()
        {
            if (!m_player) m_player = GameManager.Instance.Player as SK_Player;

            if (m_joystick && m_player)
            {
                Vector2 axis2d = m_joystick.GetAxis();
                Vector3 axis = new Vector3(axis2d.x, 0, axis2d.y);

                if (axis == default)
                {
                    m_player.NavMeshAgent.SetDestination(m_player.transform.position);
                    m_player.NavMeshAgent.velocity = default;
                }
                else
                {
                    var desireDir = axis.normalized;
                    var desirePos = m_player.transform.position + desireDir * m_pathfindingDistance;
                    var desireSpeed = axis.magnitude * m_speed;
                    m_player.NavMeshAgent.SetDestination(desirePos);
                    m_player.NavMeshAgent.velocity = m_player.NavMeshAgent.desiredVelocity.normalized * desireSpeed;
                    //m_player.transform.rotation = Quaternion.RotateTowards(m_player.transform.rotation, Quaternion.Euler(desireDir), 360);
                }
            }
        }
    }
}
