using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SpawnButton : UIButton
    {
        [SerializeField] private Tower.Tower _towerToPlace;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Image _iconImage;
        
        public Tower.Tower TowerToPlace => _towerToPlace;

        private void Awake()
        {
            _priceText.text = _towerToPlace.TowerData.Price.ToString() + "$";
            _iconImage.sprite = _towerToPlace.TowerData.TowerIcon;
        }
    }
}


