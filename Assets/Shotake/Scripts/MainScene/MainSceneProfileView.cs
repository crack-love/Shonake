using System.Collections;
using UnityCommon;
using UnityEngine;

namespace Shotake
{
    class MainSceneProfileView : GameModeView
    {
        private new void Awake()
        {
            base.Awake();

            UITool.LinkButtonOnClick("ProfileExitButton", OnProfileExitButtonClicked);
        }

        void OnProfileExitButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<MainSceneEntryView>();
            GameModeManager.Instance.SwitchMode(this, v);
        }
    }
}