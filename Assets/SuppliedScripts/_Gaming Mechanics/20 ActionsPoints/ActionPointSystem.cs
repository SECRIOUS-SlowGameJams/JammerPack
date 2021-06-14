using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SECRIOUS.Utilities;
using UnityEngine.Events;
using TMPro;

/*
 * 
 */

public class ActionPointSystem : MonoBehaviour
{
    /// Public Properties
    [Header("Design data")]
    public PlayerAction[] actions;
    public int actionPointsPerTurn;
    public bool isRegenerating;
    public int actionPointsRestoredPerSecond;


    [Header("RunTime Data")]
    public int actionPointsLeftThisTurn;


  
    [Header("UI display")]
    [Tooltip("optional UI display")]
    public TextMeshProUGUI actionPointsFrame;

    public event Action NoActionPointsLeftEvent;
    public event Action ActionPointsResetEvent;

    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649

    private void Awake()
    {
        RefillActionPointPool();
        AssignButtons();
    }

    private void Start()
    {
        SetRegeneration(isRegenerating);
    }


    public void Update()
    {
        TriggerAction();
        UpdateActionState();
    }

    ///  Public Methods
    ///  
    void TriggerAction()
    {
        foreach (var item in actions)
        {
            if (Input.GetKeyDown(item.actionkey))
            {
                if (item.canTrigger)
                {
                    DoAction(item);
                }
                else
                { Debug.Log("Not enough actions points left"); }
            }
        }
    }


    public void DoAction(PlayerAction playerAction)
    {
        RemoveActionPoints(playerAction.actionPointCost);
        playerAction.action.Invoke();
    }


    public void SetRegeneration(bool tof)
    {
        isRegenerating = tof;
        if (isRegenerating)
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
        AddActionPoints(actionPointsRestoredPerSecond);
    }


    public void AddActionPoints(int actionPointsRestored)
    {
        actionPointsLeftThisTurn += actionPointsRestored;
        actionPointsLeftThisTurn = Mathf.Clamp(actionPointsLeftThisTurn, 0, actionPointsPerTurn);
        UpdateActionState();
    }

    public void RemoveActionPoints(int actionPointCost)
    {
        actionPointsLeftThisTurn -= actionPointCost;
        CheckIfOutOfActionPoints();
        UpdateActionState();
    }

    public void RefillActionPointPool()
    {
        actionPointsLeftThisTurn = actionPointsPerTurn;
        UpdateActionState();
        ActionPointsResetEvent?.Invoke();
    }
    public void EmptyActionPointPool()
    {
        actionPointsLeftThisTurn = 0;
        UpdateActionState();
    }

    ///  Private Methods

    void CheckIfOutOfActionPoints()
    {
        if (actionPointsLeftThisTurn == 0)
            NoActionPointsLeftEvent?.Invoke();
    }

    void UpdateActionState()
    {
        foreach (var item in actions)
        {
            item.CheckIfOOM(actionPointsLeftThisTurn);
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        if (actionPointsFrame != null)
            actionPointsFrame.text = "Action points left: " + actionPointsLeftThisTurn.ToString();
    }

    void AssignButtons()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].button.onClick.AddListener(actions[i].DoAction);
        }
        foreach (var item in actions)
        {
      //      item.button.onClick.AddListener(TriggerAction);
        }
    }


}


[Serializable]
public class PlayerAction
{
    public string actionName;
    public int actionPointCost;

    public UnityEvent action;
    [Space]

    public bool executeWithKeys;
    public KeyCode actionkey;

    public bool executeWithButton;
    public Button button;

    public bool canTrigger;

    public void CheckIfOOM(float playerActionPointsInPool)
    {
        canTrigger = (playerActionPointsInPool >= actionPointCost);
        if (button != null)
            button.interactable = canTrigger;
    }

    public void DoAction()
    {
        //RemoveActionPoints(playerAction.actionPointCost);
        Debug.Log("2");
       // playerAction.action.Invoke();
    }
}


