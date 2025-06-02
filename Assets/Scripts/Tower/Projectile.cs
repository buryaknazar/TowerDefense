using System;
using SO;
using Unity.VisualScripting;
using UnityEngine;

namespace Tower
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private TowerScriptableObject _towerData;
        
        public TowerScriptableObject TowerData => _towerData;
        public Rigidbody Rigidbody => _rigidbody;

        private void OnCollisionEnter(Collision other)
        {
            gameObject.SetActive(false);
        }
    }
}