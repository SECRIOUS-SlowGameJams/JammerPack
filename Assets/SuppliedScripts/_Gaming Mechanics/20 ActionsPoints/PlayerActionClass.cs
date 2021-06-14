using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/*
 * 
 */

namespace SECRIOUS._Gaming_Mechanics
{

    [Serializable]
    public class PlayerAction
    {
        public string actionName;
        public int actionPointCost;
        public UnityEvent action;

        [Space]

        //if this action is controlled by the Keyboard
        public bool executeWithKeys;
        //show this field to add the actionKey
        public KeyCode actionkey;

        //if this is controlled by UI
        public bool executeWithButton;
        //show this field to connect to the respective button
        public ActionTriggerButton button;
        
        public bool canTrigger;
        
        public void AssignButton()
        {
            if (executeWithButton)
                if (button != null)
                {
                    button.AssignAction(this);
                }
        }

        public bool CanTrigger(float playerActionPointsInPool)
        {
            canTrigger = playerActionPointsInPool >= actionPointCost;
            if (button != null)
            {
                button.ToggleInteractability(canTrigger);
            }
            return canTrigger;
        }
    }
}