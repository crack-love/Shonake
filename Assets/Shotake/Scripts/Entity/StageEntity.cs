using System;
using System.Collections.Generic;
using UnityEngine;
using UnityCommon;

namespace Shotake
{
    [CreateAssetMenu(fileName ="New Stage Entity", menuName ="Shotake/StageEntity")]
    class StageEntity : ScriptableObject
    {
        [SerializeField] float m_viewOrder = 0;
        [SerializeField] string m_viewName = "Default Name";
        [SerializeField] float m_clearRewardScalar = 1f; // 일반 골드와 보석? 연구포인트?
        [SerializeField] Texture2D m_snapShot;
        [SerializeField] List<StageWave> m_waves = new List<StageWave>();        

        public IReadOnlyList<StageWave> Waves => m_waves;
    }
    
    [Serializable]
    class StageWave
    {
        [SerializeField] float m_durationSec = 30;
        [SerializeField] List<PatternAndPrefab> m_patterns = new List<PatternAndPrefab>();

        public float Duration => m_durationSec;
        public List<PatternAndPrefab> Patterns => m_patterns;
    };

    [Serializable]
    class PatternAndPrefab
    {
        [SerializeField] GeneratePattern m_pattern;
        [SerializeField] GameObject m_prefab;
        [SerializeField] int m_countPerSecScalar = 1;

        public GeneratePattern Pattern => m_pattern;
        public GameObject Prefab => m_prefab;
        public int CountPerSecScalar => m_countPerSecScalar;
    }
}