using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuScreen : UIScreen
    {
        [SerializeField] private MainMenuButton _playButton;
        [SerializeField] private MainMenuButton _settingsButton;
        [SerializeField] private MainMenuButton _quitButton;
        
        [SerializeField] private CanvasGroup _mainMenuCanvasGroup;
        [SerializeField] private CanvasGroup _settingsCanvasGroup;

        private void OnEnable()
        {
            _playButton.ButtonOnClick += OnPlayButtonClick;
            _settingsButton.ButtonOnClick += OnSettingsButtonClick;
            _quitButton.ButtonOnClick += OnQuitButtonClick;
        }

        private void OnDisable()
        {
            _playButton.ButtonOnClick -= OnPlayButtonClick;
            _settingsButton.ButtonOnClick -= OnSettingsButtonClick;
            _quitButton.ButtonOnClick -= OnQuitButtonClick;
        }

        private void OnPlayButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void OnSettingsButtonClick()
        {
            ChangeScreenState(_mainMenuCanvasGroup, 0f, false, false);
            ChangeScreenState(_settingsCanvasGroup, 1f, true, true);
        }

        private void OnQuitButtonClick()
        {
            Application.Quit();
        }
    }
}