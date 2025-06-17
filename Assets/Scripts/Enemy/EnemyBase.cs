using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SO;
using UnityEngine;
using UnityEngine.Events;
using Wallet;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private WalletSystem _walletSystem;
        [SerializeField] private LevelWavesScriptableObject _wavesData;
        [SerializeField] private Transform _spawnPoint;

        private int _currentWaveEnemiesLeft = 0;
        
        private List<EnemyUnit> _enemies = new List<EnemyUnit>();
        private List<GameObject> _wavesParent = new List<GameObject>();
        
        private Dictionary<int, List<EnemyUnit>> _wavesInactiveEnemies = new Dictionary<int, List<EnemyUnit>>();
        private Dictionary<int, List<EnemyUnit>> _wavesActiveEnemies = new Dictionary<int, List<EnemyUnit>>();
        
        public List<EnemyUnit> Enemies => _enemies;
        
        public event UnityAction<EnemyUnit> OnEnemySpawned;
        public event UnityAction<int, int> OnWaveActivated;
        public event UnityAction<int> OnEnemyLeft;
        public event UnityAction<float, bool> OnPauseAfterWaveStarted;
        public event UnityAction OnAllWavesEnded;
        public event UnityAction OnWaveEnded;

        private void Start()
        {
            for (int i = 0; i < _wavesData.Waves.Count; i++)
            {
                var waveParent = CreateWaveParent(i);
                _wavesParent.Add(waveParent);
            }

            StartCoroutine(ActivateEnemies());
        }

        private GameObject CreateWaveParent(int waveIndex)
        {
            var waveParent = new GameObject();
            waveParent.name = $"EnemiesFromWave_{waveIndex + 1}";
            waveParent.transform.parent = transform;
            
            return waveParent;
        }

        private EnemyUnit SpawnNewEnemy(EnemyUnit enemyUnitTemplate, Transform parent, int waveIndex)
        {
            var newEnemy = Instantiate(enemyUnitTemplate, _spawnPoint.position, _spawnPoint.rotation, parent);
            newEnemy.gameObject.SetActive(false);

            if (!_wavesInactiveEnemies.ContainsKey(waveIndex) && !_wavesActiveEnemies.ContainsKey(waveIndex))
            {
                _wavesInactiveEnemies[waveIndex] = new List<EnemyUnit>();
                _wavesActiveEnemies[waveIndex] = new List<EnemyUnit>();
            }
            
            _wavesInactiveEnemies[waveIndex].Add(newEnemy);
            _wavesActiveEnemies[waveIndex].Add(newEnemy);
            _enemies.Add(newEnemy);
            newEnemy.OnEnemyDied += OnEnemyDied;
            newEnemy.OnEnemyDied += _walletSystem.OnEnemyDeath;
            OnEnemySpawned?.Invoke(newEnemy);

            return newEnemy;
        }

        private IEnumerator ActivateEnemies()
        {
            for (int i = 0; i < _wavesData.Waves.Count; i++)
            {
                if (i > 0)
                {
                    yield return WaitUntilWaveEnemiesDied(_wavesActiveEnemies[i - 1]);
                    yield return PauseBetweenWaves(_wavesData.Waves[i - 1].PauseAfterWave);
                }
                
                var enemiesCount = _wavesData.Waves[i].EnemiesCount;
                var enemyUnit = _wavesData.Waves[i].EnemyUnit;
                var enemiesSpawnRate = _wavesData.Waves[i].EnemiesSpawnRate;

                _currentWaveEnemiesLeft = enemiesCount;
                OnEnemyLeft?.Invoke(enemiesCount);
                
                for (int j = 0; j < enemiesCount; j++)
                {
                    SpawnNewEnemy(enemyUnit, _wavesParent[i].transform, i);
                }

                yield return SpawnEnemiesWithDelay(i, enemiesCount, enemiesSpawnRate, enemyUnit);
            }
            
            yield return WaitUntilAllEnemiesDied();
        }

        private IEnumerator SpawnEnemiesWithDelay(int waveIndex, int enemiesCount, float spawnRate, EnemyUnit enemyUnit)
        {
            var spawned = 0;
            var spawnTimer = 0f;
            
            OnWaveActivated?.Invoke(waveIndex, _wavesData.Waves.Count);

            while (spawned < enemiesCount)
            {
                spawnTimer += Time.deltaTime;

                if (spawnTimer >= spawnRate)
                {
                    spawnTimer = 0f;

                    var enemy = GetInactiveEnemy(waveIndex);

                    if (enemy != null && !enemy.IsDead)
                    {
                        enemy.gameObject.SetActive(true);
                    }
                    else
                    {
                        var newEnemy = SpawnNewEnemy(enemyUnit, _wavesParent[waveIndex].transform, waveIndex);
                        newEnemy.gameObject.SetActive(true);
                    }
                    
                    spawned++;
                }
                
                yield return null;
            }
        }

        private IEnumerator PauseBetweenWaves(float pauseTime)
        {
            var pauseElapsed = 0f;

            while (pauseElapsed < pauseTime)
            {
                pauseElapsed += Time.deltaTime;
                
                var pauseTimeLeft = pauseTime - pauseElapsed;
                
                OnPauseAfterWaveStarted?.Invoke(pauseTimeLeft, true);
                
                yield return null;

                OnPauseAfterWaveStarted?.Invoke(0f, false);
            }
        }

        private IEnumerator WaitUntilAllEnemiesDied()
        {
            while (!_enemies.All(enemy => enemy.IsDead))
            {
                yield return null;
            }
            
            OnAllWavesEnded?.Invoke();
        }

        private IEnumerator WaitUntilWaveEnemiesDied(List<EnemyUnit> enemiesList)
        {
            while (!enemiesList.All(enemy => enemy.IsDead))
            {
                yield return null;
            }
            
            OnWaveEnded?.Invoke();
        }

        private EnemyUnit GetInactiveEnemy(int waveIndex)
        {
            if(!_wavesInactiveEnemies.ContainsKey(waveIndex)) return null;
            
            foreach (var enemy in _wavesInactiveEnemies[waveIndex])
            {
                if (!enemy.gameObject.activeSelf)
                {
                    _wavesInactiveEnemies[waveIndex].Remove(enemy);
                    return enemy;
                }
            }
            
            return null;
        }

        private void OnEnemyDied(int rewardForKill)
        {
            _currentWaveEnemiesLeft--;
            OnEnemyLeft?.Invoke(_currentWaveEnemiesLeft);
        }
    }
}

