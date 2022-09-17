using System.Collections;

namespace Shotake
{
    class MainSceneQuitView : GameModeView
    {
        private new void Awake()
        {
            base.Awake();

            UITool.LinkButtonOnClick("QuitSubmitButton", OnSubmitButtonClicked);
            UITool.LinkButtonOnClick("QuitCancelButton", OnCancelButtonClicked);
        }

        public override IEnumerator EnableMode()
        {
            gameObject.SetActive(true);
            return null;
        }

        public override IEnumerator DisableMode()
        {
            gameObject.SetActive(false);
            return null;
        }

        void OnSubmitButtonClicked()
        {
            UnityEngine.Application.Quit(0);
        }

        void OnCancelButtonClicked()
        {
            var m = GameModeManager.Instance.GetMode<MainSceneEntryView>();
            if (m != null)
            {
                GameModeManager.Instance.SwitchMode(this, m);
            }
        }
    }
}