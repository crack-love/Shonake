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
    [RequireComponent(typeof(SnakeCaptureSolver))]
    // 포지션 캡쳐, 솔버 업데이트
    // 테일 관리
    class SnakeHead : SnakeComponent
    {
        public float m_captureGap = 0.5f;
        public float m_maxDistance = 100;
        public AnimationCurve m_sizeGraph;

        readonly List<SnakeTail> m_tails = new List<SnakeTail>();

        SnakeCaptureSolver m_captureSolver;

        void Awake()
        {
            m_captureSolver = GetComponent<SnakeCaptureSolver>();
            m_captureSolver.Initialize(transform.position);
        }

        private void Update()
        {
            if (m_captureSolver != null)
            {
                m_captureSolver.CaptureHead(this);
                m_captureSolver.Solve(this, m_tails);
                
                m_captureSolver.DrawDebugCaptures();
                m_captureSolver.DrawDebugTails(this, m_tails);
            }

            // allign normal with terrain
            int terrainMask = LayerMask.GetMask("Terrain");
            if (Physics.Raycast(GetBegPosition().position, Vector3.down, out var bhit, 10, terrainMask, QueryTriggerInteraction.Ignore))
            {
                if (Physics.Raycast(GetEndPosition().position, Vector3.down, out var ehit, 10, terrainMask, QueryTriggerInteraction.Ignore))
                {
                    var hitDir = (bhit.point - ehit.point).normalized;
                    var applyDir = Vector3.Lerp(transform.forward, hitDir, TimeManager.Instance.DeltaTime); // smooth
                    transform.LookAt(transform.position + applyDir);
                }
            }
        }

        public void AddTail(SnakeTail tail)
        {
            m_tails.Add(tail);

            for (int i = 0; i < m_tails.Count; ++i)
            {
                var p = (float)i / m_tails.Count;
                var size = m_sizeGraph.Evaluate(p);
                m_tails[i].SetSize(size);
            }
        }

        public void RemoveTail(SnakeTail tail)
        {
            for (int i = 0;i  < m_tails.Count; ++i)
            {
                if (m_tails[i].GetTransform() == tail.transform)
                {
                    m_tails.RemoveAt(i);
                    break;
                }
            }

            for (int i = 0; i < m_tails.Count; ++i)
            {
                var p = (float)i / m_tails.Count;
                var size = m_sizeGraph.Evaluate(p);
                m_tails[i].SetSize(size);
            }
        }
    }
}
