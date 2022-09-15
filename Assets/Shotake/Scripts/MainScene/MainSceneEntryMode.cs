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
    class MainSceneEntryMode : GameMode
    {
        public void OnSettingButtonClicked()
        {
            var sv = UIManager.Instance.GetObject<MainSecneSettingView>();
            if (sv != null)
            {
                GameModeManager.Instance.SwitchActiveMode(this, sv);
            }
        }

        public void OnProfileButtonClicked()
        {

        }

        public void OnRankButtonClicked()
        {

        }

        public void OnStageButtonClicked()
        {

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

        private void Update()
        {
            // quit
            if (Input.GetKey(KeyCode.Escape))
            {
                var qmode = GameModeManager.Instance.GetMode<MainSceneQuitMode>();
                if (qmode != null)
                {
                    GameModeManager.Instance.SwitchActiveMode(this, qmode);
                }
            }
        }
    }
}