using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Shotake
{
    class UIButton : UIObject, IPointerClickHandler//, IPointerDownHandler, IPointerUpHandler, 
    {
        [SerializeField] UnityEvent m_OnClick = new UnityEvent();

        public UnityEvent OnClick => m_OnClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            //Debug.Log("OnPointerClick " + name);
            m_OnClick.Invoke();
        }
    }
}