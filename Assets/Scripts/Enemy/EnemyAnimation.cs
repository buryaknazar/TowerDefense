using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyUnit _enemyUnit;

        private readonly int _deathAnimation = Animator.StringToHash("die");
        private readonly int _attackAnimation = Animator.StringToHash("attack");
        private readonly int _hitAnimation = Animator.StringToHash("hit");

        private readonly int _hitLayerIndex = 1;

        private void OnEnable()
        {
            _enemyUnit.OnEnemyDied += OnEnemyDied;
            _enemyUnit.OnEnemyAttack += OnEnemyAttack;
            _enemyUnit.OnEnemyHit += OnEnemyHit;
        }

        private void OnDisable()
        {
            _enemyUnit.OnEnemyDied -= OnEnemyDied;
            _enemyUnit.OnEnemyAttack -= OnEnemyAttack;
            _enemyUnit.OnEnemyHit -= OnEnemyHit;
        }

        private void OnEnemyDied(int value)
        {
            _animator.CrossFade(_deathAnimation, 0.1f);
        }
        
        private void OnEnemyAttack()
        {
            _animator.CrossFade(_attackAnimation, 0.1f);
        }

        private void OnEnemyHit()
        {
            _animator.SetLayerWeight(_hitLayerIndex, 1f);
            _animator.CrossFade(_hitAnimation, 0.1f);
        }

        public void OnDieAnimationFinished()
        {
            _enemyUnit.gameObject.SetActive(false);
        }

        public void OnHitAnimationFinished()
        {
            _animator.SetLayerWeight(_hitLayerIndex, 0f);
        }
    }
}