using System;
using System.Collections.Generic;
using UnityEngine;
using UnityCommon;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;
using System.Collections;
using Unity.Plastic.Antlr3.Runtime.Debug;

namespace Shotake
{
    // [세션 게임 매니저]
    // 스테이지 진행
    // 3/2/1 시작
    // 시나리오 진행
    // 일시정지
    // 종료
    // 메인 화면으로
    class StageGameManager : MonobehaviourSingletone<StageGameManager>
    {
        [SerializeField] SK_Player m_player;
        [SerializeField] SK_PlayerState m_playerState;
        [SerializeField] SK_PlayerController m_playerController;
        [SerializeField] SK_CameraController m_CameraController;
        [SerializeField] StageEntity m_stageEntity;
        [SerializeField] MapFloor m_stageFloor;
        [SerializeField] StageState m_stageState;

        public SK_Player Player => m_player;
        public SK_PlayerState PlayerState => m_playerState;
        public SK_PlayerController PlayerController => m_playerController;
        public SK_CameraController CameraController => m_CameraController;
        public StageEntity StageEntity => m_stageEntity;
        public MapFloor StageFloor => m_stageFloor;

        public StageState StageState => m_stageState;

        StageProcessor stageProc = new StageProcessor();

        private IEnumerator Start()
        {
            // 3/2/1 시작
            yield return Counter(2f);

            if (!stageProc.IsStageSetted)
            {
                stageProc.SetConfig(m_stageEntity);
            }

            // wave process
            while (!stageProc.IsFinished)
            {
                if (m_playerState.HP <= 0)
                {
                    break;
                }

                stageProc.Update();
                yield return null;
            }

            // fail
            if (m_playerState.HP <= 0)
            {
                var failPanel = UIManager.Instance.GetObject("FailPanel");
                TimeManager.Instance.TimeScale = 0;

                if (failPanel)
                {
                    failPanel.gameObject.SetActive(true);

                    // get answer from dialog
                    // restart or quit(load level)
                }
            }
            // clear
            else
            {
                var clearPanel = UIManager.Instance.GetObject("ClearPanel");
                TimeManager.Instance.TimeScale = 0;

                if (clearPanel)
                {
                    clearPanel.gameObject.SetActive(true);

                    // get answer from dialog
                    // restart or quit(load level)
                }
            }
        }

        IEnumerator Counter(float time)
        {
            while (time > 0)
            {
                time -= TimeManager.Instance.DeltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }

}
