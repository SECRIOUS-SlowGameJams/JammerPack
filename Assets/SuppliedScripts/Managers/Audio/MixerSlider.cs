using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


/*
 * 
 */
namespace SECRIOUS.Audio
{ 
[RequireComponent(typeof(Slider))]
    public class MixerSlider : MonoBehaviour
    {
    /// Public Properties
    public AudioMixer audioMixer;
    public string exposedMixerVolumeParamName;

        [Range(-80,20)]
    public float minValue;
        [Range(-80, 20)]
        public float maxValue;

        [HideInInspector]
        public float currentValue;


        /// Serialized Fields for Editor
#pragma warning disable 0649
        [SerializeField]
    bool volumeIsControledExternallyToo;


#pragma warning restore 0649

        ///  private Fields
        [HideInInspector]
        public Slider slider;

    ///  Unity CallBacks Methods
    void Awake()
        {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(SetVolume);
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        }

        void OnEnable()
        {
        if (volumeIsControledExternallyToo)
        { SyncSliderWithMixer(); }
    }

        ///  Public Methods
       public void SetVolume(float value)
    {
            currentValue = value;
            audioMixer.SetFloat(exposedMixerVolumeParamName, currentValue);
            //for use when calling from other scripts
            SyncSliderWithMixer();
    }

    public void SyncSliderWithMixer()
    {
        float value;
        audioMixer.GetFloat(exposedMixerVolumeParamName, out value);

        currentValue = value;
        slider.SetValueWithoutNotify(currentValue);
    }

        public void ChangeVoluem(float incrementValue)
        {
            currentValue = Mathf.Clamp(currentValue + incrementValue, minValue, maxValue);
            SetVolume(currentValue);
        }

    }
}