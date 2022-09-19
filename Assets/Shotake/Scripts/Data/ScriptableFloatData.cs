using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Shotake
{
    [CreateAssetMenu(fileName ="New Data", menuName ="Shotake/Data/Float Data")]
    class ScriptableFloatData : ScriptableDataCore<float>
    {
        [CustomEditor(typeof(ScriptableFloatData))]
        class Editor : ScriptableDataCoreEditor
        {
        }
    }
}