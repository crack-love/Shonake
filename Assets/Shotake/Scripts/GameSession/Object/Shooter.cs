using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets;
using UnityEngine;

namespace Shotake
{
    // shot projectile to locked enemy
    class Shooter : MonoBehaviour
    {
        static Collider[] res = new Collider[32];

        public float m_radius = 3f;
        public float m_reload = 1f;
        public Projectile m_projectile = null;
        public bool m_debugDrawGizmo = false;
        public Transform m_projSpawnPos = null;

        float m_reloadRemainTime = 0;

        GameObject FindNearTarget()
        {
            float r = 2;
            var pos = transform.position;

            while (r <= m_radius)
            {
                
                int layer = LayerMask.GetMask("Enemy");
                if (Physics.CheckSphere(pos, r, layer, QueryTriggerInteraction.Ignore))
                {
                    int cnt = Physics.OverlapSphereNonAlloc(pos, r, res, layer, QueryTriggerInteraction.Ignore);
                    if (cnt > 0)
                    {
                        int mini = -1;
                        float mindis = float.MaxValue;
                        for (int i = 0; i < cnt; ++i)
                        {
                            if (!res[i]) continue;
                            var d = (pos - res[i].transform.position).sqrMagnitude;
                            if (d < mindis)
                            {
                                mini = i;
                                mindis = d;
                            }
                        }

                        if (mini >= 0)
                        {
                            return res[mini].gameObject;
                        }
                    }
                }

                if (r * 2 > m_radius && r < m_radius)
                {
                    r = m_radius;
                    continue;
                }
                else
                {
                    r *= 2;
                }
            }

            return null;
        }

        private void OnDrawGizmos()
        {
            if (m_debugDrawGizmo)
            {
                Gizmos.DrawWireSphere(transform.position, m_radius);
            }
        }

        private void Update()
        {
            if (m_reloadRemainTime <= 0)
            {
                var target = FindNearTarget();
                if (target)
                {
                    if (m_debugDrawGizmo)
                    {
                        Debug.DrawLine(transform.position, target.transform.position, Color.red);
                    }

                    if (m_projectile)
                    {
                        var o = Instantiate(m_projectile, m_projSpawnPos.position, Quaternion.identity);
                        o.SetTarget(target, gameObject);
                        m_reloadRemainTime = m_reload;
                    }
                }
            }
            else
            {
                m_reloadRemainTime -= TimeManager.Instance.DeltaTime;
            }
        }
    }
    
}
