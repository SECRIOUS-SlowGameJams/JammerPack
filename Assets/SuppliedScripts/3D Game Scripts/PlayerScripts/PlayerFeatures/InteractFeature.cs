using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//add this to the same hand with reachdetector


    public class InteractFeature : MonoBehaviour
    {

    public KeyCode interactKey;
    /// Public Properties
    public bool canInteract = false;
    

    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


    ///  private Fields
    InteractiveObjectTemplate validReachableTarget;
    PlayerStateController playerStateController;
    ///  Unity CallBacks Methods
    void Awake()
        {
        playerStateController = FindObjectOfType<PlayerStateController>();
        }

        void OnEnable()
        {
        ReachDetector.IItemWithinReachEvent += EnableInteractionPotential;
        ReachDetector.IItemOutOfReachEvent += DisableInteractionPotential;
        }

    void OnDisable()
    {
        ReachDetector.IItemWithinReachEvent -= EnableInteractionPotential;
        ReachDetector.IItemOutOfReachEvent -= DisableInteractionPotential;
    }


        void Update()
        {
        //only enabled 
        InteractWith();
        }

        ///  Private Methods
        ///  
        void EnableInteractionPotential(InteractiveObjectTemplate item)
    {
        canInteract = true;
        validReachableTarget = item;
    }

    void DisableInteractionPotential(InteractiveObjectTemplate item)
    {
        canInteract = false;
        validReachableTarget = null;
    }

        void InteractWith()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(interactKey))
            {
                playerStateController.ToInertState();
                Debug.Log("Interacting with item " + validReachableTarget);
                validReachableTarget.Interact();
            }
        }
    }

    }
