using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityCommon;
using UnityEngine;
using UnityEngine.UI;

namespace Shotake
{
    class StageUIManager : MonobehaviourSingletone<StageUIManager>
    {
        [SerializeField] Joystick m_joystick;
        [SerializeField] Button m_pauseButton;
        [SerializeField] RectTransform m_expBarFill;
        [SerializeField] RectTransform m_expBarBack;
        [SerializeField] TextMeshProUGUI m_waveText;
        [SerializeField] GameObject m_pausePanel;
        [SerializeField] GameObject m_clearPanel;

        public Joystick Joystick => m_joystick;

        void Start()
        {
            m_pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }

        // add animation?
        public void OnPauseButtonClicked()
        {
            TimeManager.Instance.TimeScale = 0;
            
            m_pausePanel.SetActive(true);
        }
    }
}
