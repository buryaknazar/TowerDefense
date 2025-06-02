using System;
using System.Collections;
using General;
using Player;
using SO;
using Tower;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField] private EnemyScriptableObject _enemyData;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _healthBarParent;
        [SerializeField] private Transform _healthBarLine;

        private HealthBar _enemyHealthBar;
        
        private EnemyMover _mover;
        private Transform _targetPoint;
        private int _maxHealth;
        private int _currentHealth;
        private float _speed;
        private bool _isDead;
        
        public bool IsDead => _isDead;
        
        public event UnityAction<int> OnEnemyDied;
        public event UnityAction OnEnemyAttack;
        public event UnityAction OnEnemyHit;

        private void Awake()
        {
            _mover = new EnemyMover();
            _enemyHealthBar = new HealthBar();
            _targetPoint = PlayerBase.Instance.PlayerBasePoint;

            _maxHealth = _enemyData.Health;
            _currentHealth = _maxHealth;
            _speed = _enemyData.Speed;
            
            _agent.speed = _speed;
        }

        private void OnEnable()
        {
            _mover.Move(_agent, _targetPoint);
            ResetEnemyValues();
            _enemyHealthBar.ChangeHealthBar(_currentHealth, _maxHealth, _healthBarLine);
        }

        private void Update()
        {
            _enemyHealthBar.LookAtCamera(_healthBarParent);
        }

        public void ResetEnemyValues()
        {
            _currentHealth = _maxHealth;
            _isDead = false;
        }

        private void Die()
        {
            _isDead = true;
            StopAllCoroutines();
            OnEnemyDied?.Invoke(_enemyData.RewardForKill);
        }

        private void TakeDamage(int damage)
        {
            _currentHealth -= damage;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent(out Projectile projectile))
            {
                OnEnemyHit?.Invoke();
                TakeDamage(projectile.TowerData.ShootDamage);
                _enemyHealthBar.ChangeHealthBar(_currentHealth, _maxHealth, _healthBarLine);

                if (_currentHealth <= 0)
                {
                    Die();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBase playerBase))
            {
                if (playerBase.gameObject.activeSelf)
                {
                    StartCoroutine(Attack(playerBase));
                }
                else
                {
                    StopCoroutine(Attack(playerBase));
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerBase playerBase))
            {
                StopCoroutine(Attack(playerBase));
            }
        }

        private IEnumerator Attack(PlayerBase playerBase)
        {
            while (true)
            {
                OnEnemyAttack?.Invoke();
                playerBase.TakeDamage(_enemyData.Damage);
                yield return new WaitForSeconds(_enemyData.AttackDelay);
            }
        }
    }
}