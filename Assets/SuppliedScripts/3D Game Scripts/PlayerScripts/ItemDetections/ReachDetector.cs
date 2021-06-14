using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

    public class ReachDetector : MonoBehaviour
{

#pragma warning disable 0649
    [SerializeField]
    int reach;
    [SerializeField]
    bool isEnabled;


#pragma warning restore 0649

    public static event Action<InteractiveObjectTemplate> IItemOutOfReachEvent;
    public static event Action<InteractiveObjectTemplate> IItemWithinReachEvent;

    bool isInRange;
    InteractiveObjectTemplate potentialTarget;
    [SerializeField]
    InteractiveObjectTemplate reachableTarget;

    private void Awake()
    {
        //i am only interested in reach, IF I am looking at something
        InteractiveObjectViewValidator.DetectedIItemEvent += EnableReachDetection;
        //if I am NOT looking at something, I should stop further search AND unload anything I was targetting
        InteractiveObjectViewValidator.ItemDetectionCeasedEvent += DisableReachDetection;

    }

    void EnableReachDetection(InteractiveObjectTemplate interactiveObjectTemplate)
    {
        isEnabled = true;
        potentialTarget = interactiveObjectTemplate;
    }

    void DisableReachDetection(InteractiveObjectTemplate interactiveObjectTemplate)
    {
        //no longer tracking
        isEnabled = false;
        potentialTarget = null;
        //if I had something, I lost it now
        LostReachableTarget();
    }

    private void Update()
    {
        EnableReachDetection();
    }


    void EnableReachDetection()
    {
        //if an event flicked this switch, start tracking distance
        if (isEnabled)
        {
            //check if its close enough
            ReachDetection();
        }
    }


    public void ReachDetection()
    {
        //if its within reach
        if (ReachCheck(potentialTarget.transform))
        {
            reachableTarget = potentialTarget;
            //call it out
            IItemWithinReachEvent?.Invoke(reachableTarget);
         
            if (!isInRange)
            {
                reachableTarget.ItemReachedResponse();
                Debug.Log("Can reach " + reachableTarget.name + ".");
                isInRange = true;
            }
        }
        else
        //if it is not
        {
            //it s either not close enough yet, 
            //do nothign
            //or 
            //I m no longer close enough
            LostReachableTarget();
        }
    }

    void LostReachableTarget()
    {
        //if I had something in focus
        if (reachableTarget != null)
        {
            //call out I lost it
            IItemOutOfReachEvent?.Invoke(reachableTarget);
            reachableTarget.ItemOutOfReachResponse();
            isInRange = false;
            Debug.Log("Can no longer reach " + reachableTarget.name + ".");
            //and unload it 
            reachableTarget = null;
        }
        //if I did nt have anything yet, no need to do anything
    }

    public bool ReachCheck(Transform itemTransform)
    {
        if (Vector3.Distance(this.transform.position, itemTransform.position) < reach)
        { return true; }
        else
        { return false; }
    }


}
