using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    // update shader instance property
    class GrassGroup : MonoBehaviour
    {
        MeshRenderer[] m_renders;
        Transform m_closeOne;
        Transform m_otherOne;
        MaterialPropertyBlock m_props;
        static int m_propId = -1;

        private void OnTriggerEnter(Collider other)
        {
            m_otherOne = other.transform;
        }

        void ValidateReferences()
        {
            if (m_renders == null)
            {
                m_renders = GetComponentsInChildren<MeshRenderer>();
            }
            if (m_props == null)
            {
                m_props = new MaterialPropertyBlock();
            }
            if (m_propId == -1)
            {
                m_propId = Shader.PropertyToID("_TriggerPos");
            }
        }

        private void Update()
        {
            ValidateReferences();

            if (!m_closeOne)
            {
                m_closeOne = m_otherOne;
            }

            if (m_closeOne)
            {
                if (m_otherOne && m_otherOne != m_closeOne)
                {
                    var d0 = (m_closeOne.transform.position - transform.position).sqrMagnitude;
                    var d1 = (m_otherOne.transform.position - transform.position).sqrMagnitude;
                    if (d1 <= d0)
                    {
                        m_closeOne = m_otherOne;
                        m_otherOne = null;
                    }
                }

                m_props.SetVector(m_propId, m_closeOne.transform.position);
            }
            else
            {
                m_props.SetVector(m_propId, Vector4.one * 100000);
            }

            foreach (var r in m_renders)
            {
                r?.SetPropertyBlock(m_props);
            }
        }
    }
}
