#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif
using UnityEngine;
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
            else
            {
                Debug.LogError("Can't Find " + buttonName + ". Link Failed");
            }
        }

#if UNITY_EDITOR
        public static void LinkButtonOnClickPermenant(string buttonName, UnityAction call)
        {
            var b = UIObjectManager.Instance.GetObject<UIButton>(buttonName);
            if (b)
            {
                UnityEventTools.AddPersistentListener(b.OnClick, call);
                EditorUtility.SetDirty(b);
            }
        } 
#endif
    }
}
