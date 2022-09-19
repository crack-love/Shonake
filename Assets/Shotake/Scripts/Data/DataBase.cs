using System;
using System.Collections.Generic;
using UnityCommon;
#if UNITY_EDITOR
using UnityCommon.Editors;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif
using UnityEngine;

namespace Shotake
{
    interface IDataBase
    {
        void AddData(IData data);

        T GetData<T>(string key);

        void SetData<T>(string key, T value);

        void ListenDataChange<T>(string key, IDataChangeListener<T> listener);

        void UnListenDataChange<T>(string key, IDataChangeListener<T> listener);

        void Save(IDataProvider p, bool forceWriteAll);

        void Load(IDataProvider p);
    }

    class DataBase : MonoBehaviourSingletoneAutoGenerate<DataBase, IDataBase>, IDataBase
    {
        [SerializeField] List<UnityEngine.Object> m_serializedData;

        readonly Dictionary<string, IData> m_datas = new Dictionary<string, IData>();

        internal List<UnityEngine.Object> SerializedDatas => m_serializedData;

        void LogDataLoadError(string key)
        {
            Debug.LogError("Get Data " + key + " Fail");
        }
        
        void LogDataDuplicatedError(string key)
        {
            Debug.LogError("Data Key Duplicated " + key);
        }

        public void AddData(IData data)
        {
            if (data != null)
            {
                if (!m_datas.TryAdd(data.Key, data))
                {
                    LogDataDuplicatedError(data.Key);
                }
            }
        }

        public T GetData<T>(string key)
        {
            if (m_datas.TryGetValue(key, out IData o))
            {
                if (o is IData<T> d)
                {
                    return d.Value;
                }
            }

            LogDataLoadError(key);
            return default;
        }

        public void ListenDataChange<T>(string key, IDataChangeListener<T> listener)
        {
            if (m_datas.TryGetValue(key, out IData o))
            {
                if (o is IData<T> d)
                {
                    d.ListenDataChanged(listener);
                    return;
                }
            }

            LogDataLoadError(key);
        }

        public void Load(IDataProvider p)
        {
            foreach(var pair in m_datas)
            {
                p.LoadData(pair.Value);
            }
        }

        public void Save(IDataProvider p, bool forceWriteAll)
        {
            foreach (var pair in m_datas)
            {
                if (forceWriteAll || pair.Value.IsNeedWrite)
                {
                    p.SaveData(pair.Value);
                }
            }
        }

        public void SetData<T>(string key, T value)
        {
            if (m_datas.TryGetValue(key, out IData o))
            {
                if (o is IData<T> d)
                {
                    d.Value = value;
                    return;
                }
            }

            LogDataLoadError(key);
        }

        public void UnListenDataChange<T>(string key, IDataChangeListener<T> listener)
        {
            if (m_datas.TryGetValue(key, out IData o))
            {
                if (o is IData<T> d)
                {
                    d.UnListenDataChanged(listener);
                    return;
                }
            }

            LogDataLoadError(key);
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);

            foreach(var o in m_serializedData)
            {
                if (o is IData d)
                {
                    if (!m_datas.TryAdd(d.Key, d))
                    {
                        LogDataDuplicatedError(d.Key);
                    }
                }
            }
        }
    }
}