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
    sealed class GameModeManager : MonoBehaviourSingletoneAutoGenerate<GameModeManager>
    {
        [SerializeField] List<GameMode> m_modes = new List<GameMode>();

        public T GetMode<T>(bool autoGenerate = true) where T : GameMode
        {
            foreach(var mode in m_modes)
            {
                if (mode is T t)
                {
                    return t;
                }
            }

            // create new object
            if (autoGenerate)
            {
                GameObject o = new GameObject(typeof(T).Name);
                var m = o.AddComponent<T>();
                m_modes.Add(m);
                return m;
            }
            else
            {
                Debug.LogError("Not exist Mode " + typeof(T).Name);
                return null;
            }
        }

        public void SwitchActiveMode(GameMode src, GameMode dst)
        {
            StartCoroutine(SwitchActiveModeCoroutine(src, dst));
        }

        IEnumerator SwitchActiveModeCoroutine(GameMode src, GameMode dst)
        {
            yield return src.DisableMode();
            yield return dst.EnableMode();
        }

#if UNITY_EDITOR
        public void RegistMode(GameMode src)
        {
            var type = src.GetType();

            foreach(var m in m_modes)
            {
                if (m != null && m.GetType() == type)
                {
                    Debug.LogError(src.GetType().Name + " Duplicated, Destroying last one");
                    DestroyImmediate(src);
                    EditorUtility.SetDirty(this);
                    return;
                }
            }
            m_modes.Add(src);
            EditorUtility.SetDirty(this);
        }
#endif

#if UNITY_EDITOR
        [CustomEditor(typeof(GameModeManager))]
        class GameModeManagerInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                var target = base.target as GameModeManager;

                if (target.m_modes.Count > 0)
                {
                    for (int i = 0; i < target.m_modes.Count; ++i)
                    {
                        var m = target.m_modes[i];
                        if (m == null)
                        {
                            target.m_modes.RemoveAt(i);
                            i -= 1;
                        }
                        else
                        {
                            if (EditorGUILayout.LinkButton(m.GetType().Name))
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