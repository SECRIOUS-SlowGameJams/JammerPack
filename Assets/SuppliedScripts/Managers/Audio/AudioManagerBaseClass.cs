using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SECRIOUS.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioManagerBaseClass : MonoBehaviour
    {
        public AudioMixerGroup audioMixerGroupMusic;

        public MixerSlider slider;

        //this is to fade IN sound, in seconds. 
        public float fadeInDuration;
        public float fadeOutDuration;


        public bool controlVolumeWithKeys;

        public KeyCode volumeUpKey;
        public KeyCode volumeDownKey;
        public float volumeChangeIncrement;

        public bool controlVolumeWithMouseWheel;
        public float mouseWheelSensitivity = 0.1f;


        public MuteToggle muteToggle;

        //this variable is to control the change of volume via the mouseScrollWheel / keys


        protected AudioSource audioSource;

        //for mute/unmute buttons
        float previousVolume;
        bool isMuted;


        public UnityAction<float> ReceiveInfoFromSliderEvent;

        public virtual void Awake()
        {
            //no checks if null, since there is the 'require' attribute on the class
            audioSource = GetComponent<AudioSource>();
            //to minimize working in editor
            audioSource.playOnAwake = false;

            //this is to accomodate an expansion feature with fade in and out effects on sound using Mixers. 
            audioSource.outputAudioMixerGroup = audioMixerGroupMusic;

       }

        private void Update()
        {
            if (controlVolumeWithMouseWheel)
            {
                VolumeControlWithMouseWheel();
            }

            if (controlVolumeWithKeys)
            {
                VolumeControlWithKeys();
            }
        }


        #region userMethods
        //I am wrapping up the default play method to include a check.
        public void PlayIfNotPlaying()
        {
            if (audioSource.isPlaying != true)
                audioSource.Play();
        }


        //softer stop than default function.
        void SmoothStopPlaying()
        {
            FadeOutSound(fadeOutDuration);
        }

        public virtual void PlaySoundEffectPriority(AudioClip audioClip)
        {
            if (audioSource.isPlaying)
            {
                SmoothStopPlaying();
            }
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        //pause if playing and unpause if paused.
        public void TogglePause()
        {
            if (audioSource.isPlaying)
            { audioSource.Pause(); }
            else
            { audioSource.UnPause(); }

        }

        public void Mute()
        {
            muteToggle.Mute();
            isMuted = true;
        }

        public void UnMute()
        {
            isMuted = false;
            muteToggle.Unmute();
        }

        public void ToggleMute()
        {
            if (!isMuted)
            { Mute(); }
            else
            { UnMute(); }
        }

        #endregion

        #region Slider
        //base: change volume by some value
        public void ChangeVolume(float value)
        {
                 slider.ChangeVoluem(value);
        }

    
        public void VolumeControlWithKeys()
        {
            if (Input.GetKeyDown(volumeUpKey))
            {
                ChangeVolume(volumeChangeIncrement);
            }
            else if (Input.GetKeyDown(volumeDownKey))
            {
                ChangeVolume(-volumeChangeIncrement);
            }
        }

        public void VolumeControlWithMouseWheel()
        {
            if (Input.mouseScrollDelta.y != 0)
            ChangeVolume(Input.mouseScrollDelta.y * mouseWheelSensitivity);
        }

        #endregion

        #region  FadeEffects
        public void FadeInSound(float duration)
        {
            audioSource.Play();
            audioSource.volume = 0;
            StartCoroutine(StartFade(audioSource, duration, 1));
        }

        public virtual void FadeOutSound(float duration)
        {
            StartCoroutine(StartFade(audioSource, duration, 0));
        }

        public Coroutine FadeOutSoundCor(float duration)
        {
            return StartCoroutine(StartFade(audioSource, duration, 0));
        }



        IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;

            float start = audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }

        }

        #endregion

    }


}