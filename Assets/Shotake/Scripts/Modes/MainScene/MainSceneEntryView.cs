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

            UITool.LinkButtonOnClick("EntryProfileButton", OnProfileButtonClicked);
            UITool.LinkButtonOnClick("EntryStageButton", OnStageButtonClicked);
            UITool.LinkButtonOnClick("EntrySettingButton", OnSettingButtonClicked);
            UITool.LinkButtonOnClick("EntryQuitButton", OnQuitButtonClicked);

            // execlusive setting
            GameModeManager.Instance.DisableMode<MainSceneProfileView>();
            GameModeManager.Instance.DisableMode<MainSceneQuitView>();
            GameModeManager.Instance.DisableMode<MainSceneSettingView>();
            GameModeManager.Instance.DisableMode<MainSceneStageView>();
        }

        private void OnSettingButtonClicked()
        {
            var sv = GameModeManager.Instance.GetMode<MainSceneSettingView>();
            if (sv != null)
            {
                GameModeManager.Instance.SwitchMode(this, sv);
            }
        }

        private void OnProfileButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<MainSceneProfileView>();
            if (v != null)
            {
                GameModeManager.Instance.SwitchMode(this, v);
            }
        }

        private void OnStageButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<MainSceneStageView>();
            if (v != null)
            {
                GameModeManager.Instance.SwitchMode(this, v);
            }
        }

        private void OnQuitButtonClicked()
        {
            var qmode = GameModeManager.Instance.GetMode<MainSceneQuitView>();
            if (qmode != null)
            {
                GameModeManager.Instance.SwitchMode(this, qmode);
            }
        }

        private void Update()
        {
            // quit
            if (Input.GetKey(KeyCode.Escape))
            {
                OnQuitButtonClicked();
            }
        }
    }
}