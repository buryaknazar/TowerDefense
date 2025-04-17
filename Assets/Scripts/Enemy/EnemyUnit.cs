using System;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        
        private EnemyMover _mover;
        private Transform _targetPoint;

        private void Awake()
        {
            _mover = new EnemyMover();
            _targetPoint = PlayerBase.Instance.PlayerBasePoint;
        }

        private void Start()
        {
            _mover.Move(_agent, _targetPoint);
        }
    }
}