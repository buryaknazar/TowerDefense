using System;
using Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsScreen : UIScreen
    {
        [SerializeField] private MainMenuButton _backButton;
        [SerializeField] private CanvasGroup _mainMenuCanvasGroup;
        [SerializeField] private CanvasGroup _settingsCanvasGroup;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _sfxToggle;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;

        private void Start()
        {
            _musicToggle.isOn = !GameSound.Instance.GetToggleValue(_musicToggle.name);
            _sfxToggle.isOn = !GameSound.Instance.GetToggleValue(_sfxToggle.name);
            _musicVolumeSlider.value = GameSound.Instance.GetSliderValue(_musicVolumeSlider.name);
            _sfxVolumeSlider.value = GameSound.Instance.GetSliderValue(_sfxVolumeSlider.name);
        }

        private void OnEnable()
        {
            _backButton.ButtonOnClick += OnBackButtonClick;
            
            _musicToggle.onValueChanged.AddListener(_ =>
            {
                GameSound.Instance.SetMusicToggle(_musicToggle.isOn);
            });
            
            _sfxToggle.onValueChanged.AddListener(_ =>
            {
                GameSound.Instance.SetSfxToggle(_sfxToggle.isOn);
            });
            
            
            _musicVolumeSlider.onValueChanged.AddListener(_ =>
            {
                GameSound.Instance.SetMusicVolume(_musicVolumeSlider.value);
            });
            
            _sfxVolumeSlider.onValueChanged.AddListener(_ =>
            {
                GameSound.Instance.SetSfxVolume(_sfxVolumeSlider.value);
            });
        }

        private void OnDisable()
        {
            _backButton.ButtonOnClick -= OnBackButtonClick;
            _musicToggle.onValueChanged.RemoveAllListeners();
            _sfxToggle.onValueChanged.RemoveAllListeners();
        }

        private void OnBackButtonClick()
        {
            ChangeScreenState(_settingsCanvasGroup, 0f, false, false);
            ChangeScreenState(_mainMenuCanvasGroup, 1f, true, true);
        }
    }
}