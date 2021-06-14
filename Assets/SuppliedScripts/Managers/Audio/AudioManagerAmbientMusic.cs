using UnityEngine;
using UnityEngine.SceneManagement;

namespace SECRIOUS.Audio
{
    //this script controls the ambient audio/music in all scenes. 
    //It contains some conditional statements to alter behaviour based on SceneIndex.
    [RequireComponent(typeof(AudioSource))]
    public class AudioManagerAmbientMusic : AudioManagerBaseClass
    {


        public override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += PlayAmbience;
        }

        public void PlayAmbience(Scene scene, LoadSceneMode loadSceneMode)
        {
            FadeInSound(fadeInDuration);
        }

    }
}