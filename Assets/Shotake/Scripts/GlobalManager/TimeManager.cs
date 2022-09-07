using System;
using UnityEngine;
using UnityCommon;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shotake
{
    [ExecuteAlways]
    public class TimeManager : MonobehaviourSingletone<TimeManager>
    {
        [SerializeField] float m_engineDeltaTime = 0;
        [SerializeField] float m_gameDeltaTime = 0;
        [SerializeField] float m_gameTimeScale = 1;
        [SerializeField] bool m_editorUpdate = false;

#if UNITY_EDITOR
        readonly EditorClock m_edClock = new EditorClock();
#endif

        public float EngineDeltaTime
        {
            get => m_engineDeltaTime;
        }

        public float DeltaTime
        {
            get => m_gameDeltaTime;
        }

        public float GameTimeScale
        {
            get => m_gameTimeScale;
            set => m_gameTimeScale = value;
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                UpdateDelta(Time.deltaTime);
            }
#if UNITY_EDITOR
            else
            {
                if (m_editorUpdate && !m_edClock.IsRunning)
                {
                    m_edClock.SetRunning(true, this);
                }
                else if (!m_editorUpdate && m_edClock.IsRunning)
                {
                    m_edClock.SetRunning(false, this);
                }
            }
#endif
        }

        void UpdateDelta(float dt)
        {
            m_engineDeltaTime = dt;
            m_gameDeltaTime = dt * m_gameTimeScale;
        }

#if UNITY_EDITOR
        class EditorClock
        {
            double m_lastTime = 0;
            bool m_isRunning = false;
            TimeManager m_man;

            public bool IsRunning
            {
                get => m_isRunning;
            }

            public void SetRunning(bool run, TimeManager man)
            {
                if (m_isRunning && !run)
                {
                    m_isRunning = run;
                    m_man = man;
                    EditorApplication.update -= EditorUpdate;
                }
                else if (!m_isRunning && run)
                {
                    m_isRunning = run;
                    m_lastTime = EditorApplication.timeSinceStartup;
                    m_man = man;
                    EditorApplication.QueuePlayerLoopUpdate();
                    EditorApplication.update += EditorUpdate;

                }
            }

            void EditorUpdate()
            {
                double dt = EditorApplication.timeSinceStartup - m_lastTime;
                m_lastTime = EditorApplication.timeSinceStartup;
                float dts = Convert.ToSingle(dt);
                m_man.UpdateDelta(dts);
            }
        }
#endif
    }
}