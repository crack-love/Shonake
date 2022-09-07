using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shotake
{
    sealed class Joystick : UIObject, IPointerDownHandler, IDragHandler, IPointerUpHandler, IAxisProvider
    {
        [SerializeField] float m_range = 1;
        [SerializeField] float m_threshold = 0.1f;
        [SerializeField] RectTransform m_borderImage;
        [SerializeField] RectTransform m_fillImage;        

        Canvas m_canvas;
        public Vector2 m_axis;
        Vector2 m_begPos;        
        
        new void Awake()
        {
            base.Awake();
            m_canvas = GetComponentInParent<Canvas>();
        }

        public Vector2 GetAxis()
        {
            return m_axis;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            m_borderImage.gameObject.SetActive(true);
            m_fillImage.gameObject.SetActive(true);
            m_borderImage.position = eventData.position;
            m_fillImage.position = eventData.position;
            m_begPos = eventData.position;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            float width = m_borderImage.rect.width * m_canvas.scaleFactor;
            Vector2 offset = eventData.position - m_begPos;
            Vector2 offsetC = Vector2.ClampMagnitude(offset, width * m_range);
            
            if (offsetC.sqrMagnitude < m_threshold * width * m_threshold * width)
            {
                m_fillImage.position = m_begPos;
                m_axis = Vector2.zero;
                return;
            }

            Vector2 offsetCT = Vector2.ClampMagnitude(offsetC, width * m_threshold);
            m_axis = (offsetC - offsetCT) / (width * m_range - m_threshold * width);
            m_fillImage.position = m_begPos + offsetC;
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            m_borderImage.gameObject.SetActive(false);
            m_fillImage.gameObject.SetActive(false);
            m_axis = Vector2.zero;
        }
    }
}
