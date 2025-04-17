using UnityEngine;

namespace Player
{
    public class PlayerBase : MonoBehaviour
    {
        public static PlayerBase Instance;
        
        [SerializeField] private Transform _playerBasePoint;
        
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
        }
    }
}