using System.Collections.Generic;
using UnityCommon;
using UnityEngine;
using Mono.Cecil;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shotake
{

    /// <summary>
    /// Holding UIObjects
    /// </summary>
    class UIManager : MonobehaviourSingletone<UIManager, IUIManager>, IUIManager
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
            if (m_objects.Count < id)
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
            return false;
        }
#endif

#if UNITY_EDITOR
        [CustomEditor(typeof(UIManager))]
        class UIObjectManagerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                var target = (UIManager)this.target;

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
                        if (GUILayout.Button(target.m_objects[i].name, GUILayout.ExpandWidth(true)))
                        {
                            EditorGUIUtility.PingObject(o);
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();
                }

                // trim
                // object's id will be reset
                if (GUILayout.Button("Trim"))
                {
                    List<UIObject> newList = new List<UIObject>();
                    for (int i = 0; i < target.m_objects.Count; ++i)
                    {
                        if (!target.m_objects[i])
                        {
                            continue;
                        }

                        newList.Add(target.m_objects[i]);
                        target.m_objects[i].SetUIObjectID(newList.Count - 1);
                    }

                    target.m_objects = newList;
                }
            }
        }
#endif
    }
}