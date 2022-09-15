using UnityEditor;
using UnityEngine;
using UnityCommon;

namespace Shotake
{
    [DefaultExecutionOrder(-1)]
    [ExecuteAlways]
    class UIObject : MonoBehaviour
    {
        [SerializeField, ReadOnly] int m_uIObjectID = 0;

        public int UIObjectID => m_uIObjectID;

        protected void Awake()
        {
#if UNITY_EDITOR
            EditorAwake();
#endif
        }

#if UNITY_EDITOR
        private void EditorAwake()
        {
            if (UIManager.Instance.GetObject(m_uIObjectID) != this)
            {
                m_uIObjectID = UIManager.Instance.AddObjectPersistant(this);
            }
        }

        public void SetUIObjectID(int v)
        {
            m_uIObjectID = v;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}