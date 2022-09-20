using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon;
using UnityEngine;

namespace Shotake
{
    class WaveProcesser
    {
        StageWave m_wave = null;
        float m_timeStack = 0;
        List<float> m_countStack = new List<float>();

        public bool IsFinished
        {
            get
                {
                if (m_wave != null)
                {
                    return m_timeStack >= m_wave.Duration;
                }
                else
                {
                    return true;
                }
            }
        }

        public void SetWave(StageWave wave)
        {
            m_wave = wave;
            m_timeStack = 0;
            for (int i = 0; i < m_countStack.Count; ++i)
            {
                m_countStack[i] = 0;
            }
            m_countStack.SetSize(m_wave.Patterns.Count);
        }

        public void Update()
        {
            if (m_wave == null) return;
            if (m_timeStack >= m_wave.Duration) return;
            if (GameModeManager.Instance.GetMode<StagePlayMode>())

            var dt = TimeManager.Instance.DeltaTime;
            var t = m_timeStack / m_wave.Duration;

            for (int i = 0; i < m_wave.Patterns.Count; ++i)
            {
                var pattern = m_wave.Patterns[i];
                var count = m_countStack[i];
                Generate(pattern, t, ref count);
                m_countStack[i] = count;
            }

            m_timeStack += dt;
        }


        private void Generate(PatternAndPrefab set, float timePercent, ref float countStack)
        {
            if (set == null) return;
            if (timePercent > 1) return;

            float range, angle, count;
            set.Pattern.GetTick(timePercent, out range, out angle, out count);
            countStack += count * set.CountPerSecScalar;

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
