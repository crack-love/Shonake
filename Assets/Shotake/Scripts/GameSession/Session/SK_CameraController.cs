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

        void ValidateReferences()
        {
            if (!m_playerTrans)
            {
                var p = StageGameManager.Instance.Player;
                if (p)
                {
                    m_playerTrans = p.transform;
                }
            }

            if (!m_cam)
            {
                if (m_camHolder)
                {
                    m_cam = m_camHolder.GetComponentInChildren<Camera>();
                    m_desireCamSize = m_cam.orthographicSize;
                }
            }
        }

        private void Update()
        {
            ValidateReferences();

            if (m_playerTrans)
            {
                Vector3 applyPos = m_playerTrans.position;
                applyPos.y = m_camHolder.position.y;
                float applyCamSize = m_desireCamSize;
                float dt = TimeManager.Instance.DeltaTime;

                if (m_scrollSmoothTime > 0)
                {
                    applyPos = Vector3.Lerp(m_camHolder.position, applyPos, dt / m_scrollSmoothTime);
                }
                if (m_zoomSmoothTime > 0)
                {
                    applyCamSize = Mathf.Lerp(m_cam.orthographicSize, m_desireCamSize, dt / m_zoomSmoothTime);
                }

                m_camHolder.position = applyPos;
                m_cam.orthographicSize = applyCamSize;
            }
        }

        public void SetDesireCamSize(float size)
        {
            m_desireCamSize = size;
        }
    }
}
