using UnityEngine;

namespace Shotake
{
    abstract class UIObject : MonoBehaviour
    {
        [SerializeField] string m_uiName;

        int m_index = 0;

        public string UIName => m_uiName;

        public int UIIndex => m_index;

        protected void Awake()
        {
            m_index = UIObjectManager.Instance.AddObject(this);
        }
    }
}