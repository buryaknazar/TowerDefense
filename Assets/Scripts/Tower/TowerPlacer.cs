using Tiles;
using UI;
using UnityEngine;

namespace Tower
{
    public class TowerPlacer : MonoBehaviour
    {
        [SerializeField] private Tower _towerTemplate;
        [SerializeField] private SpawnButton _spawnButton;
        [SerializeField] private Wallet.Wallet _wallet;
        
        private Tower _activeTower;
        private bool _towerSpawned;

        private void OnEnable()
        {
            _spawnButton.ButtonOnClick += OnButtonClick;
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
            _spawnButton.ButtonOnClick -= OnButtonClick;
        }

        private void OnButtonClick()
        {
            if (_wallet.MoneyValue >= 100 && _activeTower == null)
            {
                _activeTower = Instantiate(_towerTemplate, SpawnPosition(), Quaternion.identity);
                _towerSpawned = true;
                
                _wallet.SpendMoney(100);
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