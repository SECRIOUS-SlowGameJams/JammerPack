using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    //this is an instant single check, like a checkpoint gate
    public class InteractiveObjectViewValidator : MonoBehaviour
    {

        [SerializeField]
        InteractiveObjectTemplate interactiveObjectDetected;

        //and here (because I am not sure yet which gameplay I prefer 
        public static event Action<InteractiveObjectTemplate> DetectedIItemEvent;
        public static event Action<InteractiveObjectTemplate> ItemDetectionCeasedEvent;


        private void Awake()
        {
            //if you saw an object in the proper layer, check if it has the base component I am interested in 
            PlayerViewPointRaycaster.ObjectDetectedEvent += CheckIfInteractiveObject;
            //if you stopped seeing it, if you had spotted something, declare you lost it
            PlayerViewPointRaycaster.ObjectLostEvent += LostTargetResponse;
        }

        void CheckIfInteractiveObject(GameObject gameObject)
        {
            //if it is 
            if (gameObject.GetComponent<InteractiveObjectTemplate>() != null)
            {
                //load it 
                interactiveObjectDetected = gameObject.GetComponent<InteractiveObjectTemplate>();
                //and let others now I spotted something;
                DetectedIItemEvent?.Invoke(interactiveObjectDetected);
                interactiveObjectDetected.ItemSeenResponse();
                Debug.Log("Saw " + interactiveObjectDetected.name + ".");
            }
            else
            {
                //if it is not an interesting item, do nothing
            }
        }

        //this is conditional!
        void LostTargetResponse(GameObject gameObject)
        {
            //if you had spotted something previously
            if (interactiveObjectDetected != null)
            //now you lost it
            {
            ItemDetectionCeasedEvent?.Invoke(interactiveObjectDetected);
            interactiveObjectDetected.ItemUnSeenResponse();
            Debug.Log("No longer seeing " + interactiveObjectDetected.name + ".");
            interactiveObjectDetected = null;

        }

        }

    }
