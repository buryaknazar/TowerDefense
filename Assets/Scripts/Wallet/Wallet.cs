using System.Collections;
using TMPro;
using UnityEngine;

namespace Wallet
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private float _delay;
        
        private int _moneyValue = 100;
        
        public int MoneyValue => _moneyValue;

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

