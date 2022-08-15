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

        SnakeCaptureSolver m_captureSolver;

        void Awake()
        {
            m_captureSolver = new SnakeCaptureSolver(0.5f, 2, 100, this);
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
            var temp = ListPool<ISnakeCaptureTarget>.Get();
            tails.Cast<ISnakeCaptureTarget>().ToList(temp);

            m_captureSolver.CaptureCurrent(this);
            m_captureSolver.SolveCurrent(this, temp);
            m_captureSolver.DrawDebug();

            temp.ClearReturn();
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

                Destroy(item.gameObject);

                UpdateCameraSize();
            }
        }

        public override float GetComponentWidth()
        {
            return 2f;
        }
    }
}
