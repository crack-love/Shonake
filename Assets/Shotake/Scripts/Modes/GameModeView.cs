using System.Collections;
using UnityEngine;

namespace Shotake
{
    /// <summary>
    /// UI View acting like mode
    /// </summary>
    class GameModeView : GameMode
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
    }
}