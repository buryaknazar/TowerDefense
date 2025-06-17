using Enemy;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class WinLoseScreen : UIScreen
    {
        [Header("Canvas Groups")]
        [SerializeField] private CanvasGroup _winLoseCanvasGroup;
        [SerializeField] private CanvasGroup _levelCanvasGroup;
        
        [Header("UI_Elements")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private UIButton _menuButton;
        [SerializeField] private UIButton _restartButton;
        
        [Header("Base")]
        [SerializeField] private EnemyBase _enemyBase;
        [SerializeField] private PlayerBase _playerBase;
        
        [Header("Variables")]
        [SerializeField] private int _mainMenuSceneIndex;
        
        private void OnEnable()
        {
            _enemyBase.OnAllWavesEnded += OnAllWavesEnded;
            _playerBase.OnPlayerBaseDestroyed += OnPlayerBaseDestroyed;
            _menuButton.ButtonOnClick += OnMenuButtonClick;
            _restartButton.ButtonOnClick += OnRestartButtonClick;
        }

        private void OnDisable()
        {
            _enemyBase.OnAllWavesEnded -= OnAllWavesEnded;
            _playerBase.OnPlayerBaseDestroyed -= OnPlayerBaseDestroyed;
            _menuButton.ButtonOnClick -= OnMenuButtonClick;
            _restartButton.ButtonOnClick -= OnRestartButtonClick;
        }

        private void OnAllWavesEnded()
        {
            _titleText.text = "YOU WON!";
            
            ChangeScreenState(_levelCanvasGroup, 0f, false, false);
            ChangeScreenState(_winLoseCanvasGroup, 1f, true, true);
        }

        private void OnPlayerBaseDestroyed()
        {
            _titleText.text = "YOU LOSE!";
            
            ChangeScreenState(_levelCanvasGroup, 0f, false, false);
            ChangeScreenState(_winLoseCanvasGroup, 1f, true, true);
        }

        private void OnRestartButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnMenuButtonClick()
        {
            SceneManager.LoadScene(_mainMenuSceneIndex);
        }
    }
}