using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon;
using UnityEngine;

namespace Shotake
{
    class StageProcessor
    {
        StageEntity m_stage = null;
        int m_currWaveIndex = 0;
        float m_stageElapseTime = 0;
        float m_waveTimeStack = 0;
        List<float> m_waveCountStack = new List<float>();

        public bool IsFinished
        {
            get
            {
                if (!m_stage)
                {
                    return true;
                }
                else
                {
                    return m_currWaveIndex >= m_stage.Waves.Count;
                }
            }
        }

        public bool IsStageSetted
        {
            get
            {
                return m_stage != null;
            }
        }

        public void SetConfig(StageEntity entity)
        {
            m_stage = entity;
            m_currWaveIndex = 0;
            m_stageElapseTime = 0;
        }

        public void Update()
        {
            if (!m_stage) return;
            if (m_currWaveIndex >= m_stage.Waves.Count) return;

            var dt = TimeManager.Instance.DeltaTime;
            var wave = m_stage.Waves[m_currWaveIndex];

            if (m_waveTimeStack > wave.Duration)
            {
                // next wave
                MoveNextWave();
            }
            else
            {
                m_waveCountStack.SetSize(wave.Patterns.Count);

                for (int i = 0; i < wave.Patterns.Count; ++i)
                {
                    var pair = wave.Patterns[i];
                    float count = m_waveCountStack[i];
                    Generate(pair.Pattern, pair.Prefab, wave.Duration, m_waveTimeStack, ref count);
                    m_waveCountStack[i] = count;
                }
            }

            m_stageElapseTime += dt;
            m_waveTimeStack += dt;
        }

        void MoveNextWave()
        {
            m_waveTimeStack = 0;
            m_currWaveIndex += 1;
            m_waveCountStack.Clear();
            Debug.Log("Move Next Wave from " + (m_currWaveIndex - 1) + " to " + m_currWaveIndex);
        }

        private void Generate(GeneratePattern pattern, GameObject prefab, float duration, float timeStack, ref float countStack)
        {
            if (!pattern) return;

            float t = timeStack / duration;
            if (t > 1) return;

            float range, angle, count;
            pattern.GetTick(t, out range, out angle, out count);
            countStack += count;

            float rad = Mathf.Deg2Rad * angle;
            Vector2 p1 = new Vector2(0, 1);
            float x2 = Mathf.Cos(rad) * p1.x + Mathf.Sin(rad) * p1.y;
            float y2 = Mathf.Sin(rad) * p1.x + Mathf.Cos(rad) * p1.y;

            var player = StageGameManager.Instance.Player;
            var pos = player.transform.position + new Vector3(x2, 0, y2) * range;

            //clamp pos to map size
            var mapSize = MapFloor.Instance.MapSize;
            var mapSize3d = new Vector3(mapSize.x, 0, mapSize.y);
            pos = Vector3.Max(Vector3.Min(pos, mapSize3d / 2), -mapSize3d / 2);

            while (countStack >= 1)
            {
                countStack -= 1;
                GameObject.Instantiate(prefab, pos, Quaternion.identity);
            }
        }
    }
}
