using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Shotake
{
    // navmesh player move controller
    class SK_PlayerController : MonoBehaviour
    {
        [SerializeField] float m_pathfindingDistance = 3;
        [SerializeField] float m_speedAtSlow = 2;
        [SerializeField] float m_speedAtFast = 6;
        [SerializeField] float m_rotSpeedAtSlow = 0;
        [SerializeField] float m_rotSpeedAtFast = 180;

        UIJoystick m_joystick;
        SK_Player m_player;
        float m_angle;

        void ValidateReferences()
        {
            if (!m_player)
            {
                m_player = StageGameManager.Instance.Player as SK_Player;
                if (m_player)
                {
                    m_angle = m_player.transform.rotation.eulerAngles.y;
                }
            }

            if (!m_joystick) m_joystick = UIObjectManager.Instance.GetObject<UIJoystick>();
        }

        private void Update()
        {
            ValidateReferences();

            if (m_player && m_joystick)
            {
                var agent = m_player.NavMeshAgent;
                var axis = m_joystick.GetAxis();
                
                if (agent)
                {
                    UpdateMovement(agent, axis); agent.updateUpAxis = false;
                }
            }
        }

        void UpdateMovement(NavMeshAgent agent, Vector2 axis2d)
        {
            // 로테이션 : 앵글 프로퍼티로 직접 수정
            // 속도 : 에이전트 직접 설정 (엑시스 속력)
            // 목표 지점 : 포워드 + 정적오프셋
            // 레이케스트로 블럭 확인 -> 이동 금지
            // 인벨리드 패스 확인 -> 이동 금지

            if (axis2d == default)
            {
                agent.velocity = Vector3.zero;
            }
            else
            {
                // angle
                var axisMag = axis2d.magnitude;
                var angle = Vector2.SignedAngle(axis2d, Vector2.up);
                var rotSpeed = Mathf.Lerp(m_rotSpeedAtSlow, m_rotSpeedAtFast, axisMag);
                m_angle += Mathf.DeltaAngle(m_angle, angle) % (rotSpeed * Time.deltaTime);
                m_angle %= 360;
                agent.updateRotation = false;
                agent.updateUpAxis = true;
                agent.transform.rotation = Quaternion.Euler(0, m_angle, 0);

                // check raycast
                var raystart = m_player.RaycastPosition;
                if (Physics.Raycast(raystart, agent.transform.forward, out var hit, m_pathfindingDistance, int.MaxValue, QueryTriggerInteraction.Ignore))
                {
                    Debug.DrawRay(raystart, agent.transform.forward * m_pathfindingDistance, Color.red);
                    agent.velocity = Vector3.zero;
                    return;
                }
                else
                {
                    Debug.DrawRay(raystart, agent.transform.forward * m_pathfindingDistance, Color.green);
                }

                // check incalculate path
                var path = agent.path;
                if (!agent.CalculatePath(raystart + agent.transform.forward * m_pathfindingDistance, path))
                {
                    agent.velocity = Vector3.zero;
                    return;
                }

                // velocity
                var moveSpeed = Mathf.Lerp(m_speedAtSlow, m_speedAtFast, axisMag);
                agent.velocity = agent.transform.forward * moveSpeed * axisMag;
            }
        }
    }
}
