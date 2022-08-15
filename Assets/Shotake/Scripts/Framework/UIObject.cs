using UnityEngine;

namespace Shotake
{
    [DefaultExecutionOrder(-1)]
    abstract class UIObject : MonoBehaviour
    {
        int m_index = 0;

        public int Index => m_index;

        protected void Awake()
        {
            m_index = UIManager.Instance.AddObject(this);
        }
    }
}