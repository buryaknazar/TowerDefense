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
        [SerializeField] private int _shootDamage;
        [SerializeField] private int _price;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private float _shootValueOffset;
        [SerializeField] private AudioClip _projectileSound;
        [SerializeField] private Sprite _towerIcon;
        
        public float ShootRadius => _shootRadius;
        public float ShootDelay => _shootDelay;
        public float ShootForce => _shootForce;
        public int ShootDamage => _shootDamage;
        public int Price => _price;
        public Projectile Projectile => _projectile;
        public float ShootValueOffset => _shootValueOffset;
        public AudioClip ProjectileSound => _projectileSound;
        public Sprite TowerIcon => _towerIcon;
    }
}