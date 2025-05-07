using Tower;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "New Tower Data", menuName = "Tower", order = 0)]
    public class TowerScriptableObject : ScriptableObject
    {
        [SerializeField] private float _shootRadius;
        [SerializeField] private float _shootDelay;
        [SerializeField] private float _shootForce;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private float _shootValueOffset;
        
        public float ShootRadius => _shootRadius;
        public float ShootDelay => _shootDelay;
        public float ShootForce => _shootForce;
        public Projectile Projectile => _projectile;
        public float ShootValueOffset => _shootValueOffset;
    }
}