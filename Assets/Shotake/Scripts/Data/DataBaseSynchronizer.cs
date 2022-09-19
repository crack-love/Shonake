using System;
using System.Collections.Generic;
using UnityCommon;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityCommon.Editors;
#endif
using UnityEngine;

namespace Shotake
{
    class DataBaseSynchronizer : MonoBehaviour
#if UNITY_EDITOR
        , IPreprocessBuildWithReport
#endif
    {
        [Tooltip("Assets/{DIR_PATH}")]
        [SerializeField] List<string> m_synchronizedDirs;

#if UNITY_EDITOR
        /// <summary>
        /// IPreprocessBuildWithReport
        /// </summary>
        int IOrderedCallback.callbackOrder => 1000;

        void LoadAllSynchronizedAssets()
        {
            var target = DataBase.Instance as DataBase;
            var assets = AssetDatabaseUtility.LoadAllAssetsAtDir<ScriptableObject>(m_synchronizedDirs.ToArray());
            var comparison = new Comparison<UnityEngine.Object>((x, y) => x.name.CompareTo(y.name));
            var list = target.SerializedDatas;
            var comparer = new ComparisonComparer<UnityEngine.Object>(comparison);
            
            list.Sort(comparison);

            List<UnityEngine.Object> adding = new List<UnityEngine.Object>();
            foreach (var a in assets)
            {
                if (a is IData d)
                {
                    int i = list.BinarySearch(a, comparer);
                    if (i < 0)
                    {
                        adding.Add(a);
                    }
                }
            }

            if (adding.Count > 0)
            {
                foreach(var a in adding)
                {
                    list.Add(a);
                }
                EditorUtility.SetDirty(target);
            }
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            LoadAllSynchronizedAssets();
        }

        [CustomEditor(typeof(DataBaseSynchronizer))]
        class DataBaseEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                var target = base.target as DataBaseSynchronizer;

                base.OnInspectorGUI();

                if (GUILayout.Button("Load Syncronized Assets"))
                {
                    target.LoadAllSynchronizedAssets();
                }
            }
        }
#endif
    }
}
