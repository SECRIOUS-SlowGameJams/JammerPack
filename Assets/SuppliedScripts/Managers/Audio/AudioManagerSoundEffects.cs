using UnityEngine;
using UnityEngine.SceneManagement;

//this script controls the sound effects in all scenes. 
//It is used to play sound effects of gameElements, especially items instantly destroyed before they get to play the audio in their own audiosource.
//Since in 2D mode the spatial origin of the sound's don't matter, this 'hub' is a good substitute.

//Additionally, the GameObject of this script plays specific effects related to events and scene transitions.
//Some of these could be relocated to their respective buttons, but they are gathered here for now for overview purposes.

namespace SECRIOUS.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioManagerSoundEffects : AudioManagerBaseClass
    {
        //these are all sound-effects that are triggered to play. Assign them by hand in the editor. .


        public override void Awake()
        {
            base.Awake();
        }
        public override void PlaySoundEffectPriority(AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }


    }
}
