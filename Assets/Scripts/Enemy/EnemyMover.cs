using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyMover
    {
        public void Move(NavMeshAgent agent, Transform target)
        {
            agent.SetDestination(target.position);
        }
    }
}