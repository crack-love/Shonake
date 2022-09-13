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
    interface IUIManager
    {
        UIObject GetObjectByName(string name);

        UIObject GetObjectByID(int id);

        T GetObjectByType<T>() where T : class;

#if UNITY_EDITOR
        int AddObjectPersistant(UIObject o);

        bool RemoveObjectPersistant(UIObject o);
#endif
    }

    /// <summary>
    /// Holding UIObjects
    /// </summary>
    class UIManager : MonoBehaviourSingletoneAutoGenerate<UIManager, IUIManager>, IUIManager
    {
        [SerializeField] List<UIObject> m_objects = new List<UIObject>();

        /// <summary>
        /// Name is UnityEngine Object's name
        /// </summary>
        public UIObject GetObjectByName(string name)
        {
            foreach (UIObject o in m_objects)
            {
                if (o && o.name == name)
                {
                    return o;
                }
            }
            return null;
        }

        public UIObject GetObjectByID(int id)
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

        public T GetObjectByType<T>() where T : class
        {
            foreach (UIObject o in m_objects)
            {
                if (o && o is T t)
                {
                    return t;
                }
            }
            return null;
        }

#if UNITY_EDITOR
        public int AddObjectPersistant(UIObject o)
        {
            m_objects.Add(o);
            o.SetUIObjectID(m_objects.Count - 1);
            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(o);
            return m_objects.Count - 1;
        }

        public bool RemoveObjectPersistant(UIObject o)
        {
            for (int i = 0; i < m_objects.Count; ++i)
            {
                if (m_objects[i] == o)
                {
                    m_objects[i] = null;
                    return true;
                }
            }
            EditorUtility.SetDirty(this);
            return false;
        }

        [CustomEditor(typeof(UIManager))]
        class UIObjectManagerEditor : Editor
        {
            new UIManager target;

            private void OnEnable()
            {
                target = (UIManager)base.target;

                InitializeGUIContext();
            }

            List<GUIContent> m_guiContexts = new List<GUIContent>();
            void InitializeGUIContext()
            {
                m_guiContexts.Clear();
                for (int i = 0; i < target.m_objects.Count; ++i)
                {
                    var o = target.m_objects[i];
                    if (o)
                    {
                        m_guiContexts.Add(new GUIContent(o.name));
                    }
                    else
                    {
                        m_guiContexts.Add(new GUIContent("miss"));
                    }
                }
            }

            public override void OnInspectorGUI()
            {
                // list view
                if (target.m_objects.Count <= 0)
                {
                    GUILayout.Label("Has no UIObject");
                }
                else
                {
                    GUILayout.BeginVertical();
                    GUILayout.Label("Objects");
                    for (int i = 0; i < target.m_objects.Count; ++i)
                    {
                        var o = target.m_objects[i];

                        GUILayout.BeginHorizontal();
                        GUILayout.Label(i.ToString(), GUILayout.Width(100));
                        if (o)
                        {
                            if (GUILayout.Button(m_guiContexts[i], GUILayout.ExpandWidth(true)))
                            {
                                EditorGUIUtility.PingObject(o);
                            }
                        }
                        else
                        {
                            var temp = GUI.enabled;
                            GUI.enabled = false;
                            GUILayout.Button(m_guiContexts[i], GUILayout.ExpandWidth(true));
                            GUI.enabled = temp;
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();
                }

                // trim
                // object's id will be reset
                if (GUILayout.Button("Trim and Distinct"))
                {
                    List<UIObject> newList = new List<UIObject>();
                    for (int i = 0; i < target.m_objects.Count; ++i)
                    {
                        var o = target.m_objects[i];

                        if (!o)
                        {
                            continue;
                        }
                        else if (o.UIObjectID != i)
                        {
                            continue;
                        }
                        else
                        { 
                            newList.Add(target.m_objects[i]);
                            target.m_objects[i].SetUIObjectID(newList.Count - 1);
                        }
                    }

                    target.m_objects = newList;
                    InitializeGUIContext();
                }

                if (GUILayout.Button("Regist All Child UIObjects"))
                {
                    foreach (var o in target.GetComponentsInChildren<UIObject>())
                    {
                        if (target.GetObjectByID(o.UIObjectID) != o)
                        {
                            target.AddObjectPersistant(o);
                        }
                    }
                    InitializeGUIContext();
                }
            }
        }
#endif
    }
}