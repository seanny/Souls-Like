using UnityEngine;

namespace SoulsLike
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager instance { get; private set; }
        public AudioSource audioSource;
        public AudioClip[] combatMusic;
        public AudioClip[] relaxMusic;

        private void Start()
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            combatMusic = Resources.LoadAll<AudioClip>("Audio/Combat");
            relaxMusic = Resources.LoadAll<AudioClip>("Audio/Relax");
        }

        /// <summary>
        /// Play Combat Music (found in Resources/Audio/Combat)
        /// </summary>
        public void PlayCombatMusic()
        {
            int rand = Random.Range(0, combatMusic.Length);
            audioSource.clip = combatMusic[rand];
            audioSource.Stop();
            audioSource.Play();
        }

        /// <summary>
        /// Play Relaxing Music (Found in Resources/Audio/Relax)
        /// </summary>
        public void PlayRelaxMusic()
        {
            int rand = Random.Range(0, relaxMusic.Length);
            audioSource.clip = relaxMusic[rand];
            audioSource.Stop();
            audioSource.Play();
        }
    }
}
