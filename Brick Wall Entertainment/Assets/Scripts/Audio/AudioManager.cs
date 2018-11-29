using System;
using UnityEngine;

namespace BrickWallEntertainment.Managers
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;

        public static AudioManager Instance
        {
            get
            {
                return instance;
            }
        }

        public Sound[] sounds;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(this.gameObject);
            DontDestroyOnLoad(this.gameObject);

            foreach (Sound sound in this.sounds)
            {
                AudioSource source = this.gameObject.AddComponent<AudioSource>();
                sound.SetUpSource(source);
            }
        }

        public void Play(string soundName)
        {
            Sound sound = this.findSoundByName(soundName);
            if (sound != null)
            {
                sound.Play();
            }
        }

        public void Pause(string soundName)
        {
            Sound sound = this.findSoundByName(soundName);
            if (sound != null)
            {
                sound.Pause();
            }
        }

        public void Stop(string soundName)
        {
            Sound sound = this.findSoundByName(soundName);
            if (sound != null)
            {
                sound.Stop();
            }
        }

        public void PauseAll()
        {
            foreach (Sound sound in this.sounds)
            {
                sound.Pause();
            }
        }

        public void StopAll()
        {
            foreach (Sound sound in this.sounds)
            {
                sound.Stop();
            }
        }

        private Sound findSoundByName(string soundName)
        {
            Sound sound = Array.Find(this.sounds, s => s.soundName.Equals(soundName));
            if (sound == null)
            {
                Debug.LogWarning("Sound { " + soundName + " } not found !");
            }
            return sound;
        }
    }
}