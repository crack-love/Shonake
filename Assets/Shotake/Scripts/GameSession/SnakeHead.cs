using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon;
using UnityEngine;
using UnityEngine.Assertions;

namespace Shotake
{
    // capture position, rotation.
    // send to child
    // move child with delta

    // 헤드의 포지션, 로데이션을 직렬로 기록
    // 테일은 현재 헤드의 포지션에서 일정 델타만큼 떨어진 곳의 직렬 위치 표현


    class SnakeHead : SnakeComponent
    {
        public GameObject tailPrefab;
        public List<SnakeTail> tails = new List<SnakeTail>();
        SK_CameraController m_camCon;

        SnakeCaptureSolver2 m_captureSolver;

        void Awake()
        {
            m_captureSolver = new SnakeCaptureSolver2(0.5f, 50, transform.position);
        }

        private void Start()
        {
            UpdateCameraSize();
        }

        void UpdateCameraSize()
        {
            if (!m_camCon)
            {
                m_camCon = GameManager.Instance.CameraController as SK_CameraController;
            }
            
            if (m_camCon)
            {
                m_camCon.SetCamSize(6 + tails.Count * .3f);
            }
        }

        private void Update()
        {
            if (m_captureSolver != null)
            {
                m_captureSolver.CaptureHead(this);
                m_captureSolver.Solve(this, tails.Cast<ISnakeCaptureTarget2>());
                m_captureSolver.DrawDebug();
                m_captureSolver.DrawDebugTails(this, tails.Cast<ISnakeCaptureTarget2>());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null) return;
            if (other.gameObject == false) return;

            var item = other.gameObject.GetComponent<FeedItem>();
            if (item != null)
            {
                var o = Instantiate(tailPrefab);
                tails.Add(o.GetComponent<SnakeTail>());

                //o.transform.localScale = Vector3.Max(transform.localScale - Vector3.one * tails.Count * 0.1f, Vector3.one * 0.1f);

                Destroy(item.gameObject);

                UpdateCameraSize();
            }
        }
    }
}
