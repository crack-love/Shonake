using System.Collections;

namespace Shotake
{
    class MainSceneQuitMode : GameMode
    {
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
            var m = GameModeManager.Instance.GetMode<MainSceneEntryMode>();
            if (m != null)
            {
                GameModeManager.Instance.SwitchActiveMode(this, m);
            }
        }
    }
}