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
        private bool _towerPlaced;

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
            
            if (!_currentTarget && _towerPlaced)
            {
                _currentTarget = _enemyDetector.DetectEnemy(transform.position, _towerData.ShootRadius);
            }

            if (_currentTarget && _currentTarget.IsDead)
            {
                _currentTarget = null;
                return;
            }
            
            if (_currentTarget)
            {
                Vector3 directionToTarget = _currentTarget.transform.position - transform.position;
                directionToTarget.y = 0;

                if (directionToTarget != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                    float rotationSpeed = 180f;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }

                if (_attackTimer >= _towerData.ShootDelay)
                {
                    _attackTimer = 0;
                    var pointToHitEnemy = new Vector3(_currentTarget.transform.position.x, _currentTarget.transform.position.y + _towerData.ShootValueOffset, _currentTarget.transform.position.z);
                    Vector3 shootDirection = pointToHitEnemy - _projectileSpawnPoint.transform.position;
                    OnEnemyDetected?.Invoke(shootDirection);

                    if (!_enemyDetector.IsEnemyInRange(transform.position, _towerData.ShootRadius, _currentTarget))
                    {
                        _currentTarget = null;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _towerData.ShootRadius);
        }

        public void SetTowerPlaced(bool value)
        {
            _towerPlaced = value;
        }
    }
}