using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace SO
{
    [Serializable]
    public class WaveSettings
    {
        public EnemyUnit EnemyUnit;
        public int EnemiesCount;
        public float EnemiesSpawnRate;
        public float PauseAfterWave;
    }
    
    [CreateAssetMenu(fileName = "New LevelWaves Data", menuName = "Level Waves", order = 0)]
    public class LevelWavesScriptableObject : ScriptableObject
    {
        [SerializeField] private List<WaveSettings> _waves;
        
        public List<WaveSettings> Waves => _waves;
    }
}