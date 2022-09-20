using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shotake
{
    [CreateAssetMenu(fileName ="New Data", menuName ="Shotake/Data/Object Data")]
    class ScriptableObjectData : ScriptableDataCore<UnityEngine.Object>
    {
        [CustomEditor(typeof(ScriptableObjectData))]
        class Editor : ScriptableDataCoreEditor
        {
        }
    }
}