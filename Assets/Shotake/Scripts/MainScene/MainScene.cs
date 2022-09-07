using UnityEngine;
using UnityCommon;

namespace Shotake
{
    // 메인 씬 유아이 트랜젝션
    class MainScene : MonoBehaviour
    {
        public GameObject MainButtonsPanel;
        public GameObject StageUIPanel;
        public GameObject SettingUIPanel;
        public GameObject ProfileUIPanel;
        public GameObject QuiteMessagePanel;
        public float m_defaultInputDelayTime = 0.8f;

        [ReadOnly] public GameObject m_currentOverviewUIPanel;
        [ReadOnly] public float m_remainInputDelay = 0f;

        private void Awake()
        {
            MainButtonsPanel.SetActive(true);
            StageUIPanel.SetActive(false);
            SettingUIPanel.SetActive(false);
            ProfileUIPanel.SetActive(false);
            QuiteMessagePanel.SetActive(false);
        }

        public void OnStageButtonClicked()
        {
            if (!UseInputDelayTime()) return;

            if (StageUIPanel && MainButtonsPanel)
            {
                MainButtonsPanel.SetActive(false);
                StageUIPanel.SetActive(true);
                m_currentOverviewUIPanel = StageUIPanel;
            }
        }

        public void OnProfileButtonClicked()
        {
            if (!UseInputDelayTime()) return;

            if (ProfileUIPanel && MainButtonsPanel)
            {
                MainButtonsPanel.SetActive(false);
                ProfileUIPanel.SetActive(true);
                m_currentOverviewUIPanel = ProfileUIPanel;
            }
        }

        public void OnSettingButtonClicked()
        {
            if (!UseInputDelayTime()) return;

            if (SettingUIPanel && MainButtonsPanel)
            {
                MainButtonsPanel.SetActive(false);
                SettingUIPanel.SetActive(true);
                m_currentOverviewUIPanel = SettingUIPanel;
            }
        }

        public void CloseOverviewUI()
        {
            if (!UseInputDelayTime()) return;

            if (m_currentOverviewUIPanel && MainButtonsPanel)
            {
                m_currentOverviewUIPanel.SetActive(false);
                MainButtonsPanel.SetActive(true);
                m_currentOverviewUIPanel = null;
            }
        }

        bool UseInputDelayTime()
        {
            if (m_remainInputDelay > 0)
            {
                return false;
            }
            else
            {
                m_remainInputDelay = m_defaultInputDelayTime;
                return true;
            }
        }

        private void Update()
        {
            if (m_remainInputDelay > 0)
            {
                m_remainInputDelay -= Time.deltaTime;
                return;
            }

            // It's effective with android back key
            if (Input.GetKey(KeyCode.Escape))
            {
                if (m_currentOverviewUIPanel == null)
                {
                    if (QuiteMessagePanel)
                    {
                        QuiteMessagePanel.SetActive(true);
                        m_currentOverviewUIPanel = QuiteMessagePanel;
                    }
                }
                else
                {
                    CloseOverviewUI();
                }

                UseInputDelayTime();
            }
        }
    }
}