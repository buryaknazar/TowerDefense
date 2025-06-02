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

        private readonly string _musicToogle = "MusicToggle";
        private readonly string _sfxToogle = "SFXToggle";
        private readonly string _musicSlider = "MusicSlider";
        private readonly string _sfxSlider = "SFXSlider";
        
        public GameSoundScriptableObject GameSoundData => _gameSoundData;

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
            
            SetMusicToggle(true);
            SetSfxToggle(true);
            SetMusicVolume(0.3f);
            SetSfxVolume(0.5f);
            
            PlayMusic(_gameSoundData.MusicPhase1);
            PlaySfx(_gameSoundData.Sfx);
            
            DontDestroyOnLoad(gameObject);
        }

        public void PlayMusic(AudioClip clip)
        {
            _musicAudioSource.clip = clip;
            _musicAudioSource.playOnAwake = true;
            _musicAudioSource.loop = true;
            
            _musicAudioSource.mute = GetToggleValue(_musicToogle);
            _musicAudioSource.volume = GetSliderValue(_musicSlider);
            
            _musicAudioSource.Play();
        }

        public void PlaySfx(AudioClip clip)
        {
            _sfxAudioSource.clip = clip;
            
            _sfxAudioSource.mute = GetToggleValue(_sfxToogle);
            _sfxAudioSource.volume = GetSliderValue(_sfxSlider);
            
            _sfxAudioSource.Play();
        }

        public void SetMusicToggle(bool toggleValue)
        {
            _musicAudioSource.mute = !toggleValue;

            SaveSoundToggle(_musicToogle, toggleValue ? 0 : 1);
        }

        public void SetSfxToggle(bool toggleValue)
        {
            _sfxAudioSource.mute = toggleValue;
            
            SaveSoundToggle(_sfxToogle, toggleValue ? 0 : 1);
        }

        public void SetMusicVolume(float value)
        {
            _musicAudioSource.volume = value;
            
            SaveSliderValue(_musicSlider, value);
        }
        
        public void SetSfxVolume(float value)
        {
            _sfxAudioSource.volume = value;
            
            SaveSliderValue(_sfxSlider, value);
        }
        
        public bool GetToggleValue(string playerPrefsKey)
        {
            return PlayerPrefs.GetInt(playerPrefsKey, 1) == 1;
        }
        
        public float GetSliderValue(string playerPrefsKey)
        {
            return PlayerPrefs.GetFloat(playerPrefsKey, 1);
        }

        private void SaveSoundToggle(string playerPrefsKey, int value)
        {
            PlayerPrefs.SetInt(playerPrefsKey, value);
        }

        private void SaveSliderValue(string playerPrefsKey, float value)
        {
            PlayerPrefs.SetFloat(playerPrefsKey, value);
        }
    }
}