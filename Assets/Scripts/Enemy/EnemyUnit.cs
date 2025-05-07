using System;
using Player;
using SO;
using Tower;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField] private EnemyScriptableObject _enemyData;
        [SerializeField] private NavMeshAgent _agent;
        
        private EnemyMover _mover;
        private Transform _targetPoint;
        private int _maxHealth;
        private int _currentHealth;
        private float _speed;
        private bool _isDead;
        
        public bool IsDead => _isDead;

        private void Awake()
        {
            _mover = new EnemyMover();
            _targetPoint = PlayerBase.Instance.PlayerBasePoint;

            _maxHealth = _enemyData.Health;
            _currentHealth = _maxHealth;
            _speed = _enemyData.Speed;
            
            _agent.speed = _speed;
        }

        private void OnEnable()
        {
            _mover.Move(_agent, _targetPoint);
        }

        public void ResetEnemyValues()
        {
            _currentHealth = _maxHealth;
            _isDead = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent(out Projectile projectile))
            {
                _currentHealth--;

                if (_currentHealth <= 0)
                {
                    gameObject.SetActive(false);
                    _isDead = true;
                }
            }
        }
    }
}