using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shotake
{
    [CreateAssetMenu(fileName ="New Data", menuName ="Shotake/Data/Int Data")]
    class ScriptableIntData : ScriptableDataCore<int>
    {
        [CustomEditor(typeof(ScriptableIntData))]
        class Editor : ScriptableDataCoreEditor
        {
        }
    }
}