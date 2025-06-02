using System;
using Sound;
using UnityEngine;

namespace Tower
{
    public class TowerSfx : MonoBehaviour
    {
        [SerializeField] private TowerShooter _towerShooter;

        private void OnEnable()
        {
            _towerShooter.OnTowerShooted += OnTowerShooted;
        }

        private void OnDisable()
        {
            _towerShooter.OnTowerShooted -= OnTowerShooted;
        }

        private void OnTowerShooted()
        {
            var sfxClip = _towerShooter.Tower.TowerData.ProjectileSound;
            
            GameSound.Instance.PlaySfx(sfxClip);
        }
    }
}