using System;
using System.Collections.Generic;
using UnityEngine;
using UnityCommon;

namespace Shotake
{
    [Serializable]
    class PatternAndPrefab
    {
        public GeneratePattern Pattern;
        public GameObject Prefab;
    }
    
    [Serializable]
    class StageWave
    {
        // 시작 조건
        // 기간
        // 패턴(범위/거리/각, 적/아이템등 프리펩)
        // 끝 조건

        public float Duration;
        public List<PatternAndPrefab> Patterns;
    };

    [CreateAssetMenu(fileName ="SS", menuName ="Shotake/StageScenario")]
    class StageEntity : ScriptableObject
    {
        [SerializeField] List<StageWave> m_waves = new List<StageWave>();

        public IReadOnlyList<StageWave> Waves => m_waves;
    }
}