using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace BrickWallEntertainment
{

    [System.Serializable]
    public class Sound
    {
        public string soundName;

        public AudioClip clip;

        public AudioMixerGroup mixerGroup;

        public bool playOnAwake;

        public bool loop;

        [Range(0.0f, 1.0f)]
        public float volume;

        [Range(-3.0f, 3.0f)]
        public float pitch;

        private AudioSource source;

        public void SetUpSource(AudioSource source)
        {
            this.source = source;
            this.source.clip = this.clip;
            this.source.outputAudioMixerGroup = mixerGroup;
            this.source.playOnAwake = this.playOnAwake;
            this.source.loop = this.loop;
            this.source.volume = this.volume;
            this.source.pitch = this.pitch;
        }

        public void Play()
        {
            this.source.Play();
        }

        public void Pause()
        {
            if (this.source.isPlaying)
            {
                this.source.Pause();
            }
        }

        public void Stop()
        {
            if (this.source.isPlaying)
            {
                this.source.Stop();
            }
        }
    }
}