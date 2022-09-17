using System.Collections;
using UnityCommon;
using UnityEngine;

namespace Shotake
{
    class MainSceneStageView : GameModeView
    {
        private new void Awake()
        {
            base.Awake();

            UITool.LinkButtonOnClick("StageExitButton", OnExitButtonClicked);
        }

        void OnExitButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<MainSceneEntryView>();
            GameModeManager.Instance.SwitchMode(this, v);
        }
    }
}