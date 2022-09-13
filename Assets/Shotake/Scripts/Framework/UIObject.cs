using System;
using UnityEngine;

namespace Shotake
{
    [DefaultExecutionOrder(-1)]
    class UIObject : MonoBehaviour
    {
        [SerializeField] int m_uiObjectID = 0;

        public int UIObjectID => m_uiObjectID;

#if UNITY_EDITOR
        private void Reset()
        {
            m_uiObjectID = UIManager.Instance.AddObjectPersistant(this);
        }

        internal void SetUIObjectID(int v)
        {
            throw new NotImplementedException();
        }
#endif
    }
}