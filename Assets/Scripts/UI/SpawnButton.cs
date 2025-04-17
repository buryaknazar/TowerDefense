using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SpawnButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
    
        public event UnityAction ButtonOnClick;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnClick()
        {
            ButtonOnClick?.Invoke();
        }
    }
}


