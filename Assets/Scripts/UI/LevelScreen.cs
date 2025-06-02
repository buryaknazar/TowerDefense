using System;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class LevelScreen : UIScreen
    {
        [SerializeField] private CanvasGroup _levelCanvasGroup;
        [SerializeField] private CanvasGroup _settingsCanvasGroup;
        [SerializeField] private SettingsButton _settingsButton;

        private void OnEnable()
        {
            _settingsButton.ButtonOnClick += OnSettingsButtonClick;
        }

        private void OnDisable()
        {
            _settingsButton.ButtonOnClick -= OnSettingsButtonClick;
        }

        private void OnSettingsButtonClick()
        {
            ChangeScreenState(_levelCanvasGroup,0f, false, false);
            ChangeScreenState(_settingsCanvasGroup,1f, true, true);
        }
    }
}