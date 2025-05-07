using Enemy;
using UnityEngine;

namespace Enemy
{
    public class EnemyDetector
    {
        public EnemyUnit DetectEnemy(Vector3 position, float radius)
        {
            var enemies = Physics.OverlapSphere(position, radius);

            foreach (var enemy in enemies)
            {
                if (enemy.gameObject.TryGetComponent(out EnemyUnit enemyUnit))
                {
                    return !enemyUnit.IsDead ? enemyUnit : null;
                }
            }
            
            return null;
        }

        public bool IsEnemyInRange(Vector3 position, float radius, EnemyUnit enemy)
        {
            return Vector3.Distance(position, enemy.transform.position) <= radius;
        }
    }
}