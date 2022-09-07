using System.Collections.Generic;
using UnityCommon;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shotake
{
    class UIManager : MonobehaviourSingletone<UIManager>
    {
        List<UIObject> m_objects = new List<UIObject>();

        /// <summary>
        /// Name is UnityEngine Object's name
        /// </summary>
        public UIObject GetObjectByName(string name)
        {
            foreach (UIObject o in m_objects)
            {
                if (o.name == name)
                {
                    return o;
                }
            }
            return null;
        }

        public UIObject GetObjectByIndex(int index)
        {
            if (m_objects.Count < index)
            {
                return m_objects[index];
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
                if (o is T t)
                {
                    return t;
                }
            }
            return null;
        }

        public int AddObject(UIObject o)
        {
            Debug.Log(o.name + " Added");
            m_objects.Add(o);
            return m_objects.Count - 1;
        }

        public bool RemoveObject(UIObject o)
        {
            return m_objects.Remove(o);
        }

        public void RemoveObject(int idx)
        {
            m_objects.RemoveAt(idx);
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(UIManager))]
        class UIObjectManagerEditor : Editor
        {
            List<GUIContent> m_guicontents;

            private void OnEnable()
            {
                m_guicontents = new List<GUIContent>();
                var target = base.target as UIManager;
                if (target && target.m_objects != null)
                {
                    for (int i = 0; i < target.m_objects.Count; ++i)
                    {
                        m_guicontents.Add(new GUIContent(i + " " + target.m_objects[i].name));
                    }
                }
            }

            public override void OnInspectorGUI()
            {
                var target = (UIManager)this.target;

                if (target.m_objects.Count > 0)
                {
                    GUILayout.BeginVertical();
                    GUILayout.Label("Objects");
                    for (int i = 0; i < target.m_objects.Count; ++i)
                    {
                        var o = target.m_objects[i];

                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(m_guicontents[i], GUILayout.ExpandWidth(true)))
                        {
                            EditorGUIUtility.PingObject(o);
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();
                }
                else
                {
                    GUILayout.Label("Has no UIObject");
                }
            }
        }
#endif
    }
}