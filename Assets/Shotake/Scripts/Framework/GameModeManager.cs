using System;
using System.Collections;
using System.Collections.Generic;
using UnityCommon;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; 
#endif

namespace Shotake
{
    interface IGameModeManager
    {
        bool IsLoaded<T>() where T : GameMode;

        T GetMode<T>() where T : GameMode;

        T GetOrLoadMode<T>() where T : GameMode;

        void EnableMode<T>() where T : GameMode;

        void DisableMode<T>() where T : GameMode;

        void SwitchMode(GameMode off, GameMode on);

        void SwitchMode<TSrc, TDsr>() where TSrc : GameMode where TDsr : GameMode;
    }

    [DefaultExecutionOrder(ExecutionOrder)]
    sealed class GameModeManager : MonoBehaviourSingletoneAutoGenerate<GameModeManager, IGameModeManager>, IGameModeManager
    {
        readonly Dictionary<Type, GameMode> m_modeDic = new Dictionary<Type, GameMode>();

        public const int ExecutionOrder = -2;
        public const int ChildExecutionOrder = ExecutionOrder + 1;

        private void Awake()
        {
            // load all modes in scene
            var modes = FindObjectsOfType(typeof(GameMode), true);
            for (int i = 0; i < modes.Length; ++i)
            {
                if (modes[i] is GameMode m)
                {
                    m_modeDic.Add(modes[i].GetType(), m);
                    m.SetRegisted();
                }
            }
        }

        public bool IsLoaded<T>() where T : GameMode
        {
            return m_modeDic.ContainsKey(typeof(T));
        }

        public T GetMode<T>() where T : GameMode
        {
            if (m_modeDic.ContainsKey(typeof(T)))
            {
                return (T)m_modeDic[typeof(T)];
            }
            else
            {
                return null;
            }
        }

        public T GetOrLoadMode<T>() where T : GameMode
        {
            if (m_modeDic.ContainsKey(typeof(T)))
            {
                return (T)m_modeDic[typeof(T)];
            }
            else
            {
                GameObject o = new GameObject(typeof(T).Name);
                var m = o.AddComponent<T>();
                m_modeDic.Add(typeof(T), m);
                m.SetRegisted();
                return m;
            }
        }
        public void SwitchMode(GameMode off, GameMode on)
        {
            if (on != null && off != null)
            {
                StartCoroutine(SwitchModeCoroutine(off, on));
            }
        }

        public void SwitchMode<TSrc, TDst>() where TSrc : GameMode where TDst : GameMode
        {
            var src = GetMode<TSrc>();
            var dst = GetMode<TDst>();
            if (src != null && dst != null)
            {
                StartCoroutine(SwitchModeCoroutine(src, dst));
            }
        }

        IEnumerator SwitchModeCoroutine(GameMode src, GameMode dst)
        {
            yield return src.DisableMode();
            yield return dst.EnableMode();
        }

        public void EnableMode<T>() where T : GameMode
        {
            var mode = GetMode<T>();
            if (mode != null)
            {
                StartCoroutine(EnableModeCoroutine(mode));
            }
        }

        IEnumerator EnableModeCoroutine(GameMode mode)
        {
            yield return mode.EnableMode();
        }

        public void DisableMode<T>() where T : GameMode
        {
            var mode = GetMode<T>();
            if (mode != null)
            {
                StartCoroutine(DisableModeCoroutine(mode));
            }
        }

        IEnumerator DisableModeCoroutine(GameMode mode)
        {
            yield return mode.DisableMode();
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(GameModeManager))]
        class GameModeManagerInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                var target = base.target as GameModeManager;
                
                if (target.m_modeDic.Count > 0)
                {
                    foreach (var pair in target.m_modeDic)
                    {
                        var m = pair.Value;
                        if (m == null)
                        {
                            EditorGUILayout.LabelField(pair.Key.Name + " (miss)");
                        }
                        else if (!m)
                        {
                            EditorGUILayout.LabelField(pair.Key.Name + " (invalid)");
                        }
                        else 
                        {
                            if (EditorGUILayout.LinkButton(pair.Key.Name))
                            {
                                EditorGUIUtility.PingObject(m);
                            }
                        }
                    }
                }
            }
        }
#endif
    }
}