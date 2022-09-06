using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Shotake
{
    class EnemyGenerator : MonoBehaviour
    {
        public GeneratePattern m_pattern;
        public float m_stageDuration = 20;
        public GameObject m_prefab;

        float countStack = 0;
        float timeStack = 0;

        private void Update()
        {
            if (!m_pattern) return;

            timeStack += TimeManager.Instance.DeltaTime;
            float t = timeStack / m_stageDuration;
            if (t > 1) return;

            float range, angle, count;
            m_pattern.GetTick(t, out range, out angle, out count);
            countStack += count;

            float rad = Mathf.Deg2Rad * angle;
            Vector2 p1 = new Vector2(0, 1);
            float x2 = Mathf.Cos(rad) * p1.x + Mathf.Sin(rad) * p1.y;
            float y2 = Mathf.Sin(rad) * p1.x + Mathf.Cos(rad) * p1.y;

            var pos = transform.position + new Vector3(x2, 0, y2) * range;
            // todo : clamp pos to map size

            while (countStack >= 1)
            {
                countStack -= 1;
                Instantiate(m_prefab, pos, Quaternion.identity);
            }
        }
    }
}
