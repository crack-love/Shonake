using System.Collections;
using UnityCommon;
#if UNITY_EDITOR
using UnityEditor.Events;
using UnityEditorInternal;
#endif
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

namespace Shotake
{
    class MainSceneEntryView : GameModeView
    {
        private new void Awake()
        {
            base.Awake();

            UITool.LinkButtonOnClick("ProfileButton", OnProfileButtonClicked);
            UITool.LinkButtonOnClick("RankButton", OnRankButtonClicked);
            UITool.LinkButtonOnClick("StageButton", OnStageButtonClicked);
            UITool.LinkButtonOnClick("SettingButton", OnSettingButtonClicked);

            // execlusive setting
            GameModeManager.Instance.DisableMode<MainSceneProfileView>();
            GameModeManager.Instance.DisableMode<MainSceneQuitView>();
            GameModeManager.Instance.DisableMode<MainSceneSettingView>();
            GameModeManager.Instance.DisableMode<MainSceneRankView>();
            GameModeManager.Instance.DisableMode<MainSceneStageView>();
        }

        public void OnSettingButtonClicked()
        {
            var sv = GameModeManager.Instance.GetMode<MainSceneSettingView>();
            if (sv != null)
            {
                GameModeManager.Instance.SwitchMode(this, sv);
            }
        }

        public void OnProfileButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<MainSceneProfileView>();
            if (v != null)
            {
                GameModeManager.Instance.SwitchMode(this, v);
            }
        }

        public void OnRankButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<MainSceneRankView>();
            if (v != null)
            {
                GameModeManager.Instance.SwitchMode(this, v);
            }
        }

        public void OnStageButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<MainSceneStageView>();
            if (v != null)
            {
                GameModeManager.Instance.SwitchMode(this, v);
            }
        }

        private void Update()
        {
            // quit
            if (Input.GetKey(KeyCode.Escape))
            {
                var qmode = GameModeManager.Instance.GetMode<MainSceneQuitView>();
                if (qmode != null)
                {
                    GameModeManager.Instance.SwitchMode(this, qmode);
                }
            }
        }
    }
}