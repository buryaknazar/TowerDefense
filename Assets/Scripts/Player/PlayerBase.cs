using System;
using General;
using UnityEngine;

namespace Player
{
    public class PlayerBase : MonoBehaviour
    {
        public static PlayerBase Instance;
        
        [SerializeField] private Transform _playerBasePoint;
        [SerializeField] private Transform _healthBarParent;
        [SerializeField] private Transform _healthBarLine;
        [SerializeField] private int _maxHealth;
        
        private HealthBar _playerBaseHealthBar;
        private int _currentHealth;
        
        public Transform PlayerBasePoint => _playerBasePoint;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _playerBaseHealthBar = new HealthBar();
            _currentHealth = _maxHealth;
        }

        private void Update()
        {
            _playerBaseHealthBar.LookAtCamera(_healthBarParent);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _playerBaseHealthBar.ChangeHealthBar(_currentHealth, _maxHealth, _healthBarLine);

            if (_currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}