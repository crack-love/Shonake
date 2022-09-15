using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shotake
{
    // update나 코루틴이 있을 수 있으므로 모노비해비어로 한다
    abstract class GameMode : MonoBehaviour
    {
        public virtual IEnumerator EnableMode() { return null; }

        public virtual IEnumerator DisableMode() { return null; }

#if UNITY_EDITOR
        protected void Reset()
        {
            name = GetType().Name;
            GameModeManager.Instance.RegistMode(this);
        }
#endif
    }
}
