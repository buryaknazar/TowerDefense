using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Tower
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        public Rigidbody Rigidbody => _rigidbody;

        private void OnCollisionEnter(Collision other)
        {
            gameObject.SetActive(false);
        }
    }
}