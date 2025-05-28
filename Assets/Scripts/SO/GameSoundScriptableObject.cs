using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "New Game Sound", menuName = "Game Sound", order = 0)]
    public class GameSoundScriptableObject : ScriptableObject
    {
        [SerializeField] private AudioClip _musicPhase1;
        [SerializeField] private AudioClip _musicPhase2;
        [SerializeField] private AudioClip _musicPhase3;

        [SerializeField] private AudioClip _sfx;
        
        public AudioClip MusicPhase1 => _musicPhase1;
        public AudioClip MusicPhase2 => _musicPhase2;
        public AudioClip MusicPhase3 => _musicPhase3;
        
        public AudioClip Sfx => _sfx;
    }
}