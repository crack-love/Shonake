using System.Collections.Generic;
using UnityCommon;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shotake
{
    class UIObjectManager : MonobehaviourSingletone<UIObjectManager>
    {
        List<UIObject> m_objects = new List<UIObject>();
        
        public UIObject GetObject(string name)
        {
            foreach (UIObject o in m_objects)
            {
                if (name == o.UIName)
                {
                    return o;
                }
            }
            return null;
        }

        public T GetObject<T>(string name) where T : UIObject
        {
            foreach(UIObject o in m_objects)
            {
                if (name == o.UIName)
                {
                    return (T)o;
                }
            }
            return null;
        }

        public UIObject GetObject(int idx)
        {
            return m_objects[idx];
        }

        public T GetObject<T>(int idx) where T : UIObject
        {
            return (T)m_objects[idx];
        }

        public int AddObject(UIObject o)
        {
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
        [CustomEditor(typeof(UIObjectManager))]
        class UIObjectManagerEditor : Editor
        {
            List<GUIContent> m_guicontents = new List<GUIContent>();

            public override void OnInspectorGUI()
            {
                var target = (UIObjectManager)this.target;
                m_guicontents.SetSize(target.m_objects.Count);

                if (target.m_objects.Count > 0)
                {
                    GUILayout.BeginVertical();
                    GUILayout.Label("Registed Objects");
                    for (int i = 0; i < target.m_objects.Count; ++i)
                    {
                        var o = target.m_objects[i];

                        // update guicontents
                        if (m_guicontents[i] == null)
                        {
                            m_guicontents[i] = new GUIContent();
                        }
                        if (o.UIName != m_guicontents[i].text)
                        {
                            m_guicontents[i].text = o.UIIndex + " " + o.UIName;
                        }

                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(m_guicontents[i], GUILayout.ExpandWidth(true)))
                        {
                            EditorGUIUtility.PingObject(o);
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();
                }
            }
        }
#endif
    }
}