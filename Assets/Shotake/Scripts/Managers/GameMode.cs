﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shotake
{
    [DefaultExecutionOrder(GameModeManager.ChildExecutionOrder)]
    abstract class GameMode : MonoBehaviour
    {
        bool m_isEnabled = false;

        public bool IsModeEnabled
        {
            get => m_isEnabled;
        }

        public virtual IEnumerator EnableMode()
        {
            m_isEnabled = true;
            return null;
        }

        public virtual IEnumerator DisableMode() 
        {
            m_isEnabled = false;
            return null; 
        }

        bool m_isRegisted = false;

        public void SetRegisted()
        {
            m_isRegisted = true;
        }

        protected void Awake()
        {
#if UNITY_EDITOR
            // ensure manager to be instanced
            var _ = GameModeManager.Instance;
            bool __ = _.Equals(null);

            if (!m_isRegisted)
            {
                Debug.LogError("Found Unrgisted Mode " + GetType().Name + " (It will be destroyed)");
                Destroy(gameObject);
            }
#endif
        }
    }
}