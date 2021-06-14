using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace SECRIOUS.Audio
{
    [RequireComponent(typeof(Toggle))]
    public class MuteToggle : MonoBehaviour
    {
        Toggle toggle;

        public MixerSlider mixerSlider;
        public float zeroValue;
        
        public bool turnToggleOnToMute;

        float previousVolume;

        // Start is called before the first frame update
        void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(Mute);
            StorePreviousVolumeLvl();
        }

        void Mute(bool tof)
        {
            if (turnToggleOnToMute)
            {
                if (tof)
                    Mute();
                else
                    Unmute();
            }
            else
            {
                if (tof)
                    Unmute();
                else
                    Mute();
            }


        }

        void StorePreviousVolumeLvl()
        {
            previousVolume = mixerSlider.currentValue;
        }

      public  void Mute()
        {
            StorePreviousVolumeLvl();
            mixerSlider.SetVolume(zeroValue);
            mixerSlider.slider.interactable = false;
        }

        public void Unmute()
        {
            mixerSlider.SetVolume(previousVolume);
            mixerSlider.slider.interactable = true;
        }
    }
}