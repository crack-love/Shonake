using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityCommon;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shotake
{
    // !!!!!!!!!!! LINK UI OBJECT (GET FROM UI HOLDER) WITH INITILIZER CLASS...

    class StageUIManager : MonoBehaviour
    {
        [SerializeField] UIJoystick m_joystick;
        [SerializeField] GameObject m_pausePanel;
        [SerializeField] GameObject m_clearPanel;
        [SerializeField] GameObject m_failPanel;
        [SerializeField] GameObject m_statisticsPanel;

        Stack<GameObject> m_panelStack = new Stack<GameObject>();

        public UIJoystick Joystick => m_joystick;

        public void ShowClearPanel()
        {
            m_clearPanel.SetActive(true);
            m_panelStack.Push(m_clearPanel);
        }

        public void ShowFailPanel()
        {
            m_failPanel.SetActive(true);
            m_panelStack.Push(m_failPanel);
        }

        public void OnPauseButtonClicked()
        {
            TimeManager.Instance.TimeScale = 0;
            m_pausePanel.SetActive(true);
            m_panelStack.Push(m_pausePanel);
        }

        public void OnCloseCurrentPanelButtonClicked()
        {
            if (m_panelStack.Count > 0)
            {
                var top = m_panelStack.Pop();
                top.SetActive(false);

                if (top == m_pausePanel)
                {
                    TimeManager.Instance.TimeScale = 1;
                }

                if (m_panelStack.Count > 0)
                {
                    m_panelStack.Peek().SetActive(true);
                }
            }
        }

        public void OnAdToReviveButtonClicked()
        {
            // show ad

            // if ok, revive
        }

        public void OnAdToRewardButtonClicked()
        {
            // show ad

            // if ok, reward
        }

        public void OnQuitButtonClicked()
        {
            // send result
            // StageResultManager.Instance.dosometing();

            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }

        public void OnStatisticsButtonClicked()
        {
            if (m_panelStack.Count > 0)
            {
                m_panelStack.Peek().SetActive(false);
            }

            m_statisticsPanel.SetActive(true);
            m_panelStack.Push(m_statisticsPanel);
        }
    }
}
