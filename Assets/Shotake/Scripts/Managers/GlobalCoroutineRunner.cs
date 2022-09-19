using System.Collections;
using System.Collections.Generic;
using UnityCommon;
using UnityEditor;
using UnityEngine;

namespace Shotake
{
    class GlobalCoroutineRunner : MonobehaviourSingletone<GlobalCoroutineRunner>
    {
        struct CoroutineUnit
        {
            public Coroutine Coroutine;
            public Object RunProvider;
            public bool IsFinished;
            
            public void SetFinished()
            {
                IsFinished = true; 
            }
        }

        readonly List<CoroutineUnit> m_list = new List<CoroutineUnit>();

        public void Run(IEnumerator coroutine)
        {
            var cr = StartCoroutine(WrapCoroutineToKnowFinished(coroutine, m_list.Count - 1));

            m_list.Add(new CoroutineUnit
            {
                Coroutine = cr,
                RunProvider = null,
                IsFinished = false,
            });
        }

        public void Run(IEnumerator coroutine, Object runProvider)
        {
            var cr = StartCoroutine(WrapCoroutineToKnowFinished(coroutine, m_list.Count));

            m_list.Add(new CoroutineUnit
            {
                Coroutine = cr,
                RunProvider = runProvider,
                IsFinished = false,
            });
        }

        IEnumerator WrapCoroutineToKnowFinished(IEnumerator src, int idx)
        {
            yield return src;
            m_list[idx].SetFinished();
            Debug.Log(m_list[idx].IsFinished);
        }

        private void LateUpdate()
        {
            while (m_list.Count > 0)
            {
                var back = m_list[m_list.Count - 1];
                if (back.IsFinished)
                {
                    m_list.RemoveAt(m_list.Count - 1);
                }
                else if (back.RunProvider != null && !back.RunProvider)
                {
                    StopCoroutine(back.Coroutine);
                    m_list.RemoveAt(m_list.Count - 1);
                }
            }
        }
    }
}
