using System;
using System.Collections;
using Enemy;
using TMPro;
using UnityEngine;

namespace Wallet
{
    public class WalletSystem : MonoBehaviour
    {
        [SerializeField] private EnemyBase _enemyBase;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private float _delay;
        
        private int _moneyValue = 100;
        
        public int MoneyValue => _moneyValue;

        private void OnEnable()
        {
            // foreach (var enemy in _enemyBase.Enemies)
            // {
            //     enemy.OnDeath += OnEnemyDeath;
            // }
        }

        private void OnDisable()
        {
            foreach (var enemy in _enemyBase.Enemies)
            {
                enemy.OnEnemyDied -= OnEnemyDeath;
            }
        }

        public void OnEnemyDeath(int rewardValue)
        {
            AddMoney(rewardValue);
        }

        private void Start()
        {
            StartCoroutine(AddMoneyPerSecond());
        }

        private IEnumerator AddMoneyPerSecond()
        {
            while (true)
            {
                yield return new WaitForSeconds(_delay);
                
                _moneyValue+=5;
                _moneyText.text = $"Money: {_moneyValue}";
            }
        }

        public void SpendMoney(int money)
        {
            if (money <= 0) return;
            
            if (_moneyValue >= money)
            {
                _moneyValue -= money;
            }
        }

        public void AddMoney(int money)
        {
            if (money <= 0) return;
            
            _moneyValue += money;
        }
    }
}

