using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wallet;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemyUnit _enemyTemplate;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _poolSize;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private WalletSystem _walletSystem;
        
        private List<EnemyUnit> _enemies = new List<EnemyUnit>();
        
        public List<EnemyUnit> Enemies => _enemies;

        private void Start()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                var newEnemy = SpawnNewEnemy();
            }

            StartCoroutine(SpawnEnemies());
        }

        private EnemyUnit SpawnNewEnemy()
        {
            var newEnemy = Instantiate(_enemyTemplate, _spawnPoint.position, _spawnPoint.rotation, transform);
            newEnemy.gameObject.SetActive(false);
            _enemies.Add(newEnemy);
            newEnemy.OnDeath += _walletSystem.OnEnemyDeath;

            return newEnemy;
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelay);

                var enemy = GetInactiveEnemy();

                if (enemy != null)
                {
                    ResetEnemy(enemy);
                    enemy.gameObject.SetActive(true);
                    _enemies.Add(enemy);
                }
                else
                {
                    var newEnemy = SpawnNewEnemy();
                    newEnemy.gameObject.SetActive(true);
                }
            }
        }

        private void ResetEnemy(EnemyUnit enemyUnit)
        {
            enemyUnit.transform.position = _spawnPoint.position;
            enemyUnit.transform.rotation = _spawnPoint.rotation;
        }

        private EnemyUnit GetInactiveEnemy()
        {
            foreach (var enemy in _enemies)
            {
                if (!enemy.gameObject.activeSelf)
                {
                    _enemies.Remove(enemy);
                    return enemy;
                }
            }
            
            return null;
        }
    }
}

