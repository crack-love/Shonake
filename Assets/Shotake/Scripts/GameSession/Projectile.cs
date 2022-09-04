using UnityEngine;

namespace Shotake
{
    internal class Projectile : MonoBehaviour
    {
        public float m_speed = 2f;
        public float m_damage = 1f;
        public float m_lifeTime = 3f;
        public int m_damageLayer = int.MaxValue;

        GameObject m_instanter = null;
        float m_remainLifetime = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other != null && other.gameObject)
            {
                var td = other.GetComponent<IDamageTakable>();
                if (td != null)
                {
                    td.TakeDamage(gameObject, m_instanter, m_damage, m_damageLayer);
                    Destroy(gameObject);
                }
            }
        }

        public void SetTarget(GameObject target, GameObject instanter)
        {
            transform.LookAt(target.transform.position);
            m_instanter = instanter;
            m_remainLifetime = m_lifeTime;
        }

        private void Update()
        {
            if (m_remainLifetime < 0)
            {
                Destroy(gameObject);
            }

            transform.position += transform.forward * m_speed * TimeManager.Instance.DeltaTime;
            m_remainLifetime -= TimeManager.Instance.DeltaTime;
        }
    }
}
