using System;
using Enemy;
using UnityEngine;

namespace Tower
{
    public class TowerShooter : MonoBehaviour
    {
        [SerializeField] private Tower _tower;

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
            var newProjectile = Instantiate(projectile, _tower.ProjectileSpawnPoint.position, Quaternion.identity);
            newProjectile.Rigidbody.AddForce(shootDirection * shootForce, ForceMode.Acceleration);
        }
    }
}