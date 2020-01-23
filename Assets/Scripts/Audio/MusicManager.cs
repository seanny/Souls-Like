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
        public int musicType;

        private void Start()
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            combatMusic = Resources.LoadAll<AudioClip>("Audio/Combat");
            relaxMusic = Resources.LoadAll<AudioClip>("Audio/Relax");
        }

        public void PlayCombatMusic()
        {
            if(musicType != 1)
            {
                musicType = 1;
                int rand = Random.Range(0, combatMusic.Length);
                audioSource.clip = combatMusic[rand];
                audioSource.Stop();
                audioSource.Play();
            }
        }

        public void PlayRelaxMusic()
        {
            if(musicType != 2)
            {
                musicType = 2;
                int rand = Random.Range(0, relaxMusic.Length);
                audioSource.clip = relaxMusic[rand];
                audioSource.Stop();
                audioSource.Play();
            }
        }
    }
}
