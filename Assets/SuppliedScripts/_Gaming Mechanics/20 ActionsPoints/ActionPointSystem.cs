using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SECRIOUS.Utilities;
using UnityEngine.Events;
using TMPro;

namespace SECRIOUS._Gaming_Mechanics
{
    /*
     * 
     */

    public class ActionPointSystem : MonoBehaviour
    {
        /// Public Properties
        public ActionPointPool pointPool;
        public PlayerAction[] playerActions;


       //useful if any event is triggered when when you have no action points, e.g. automated end of turn or temporary debuff etc
        public event Action NoActionPointsLeftEvent;
        public event Action ActionPointsResetEvent;

        /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649

        private void Awake()
        {
        
        }

        private void Start()
        {
            pointPool.InitializeActionPointPool();
            //if there is regeneration, initiate it
            SetRegeneration(pointPool.isRegenerating);
            //must be after awake so that the buttons can get hold of their component automatically
            AssignButtons();
        }

        //for any of the actions that are triggered by UI, add the event to that UI element automatically.
        void AssignButtons()
        {
            //conditionals included in data class
            for (int i = 0; i < playerActions.Length; i++)
            {
                playerActions[i].AssignButton();
            }
        }

        //if you want to stop regerenation in runtime, call this with false value;
        public void SetRegeneration(bool tof)
        {
            if (tof)
            {
                InvokeRepeating("RegenActionPoints", Time.deltaTime, 1);
            }
            else
            {
                CancelInvoke();
            }
        }

        void RegenActionPoints()
        {
            pointPool.AddActionPoints(pointPool.actionPointsRestoredPerSecond);
        }


        //listen to player input during update
        public void Update()
        {
            TriggerActionUsingKeys();
        }

        ///  Public Methods
        ///  
        void TriggerActionUsingKeys()
        {
            //if the player hits any of the keys tied to an action
            foreach (var item in playerActions)
            {
                if (Input.GetKeyDown(item.actionkey))
                {
                    //if I have enough action points left for that action (also calculated after each spending, this is for the first time per turn)
                    if (item.CanTrigger(pointPool.actionPointsLeftThisTurn))
                    {
                        //do that action
                        DoAction(item);
                    }
                    else
                    { 
                        //usually nothing happens, add anything you want, there is get a debug log fyi
                        Debug.Log("Not enough actions points left"); }
                }
            }
        }


        public void DoAction(PlayerAction playerAction)
        {
            //remove the point cost
            pointPool.RemoveActionPoints(playerAction.actionPointCost);
            // I can never get in negative, but if I am exactly at 0, declare it
            CheckIfOutOfActionPoints();
            //and invoke the respective action
            playerAction.action.Invoke();
            //update my state
            UpdateActionState();

            void CheckIfOutOfActionPoints()
            {
                if (pointPool.actionPointsLeftThisTurn == 0)
                {
                    Debug.Log("Player " + this.gameObject + "is out of points.");
                    NoActionPointsLeftEvent?.Invoke();
                }
            }
        }

        public void ResetPoints()
        {
            pointPool.RefillActionPointPool();
            Debug.Log("Player " + this.gameObject + "refilled the point pool.");
            ActionPointsResetEvent?.Invoke();
            UpdateActionState();
        }

        
       ///  Private Methods

        void UpdateActionState()
        {
            foreach (var item in playerActions)
            {
                //this sets both the flag (which I dont use now) and the UI controls
                item.CanTrigger(pointPool.actionPointsLeftThisTurn);
            }
            //for use with the UI diplayer
            SendMessage("OnStateUpdate", SendMessageOptions.DontRequireReceiver);
        }

    }

    [Serializable]
    public class ActionPointPool
        {

        [Header("Design data")]
        public int actionPointPoolSize;
        public bool isRegenerating;
        public int actionPointsRestoredPerSecond;

        [Header("RunTime Data")]
        public int actionPointsLeftThisTurn;




        public void InitializeActionPointPool()
        {
            actionPointsLeftThisTurn = actionPointPoolSize;
        }


        public void AddActionPoints(int actionPointsRestored)
        {
            actionPointsLeftThisTurn += actionPointsRestored;
            actionPointsLeftThisTurn = Mathf.Clamp(actionPointsLeftThisTurn, 0, actionPointPoolSize);
        }


        public void RemoveActionPoints(int actionPointCost)
        {
            actionPointsLeftThisTurn -= actionPointCost;
            actionPointsLeftThisTurn = Mathf.Clamp(actionPointsLeftThisTurn, 0, actionPointPoolSize);
        }

        public void RefillActionPointPool()
        {
            actionPointsLeftThisTurn = actionPointPoolSize;
        }


        public void EmptyActionPointPool()
        {
            actionPointsLeftThisTurn = 0;
        }


        public void ChangePoolSize(int additionOrRemoval)
        {
            actionPointPoolSize += additionOrRemoval;
        }
        

    }

}
