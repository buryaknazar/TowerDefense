using System.Collections.Generic;
using Tiles;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Wallet;

namespace Tower
{
    public class TowerPlacer : MonoBehaviour
    {
        public static TowerPlacer Instance;
        
        [SerializeField] private Tower _towerTemplate;
        [SerializeField] private List<SpawnButton> _spawnButtons;
        [SerializeField] private WalletSystem walletSystem;
        
        private Tower _activeTower;
        private bool _towerSpawned;

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

        private void OnEnable()
        {
            foreach (var button in _spawnButtons)
            {
                var towerPrefab = button.TowerToPlace;
                button.ButtonOnClick = () => OnButtonClick(towerPrefab);
            }
        }

        private void Update()
        {
            if (!_towerSpawned) return;
            
            _activeTower.transform.position = SpawnPosition();
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if(hit.transform.TryGetComponent(out TileToPlace tile) && !tile.IsFilled)
                {
                    _activeTower.transform.position = tile.transform.position;

                    if (Input.GetMouseButtonDown(0))
                    {
                        _activeTower.SetTowerPlaced(true);
                        tile.Filled();
                        tile.SetPlacedTower(_activeTower);
                        _activeTower = null;
                        _towerSpawned = false;
                    }
                }
                else
                {
                    _activeTower.transform.position = hit.point;
                }
            }
        }

        private void OnDisable()
        {
            foreach (var button in _spawnButtons)
            {
                button.ButtonOnClick = null;
            }
        }

        private void OnButtonClick(Tower towerPrefab)
        {
            if (walletSystem.MoneyValue >= towerPrefab.TowerData.Price && _activeTower == null)
            {
                _activeTower = Instantiate(towerPrefab, SpawnPosition(), Quaternion.identity);
                _towerSpawned = true;
                _activeTower.SetTowerPlaced(false);
                
                walletSystem.SpendMoney(towerPrefab.TowerData.Price);
            }
        }

        private Vector3 MousePositionWithCamera()
        {
            return new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        }

        private Vector3 SpawnPosition()
        {
            return Camera.main.ScreenToWorldPoint(MousePositionWithCamera());
        }
    }
}