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

        public UIObject GetObject(int idx)
        {
            return m_objects[idx];
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
            public override void OnInspectorGUI()
            {
                var target = (UIObjectManager)this.target;

                if (target.m_objects.Count > 0)
                {
                    GUILayout.BeginVertical();
                    GUILayout.Label("Registed Objects");
                    foreach (UIObject o in target.m_objects)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Index : " + o.UIIndex, GUILayout.Width(100));
                        if (GUILayout.Button(o.UIName, GUILayout.ExpandWidth(true)))
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