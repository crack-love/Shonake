using System;
using System.Collections.Generic;
using UnityEngine;
using UnityCommon;
using UnityEngine.UI;
using TMPro;

namespace Shotake
{
    // 스테이지 세션 상태
    class StageState : MonoBehaviour
    {
        [SerializeField] int m_wave = 1;

        public int CurrentWaveIndex => m_wave;
    }
}
