using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


namespace SECRIOUS._Gaming_Mechanics
{
    /*
     * This is a component to control the behaviour of UI elements that are connected to actions within an action point system.
     * It sets the interactability of the UI element according to the availability of the action and it triggers the action on click.
     * Add it to the button you want, assign the respective owner/player (ActionPointSystem) to this component (upwards communication) 
     * and declare this component under the respective PlayerAction in the ActionPointSystem script (downwards communication).
     * You don't need to set anything else (no button click events, no player action, etc.). 
     */

    [RequireComponent(typeof(Button))]
    public class ActionTriggerButton : MonoBehaviour
    {
        /// Public Properties
        public ActionPointSystem actionPointsMechanic;

        /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649

        ///  private Fields
        Button button;
        PlayerAction playerAction;
        ///  Unity CallBacks Methods
        void Awake()
        {
            button = GetComponent<Button>();
        }

        //called from PlayerAction
        public void AssignAction(PlayerAction incomingAction)
        {
            playerAction = incomingAction;
            //ties to the ActionPointSystem
            button.onClick.AddListener(RemoteCallAction);
        }

        //called from PlayerAction
        public void ToggleInteractability(bool tof)
        {
            button.interactable = tof;
        }

        //necessary to wrap method in UnityCall (void type)
        public void RemoteCallAction()
        {
            actionPointsMechanic.DoAction(playerAction);
        }

    }

}