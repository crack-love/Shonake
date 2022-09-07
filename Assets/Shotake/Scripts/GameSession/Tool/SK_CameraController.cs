using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shotake
{
    class SK_CameraController : CameraController
    {
        [SerializeField] Transform m_camHolder;
        [SerializeField] float m_scrollSmoothTime = 0.1f;
        [SerializeField] float m_zoomSmoothTime = 0.3f;

        float m_desireCamSize = 5;

        // cache
        Transform m_playerTrans;
        Camera m_cam;

        private void Update()
        {
            if (!m_playerTrans)
            {
                if (GameManager.Instance.Player)
                {
                    m_playerTrans = GameManager.Instance.Player.transform;
                }
            }
            if (!m_cam)
            {
                m_cam = m_camHolder.GetComponentInChildren<Camera>();
            }

            if (m_playerTrans)
            {
                Vector3 desirePos = m_playerTrans.position;
                desirePos.y = m_camHolder.position.y;
                var actualCamSize = m_desireCamSize;
                if (m_scrollSmoothTime > 0)
                {
                    var dt = TimeManager.Instance.DeltaTime;
                    desirePos = Vector3.Lerp(m_camHolder.position, desirePos, dt / m_scrollSmoothTime);
                }
                if (m_zoomSmoothTime > 0)
                {
                    var dt = TimeManager.Instance.DeltaTime;
                    actualCamSize = Mathf.Lerp(m_cam.orthographicSize, m_desireCamSize, dt / m_zoomSmoothTime);
                }

                m_camHolder.position = desirePos;
                m_cam.orthographicSize = actualCamSize;
            }
        }

        public void SetCamSize(float size)
        {
            m_desireCamSize = size;
        }
    }
}
