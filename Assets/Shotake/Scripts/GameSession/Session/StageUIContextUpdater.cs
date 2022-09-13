using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityCommon;
using UnityEngine;

namespace Shotake
{
    class StageUIContextUpdater : MonoBehaviour
    {
        [SerializeField] float m_updateDelay = 1f;
        float m_remainDelay = 0f;

        private void Update()
        {
            var udt = TimeManager.Instance.UnscaledDeltaTime;
            m_remainDelay = Mathf.Max(m_remainDelay - udt, 0);
            if (m_remainDelay > 0f)
            {
                return;
            }
            m_remainDelay = m_updateDelay;

            // update wavetext
            var stageState = StageGameManager.Instance.StageState;
            var stageEntity = StageGameManager.Instance.StageEntity;
            if (stageState && stageEntity)
            {
                var currWave = stageState.CurrentWaveIndex;
                var maxWave = stageEntity.Waves.Count;
                var waveTextUi = UIManager.Instance.GetObjectByName("WaveText");
                if (waveTextUi)
                {
                    var waveTextUiCmp = waveTextUi.GetComponent<TextMeshProUGUI>();
                    if (waveTextUiCmp)
                    {
                        waveTextUiCmp.text = (currWave.ToString() + "/" + maxWave.ToString());
                    }
                }
            }

            // update expbar
            var playerState = StageGameManager.Instance.PlayerState;
            if (playerState)
            {
                var curExp = playerState.Exp;
                var maxExp = playerState.MaxExp;
                var expBarBackUi = UIManager.Instance.GetObjectByName("ExpBarBack");
                var expBarFillUi = UIManager.Instance.GetObjectByName("ExpBarFill");
                var expBarTextUi = UIManager.Instance.GetObjectByName("ExpBarText");
                if (expBarBackUi && expBarFillUi && expBarTextUi)
                {
                    var backWidth = expBarBackUi.GetComponent<RectTransform>().rect.width;
                    var fillRect = expBarFillUi.GetComponent<RectTransform>();
                    fillRect.sizeDelta = new Vector2(backWidth * curExp / maxExp * 2, fillRect.sizeDelta.y);
                    var textCmp = expBarTextUi.GetComponent<TextMeshProUGUI>();
                    if (textCmp)
                    {
                        textCmp.text = curExp.ToString() + "/" + maxExp.ToString();
                    }
                }
            }
        }
    }
}
