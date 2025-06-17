using System;
using Enemy;
using Player;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class LevelScreen : UIScreen
    {
        [Header("Canvas Groups")]
        [SerializeField] private CanvasGroup _levelCanvasGroup;
        [SerializeField] private CanvasGroup _settingsCanvasGroup;
        
        [Header("Settings")]
        [SerializeField] private SettingsButton _settingsButton;
        
        [Header("Base")]
        [SerializeField] private EnemyBase _enemyBase;
        [SerializeField] private PlayerBase _playerBase;
        
        [Header("UI_Elements")]
        [SerializeField] private TMP_Text _wavesText;
        [SerializeField] private TMP_Text _enemiesText;
        [SerializeField] private TMP_Text _pauseText;

        private void OnEnable()
        {
            _settingsButton.ButtonOnClick += OnSettingsButtonClick;
            _enemyBase.OnWaveActivated += OnWaveActivated;
            _enemyBase.OnEnemyLeft += OnEnemyLeft;
            _enemyBase.OnPauseAfterWaveStarted += OnPauseAfterWaveStarted;
        }

        private void OnDisable()
        {
            _settingsButton.ButtonOnClick -= OnSettingsButtonClick;
            _enemyBase.OnWaveActivated -= OnWaveActivated;
            _enemyBase.OnEnemyLeft -= OnEnemyLeft;
            _enemyBase.OnPauseAfterWaveStarted -= OnPauseAfterWaveStarted;
        }

        private void OnWaveActivated(int waveIndex, int wavesCount)
        {
            _wavesText.text = $"Waves: {waveIndex+1}/{wavesCount}";
        }
        
        private void OnEnemyLeft(int enemiesLeft)
        {
            _enemiesText.text = $"Enemies Left: {enemiesLeft}";
        }
        
        private void OnPauseAfterWaveStarted(float pauseTime, bool pause)
        {
            if (pause)
            {
                _pauseText.gameObject.SetActive(true);
                _pauseText.text = $"Next wave in {(int)pauseTime}";
            }
            else
            {
                _pauseText.gameObject.SetActive(false);
            }
        }

        private void OnSettingsButtonClick()
        {
            ChangeScreenState(_levelCanvasGroup,0f, false, false);
            ChangeScreenState(_settingsCanvasGroup,1f, true, true);
        }
    }
}