using System;
using Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Tower
{
    public class TowerShooter : MonoBehaviour
    {
        [SerializeField] private Tower _tower;
        
        public Tower Tower => _tower;
        
        public event UnityAction OnTowerShooted;

        private void OnEnable()
        {

            _tower.OnEnemyDetected += OnEnemyDetected;
        }

        private void OnDisable()
        {
            _tower.OnEnemyDetected -= OnEnemyDetected;
        }

        private void OnEnemyDetected(Vector3 shootDirection)
        {
            Shoot(_tower.TowerData.Projectile, shootDirection, _tower.TowerData.ShootForce);
        }

        private void Shoot(Projectile projectile, Vector3 shootDirection, float shootForce)
        {
            var newProjectile = Instantiate(projectile, _tower.ProjectileSpawnPoint.position, Quaternion.LookRotation(shootDirection));
            newProjectile.Rigidbody.AddForce(shootDirection * shootForce, ForceMode.Acceleration);
            
            OnTowerShooted?.Invoke();
        }
    }
}