using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Shotake
{
    class UIButton : UIObject, IPointerClickHandler
    {
        [SerializeField] UnityEvent m_OnClick = new UnityEvent();

        public UnityEvent OnClick => m_OnClick;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            m_OnClick.Invoke();
        }
    }
}