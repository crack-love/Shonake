using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif
using UnityEngine.Events;

namespace Shotake
{
    static class UITool
    {
        public static void LinkButtonOnClick(string buttonName, UnityAction call)
        {
            var b = UIObjectManager.Instance.GetObject<UIButton>(buttonName);
            if (b)
            {
                b.OnClick.AddListener(call);
            }
        }

        [Conditional("UNITY_EDITOR")]
        public static void LinkButtonOnClickPermenant(string buttonName, UnityAction call)
        {
            var b = UIObjectManager.Instance.GetObject<UIButton>(buttonName);
            if (b)
            {
                UnityEventTools.AddPersistentListener(b.OnClick, call);
                EditorUtility.SetDirty(b);
            }
        }
    }
}
