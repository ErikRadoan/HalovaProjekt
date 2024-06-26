using System;
using System.Collections.Generic;
using Core;
using School.Core;
using UnityEngine;

namespace Sound
{
    public class SoundPlayer : MonoBehaviour, IRegistrable
    {
        public static SoundPlayer Instance { get; private set; }
        
        [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                ServiceLocator.Register(this);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        void Start()
        {
            
        }
        

        public void PlaySound(string soundName)
        {
            var sound = LoadSound(soundName);
            var audioSource = Camera.main.gameObject.AddComponent<AudioSource>();
            audioSource.clip = sound;
            audioSource.volume = ServiceLocator.Get<GameManager>().volumeSettings;
            audioSource.Play();
            Destroy(audioSource, sound.length);
        }
        
        public AudioSource PlayUntilStopped(string soundName, bool loop = false)
        {
            var audioSource = Camera.main.gameObject.AddComponent<AudioSource>();
            audioSource.clip = LoadSound(soundName);
            audioSource.loop = loop;
            audioSource.volume = ServiceLocator.Get<GameManager>().volumeSettings;
            audioSource.Play();
            return audioSource;
        }
        
        public AudioSource PlayUntilStopped(GameObject destination, string soundName, bool loop = false, float maxDistance = 25f)
        {
            var audioSource = destination.AddComponent<AudioSource>();
            audioSource.clip = LoadSound(soundName);
            audioSource.loop = loop;
            audioSource.volume = ServiceLocator.Get<GameManager>().volumeSettings;
            audioSource.maxDistance = maxDistance;
            audioSource.spatialBlend = 1f;
            audioSource.Play();
            return audioSource;
        }
        
        private AudioClip LoadSound(string soundName)
        {
            foreach (var clip in audioClips)
            {
                if (clip.name == soundName)
                {
                    return clip;
                }
            }
            return null;
        }
    }
}
