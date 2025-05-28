using SO;
using UnityEngine;

namespace Sound
{
    public class GameSound : MonoBehaviour
    {
        public static GameSound Instance;
        
        [SerializeField] private GameSoundScriptableObject _gameSoundData;
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _sfxAudioSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            SetToggle("MusicToggle", false);
            SetToggle("SFXToggle", false);
            SetSlider("MusicSlider", 0.5f);
            SetSlider("SFXSlider", 0.75f);

            PlayMusic();
            
            DontDestroyOnLoad(gameObject);
        }

        public void PlayMusic()
        {
            _musicAudioSource.clip = _gameSoundData.MusicPhase1;
            
            _musicAudioSource.mute = GetToggleValue("MusicToggle");
            _musicAudioSource.volume = GetSliderValue("SFXToggle");
            
            _musicAudioSource.Play();
        }

        public void PlaySfx()
        {
            _sfxAudioSource.clip = _gameSoundData.Sfx;
            
            _sfxAudioSource.mute = GetToggleValue("SFXToggle");
            _sfxAudioSource.volume = GetSliderValue("SFXSlider");
            
            _sfxAudioSource.Play();
        }
        
        public void SetToggle(string toggleName, bool value)
        {
            _musicAudioSource.mute = value;
            SaveSoundToggle(toggleName, value ? 1 : 0);
        }

        public void SetSlider(string sliderName, float value)
        {
            _musicAudioSource.volume = value;
            SaveSliderValue(sliderName, value);
        }
        
        public bool GetToggleValue(string toggleName)
        {
            return PlayerPrefs.GetInt(toggleName, 1) == 1;
        }
        
        public float GetSliderValue(string sliderName)
        {
            return PlayerPrefs.GetFloat(sliderName, 1);
        }

        public void SaveSoundToggle(string toggleName, int value)
        {
            PlayerPrefs.SetInt(toggleName, value);
        }

        public void SaveSliderValue(string sliderName, float value)
        {
            PlayerPrefs.SetFloat(sliderName, value);
        }
    }
}