using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityReflection;

namespace Shotake
{
    abstract class ScriptableDataCore<T> : ScriptableObject, IData<T>
    {
        [Tooltip("Save/Loadable to IDataProvider")]
        [SerializeField] bool m_isWritable;
        [SerializeField] T m_value;

        readonly List<IDataChangeListener<T>> m_listeners = new List<IDataChangeListener<T>>(0);
        bool m_isChanged = false;

        // object's name is key
        public string Key => name;

        public bool IsNeedWrite => m_isWritable && m_isChanged;

        public T Value
        {
            get => m_value;
            set
            {
                m_value = value;
                m_isChanged = true;

                foreach (var l in m_listeners)
                {
                    try
                    {
                        if (l != null)
                        {
                            if (l is UnityEngine.Object o)
                            {
                                if (o)
                                {
                                    l.OnDataChanged(name, m_value);
                                }
                            }
                            else
                            {
                                l.OnDataChanged(name, m_value);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
        }

        public void ListenDataChanged(IDataChangeListener<T> listener)
        {
            m_listeners.Add(listener);
        }

        public void UnListenDataChanged(IDataChangeListener<T> listener)
        {
            m_listeners.Remove(listener);
        }
    }

    [CustomEditor(typeof(ScriptableDataCore<>))]
    abstract class ScriptableDataCoreEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Key"); 
            GUILayout.Label(target.name);
            GUILayout.EndHorizontal();

            base.OnInspectorGUI();
        }

        //private void OnDisable()
        //{
        //    // rename object name to key
        //    var path = AssetDatabase.GetAssetPath(target);
        //    var name = Path.GetFileNameWithoutExtension(path);
        //    var key = serializedObject.FindProperty("m_key").stringValue;
        //    if (name != key)
        //    {
        //        AssetDatabase.RenameAsset(path, key);
        //        EditorGUIUtility.PingObject(target);
        //    }
        //}
    }
}
