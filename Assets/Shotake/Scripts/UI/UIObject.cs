using UnityEditor;
using UnityEngine;
using UnityCommon;
using System;
using Codice.CM.Triggers;

namespace Shotake
{
    interface IUIObjectIDSetTarget
    {
        void SetObjectID(int newID);
    }

    [DefaultExecutionOrder(UIObjectManager.ChildExecutionOrder)]
    class UIObject : MonoBehaviour, IUIObjectIDSetTarget
    {
        int m_uIObjectID = 0;

        public int UIObjectID => m_uIObjectID;

        void IUIObjectIDSetTarget.SetObjectID(int newID)
        {
            m_uIObjectID = newID;
        }

        protected void Awake()
        {
            var o = UIObjectManager.Instance.GetObject(m_uIObjectID);
            if (o != this)
            {
                UIObjectManager.Instance.RegistObject(this);
            }
        }
    }
}