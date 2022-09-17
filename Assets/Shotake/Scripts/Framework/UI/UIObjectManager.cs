using System.Collections.Generic;
using UnityCommon;
using UnityEngine;
using Mono.Cecil;
using UnityEngine.Rendering;
using System.Diagnostics;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shotake
{
    interface IUIObjectManager
    {
        UIObject GetObject(string name);

        UIObject GetObject(int id);

        T GetObject<T>() where T : class;

        T GetObject<T>(string name) where T : class;

        T GetObject<T>(int id) where T : class;

        void RegistObject(UIObject obj);
    }

    /// <summary>
    /// Holding UIObjects
    /// </summary>
    [DefaultExecutionOrder(ExecutionOrder)]
    class UIObjectManager : MonoBehaviourSingletoneAutoGenerate<UIObjectManager, IUIObjectManager>, IUIObjectManager
    {
        [SerializeField] bool m_loadOnlyChildUIObjects = false;

        public const int ExecutionOrder = -2;
        public const int ChildExecutionOrder = ExecutionOrder + 1;

        readonly List<UIObject> m_objects = new List<UIObject>();
        readonly Dictionary<string, UIObject> m_objectNameIndex = new Dictionary<string, UIObject>();

        private void Awake()
        {
            // load all uiobjects in scene
            if (m_loadOnlyChildUIObjects)
            {
                foreach (var o in GetComponentsInChildren<UIObject>(true))
                {
                    RegistObject(o);
                }
            }
            else
            {
                foreach (var o in FindObjectsOfType<UIObject>(true))
                {
                    RegistObject(o);
                }
            }
        }

        /// <summary>
        /// Name is UnityEngine Object's name
        /// </summary>
        public UIObject GetObject(string name)
        {
            if (m_objectNameIndex.TryGetValue(name, out var res))
            {
                return res;
            }
            else
            {
                return null;
            }
        }

        public UIObject GetObject(int id)
        {
            if (id < m_objects.Count)
            {
                return m_objects[id];
            }
            else
            {
                return null;
            }
        }
        public T GetObject<T>() where T : class
        {
            foreach(var o in m_objects)
            {
                if (o is T t)
                {
                    return t;
                }
            }
            return null;
        }

        public T GetObject<T>(string name) where T : class
        {
            return GetObject(name) as T;
        }

        public T GetObject<T>(int id) where T : class
        {
            return GetObject(id) as T;
        }

        public void RegistObject(UIObject o)
        {
            m_objects.Add(o);
            m_objectNameIndex.TryAdd(o.name, o);
            ((IUIObjectIDSetTarget)o).SetObjectID(m_objects.Count - 1);
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(UIObjectManager))]
        class UIObjectManagerEditor : Editor
        {
            List<string> m_itoStringCache = new List<string>();

            public override void OnInspectorGUI()
            {
                var target = base.target as UIObjectManager;

                // list view
                if (target.m_objects.Count > 0)
                {
                    int idwidth = Mathf.CeilToInt(Mathf.Log10(target.m_objects.Count)) * 15;
                    var width = GUILayout.Width(idwidth);

                    for (int i = 0; i < target.m_objects.Count; ++i)
                    {
                        var o = target.m_objects[i];
                        GUILayout.BeginHorizontal();

                        // id
                        while (m_itoStringCache.Count <= i)
                        {
                            m_itoStringCache.Add(m_itoStringCache.Count.ToString());
                        }
                        GUILayout.Label(m_itoStringCache[i], width);

                        if (o == null)
                        {
                            EditorGUILayout.LabelField("(miss)");
                        }
                        // check id missmatch?
                        else if (false)
                        {

                        }
                        else
                        {
                            // name
                            if (EditorGUILayout.LinkButton(o.name))
                            {
                                EditorGUIUtility.PingObject(o);
                            }

                        }

                        GUILayout.EndHorizontal();
                    }
                }
            }
        }
#endif
    }
}