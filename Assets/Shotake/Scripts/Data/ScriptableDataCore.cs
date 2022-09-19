using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon;
using UnityEditor;
using UnityEngine;
using UnityReflection;

namespace Shotake
{
    abstract class ScriptableDataCore<T> : ScriptableObject, IData<T>
    {
        [SerializeField] bool m_isWritable;
        [SerializeField] string m_key;
        [SerializeField] T m_value;

        readonly List<IDataChangeListener<T>> m_listeners = new List<IDataChangeListener<T>>(0);
        bool m_isChanged = false;

        public string Key => m_key;

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
                                    l.OnDataChanged(m_key, m_value);
                                }
                            }
                            else
                            {
                                l.OnDataChanged(m_key, m_value);
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
        private void OnDisable()
        {
            var path = AssetDatabase.GetAssetPath(target);
            var name = Path.GetFileNameWithoutExtension(path);
            var key = serializedObject.FindProperty("m_key").stringValue;
            if (name != key)
            {
                AssetDatabase.RenameAsset(path, key);
                EditorGUIUtility.PingObject(target);
            }
        }
    }
}
