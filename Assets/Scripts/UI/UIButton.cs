using Sound;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public abstract class UIButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        public UnityAction ButtonOnClick;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        protected virtual void OnClick()
        {
            var sfxClip = GameSound.Instance.GameSoundData.Sfx;
            GameSound.Instance.PlaySfx(sfxClip);
            
            ButtonOnClick?.Invoke();
        }
    }
}