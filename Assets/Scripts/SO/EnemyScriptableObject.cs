using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy", order = 0)]
    public class EnemyScriptableObject : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private int _rewardForKill;
        [SerializeField] private float _attackDelay;
        
        public int Health => _health;
        public float Speed => _speed;
        public int Damage => _damage;
        public int RewardForKill => _rewardForKill;
        public float AttackDelay => _attackDelay;
    }
}