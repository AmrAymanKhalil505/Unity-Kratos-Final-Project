using System;
using UnityEngine;
using UnityEngine.Audio;

namespace BrickWallEntertainment.Managers
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager audioManager;

        public static AudioManager Instance
        {
            get
            {
                return audioManager;
            }
        }

        public SoundData[] sounds;

        void Awake()
        {
            if (audioManager == null)
                audioManager = this;
            else if (audioManager != this)
                Destroy(this.gameObject);
            DontDestroyOnLoad(this.gameObject);

            foreach (SoundData sound in this.sounds)
            {
                AudioSource source = this.gameObject.AddComponent<AudioSource>();
                sound.SetUpSource(source);
            }
        }

        public void Play(string soundName)
        {
            SoundData sound = this.findSoundByName(soundName);
            if (sound != null)
            {
                sound.Play();
            }
        }

        public void Pause(string soundName)
        {
            SoundData sound = this.findSoundByName(soundName);
            if (sound != null)
            {
                sound.Pause();
            }
        }

        public void Stop(string soundName)
        {
            SoundData sound = this.findSoundByName(soundName);
            if (sound != null)
            {
                sound.Stop();
            }
        }

        public void UnPause(string soundName)
        {
            SoundData sound = this.findSoundByName(soundName);
            if (sound != null)
            {
                sound.UnPause();
            }
        }

        public void PauseAll()
        {
            foreach (SoundData sound in this.sounds)
            {
                sound.Pause();
            }
        }

        public void StopAll()
        {
            foreach (SoundData sound in this.sounds)
            {
                sound.Stop();
            }
        }

        public void UnPauseAll()
        {
            foreach (SoundData sound in this.sounds)
            {
                sound.UnPause();
            }
        }

        private SoundData findSoundByName(string soundName)
        {
            SoundData sound = Array.Find(this.sounds, s => s.soundName.Equals(soundName));
            if (sound == null)
            {
                Debug.LogWarning("Sound { " + soundName + " } not found !");
            }
            return sound;
        }
    }
}