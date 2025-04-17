using System;
using Enemy;
using SO;
using UnityEngine;
using UnityEngine.Events;

namespace Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private TowerScriptableObject _towerData;
        [SerializeField] private Transform _projectileSpawnPoint;

        private EnemyDetector _enemyDetector;
        private EnemyUnit _currentTarget;

        private float _attackTimer;

        public event UnityAction<Vector3> OnEnemyDetected;
        
        public TowerScriptableObject TowerData => _towerData;
        public Transform ProjectileSpawnPoint => _projectileSpawnPoint;

        private void Awake()
        {
            _enemyDetector = new EnemyDetector();
        }

        private void Update()
        {
            _attackTimer += Time.deltaTime;
            
            if (!_currentTarget)
            {
                _currentTarget = _enemyDetector.DetectEnemy(transform.position, _towerData.ShootRadius);
            }
            else if (_currentTarget && _attackTimer >= _towerData.ShootDelay)
            {
                _attackTimer = 0;
                
                var shootDirection = _currentTarget.transform.position - _projectileSpawnPoint.transform.position;
                
                OnEnemyDetected?.Invoke(shootDirection);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _towerData.ShootRadius);
        }
    }
}