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
                    return enemyUnit;
                }
            }
            
            return null;
        }
    }
}