using System;
using Enemy;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class LevelScreen : UIScreen
    {
        [SerializeField] private CanvasGroup _levelCanvasGroup;
        [SerializeField] private CanvasGroup _settingsCanvasGroup;
        [SerializeField] private SettingsButton _settingsButton;
        [SerializeField] private EnemyBase _enemyBase;
        [SerializeField] private PlayerBase _playerBase;

        private void OnEnable()
        {
            _settingsButton.ButtonOnClick += OnSettingsButtonClick;
            _enemyBase.OnWaveActivated += OnWaveActivated;
            _enemyBase.OnAllWavesEnded += OnAllWavesEnded;
            _enemyBase.OnEnemyLeft += OnEnemyLeft;
            _enemyBase.OnPauseAfterWaveStarted += OnPauseAfterWaveStarted;
            // _playerBase.OnPlayerBaseDestroyed += OnPlayerBaseDestroyed;
        }

        private void OnDisable()
        {
            _settingsButton.ButtonOnClick -= OnSettingsButtonClick;
            _enemyBase.OnWaveActivated -= OnWaveActivated;
            _enemyBase.OnAllWavesEnded -= OnAllWavesEnded;
            _enemyBase.OnEnemyLeft -= OnEnemyLeft;
            _enemyBase.OnPauseAfterWaveStarted -= OnPauseAfterWaveStarted;
            // _playerBase.OnPlayerBaseDestroyed -= OnPlayerBaseDestroyed;
        }

        private void OnWaveActivated(int waveIndex, int wavesCount)
        {
            
        }
        
        private void OnAllWavesEnded()
        {
            
        }
        
        private void OnEnemyLeft(int enemiesLeft)
        {
            
        }
        
        private void OnPauseAfterWaveStarted(float pauseTime, bool pause)
        {
            
        }
        
        private void OnPlayerBaseDestroyed()
        {
            
        }

        private void OnSettingsButtonClick()
        {
            ChangeScreenState(_levelCanvasGroup,0f, false, false);
            ChangeScreenState(_settingsCanvasGroup,1f, true, true);
        }
    }
}