using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SECRIOUS.Utilities;
using UnityEngine.Events;

/*
 * 
 */


public class WinLoseConditionManager : MonoBehaviour
    {

    CompetitivePlayer competitivePlayer;
    public CompetitionAspect competitionAspect;
    [SerializeField]
    public QuantitativeWinLoseCondition[] WinOrLoseConditions;
    
    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649
    private void Start()
    {
        competitivePlayer = competitionAspect.competitivePlayer;
        competitionAspect.keyValueChangedEvent.AddListener(DoCheck);
    }

    public void DoCheck(float value)
    {
        foreach (var item in WinOrLoseConditions)
        {
            if (item.CheckForCondition(value))
            {
                ShortCutToCompetitivePlayerEventResponse();
            }
        }
    }


    void ShortCutToCompetitivePlayerEventResponse()
    {
        ExecuteAdequateWinLoseTriggeredCondition();
        CheckIfMultiConditionHasBeenTriggered();
    }

    void ExecuteAdequateWinLoseTriggeredCondition()
    {
        foreach (var item in WinOrLoseConditions)
        {
            if (item.isResolved)
            {
                if (item.isAdequateCondition)
                {
                    switch (item.winOrLoseEvent)
                    {
                        case WinLoseCondition.ResponseEventType.Lose:
                            {
                                competitivePlayer.PlayerLost();
                                break;
                            }
                        case WinLoseCondition.ResponseEventType.Win:
                            {
                                competitivePlayer.PlayerWon();
                                break;
                            }
                    }
                }
            }
        }
    }

    void CheckIfMultiConditionHasBeenTriggered()
    {
        foreach (var item in WinOrLoseConditions)
        {
            if (item.isResolved)
            {
                if (item.isPartOfMultiCondition)
                {
                    switch (item.winOrLoseEvent)
                    {
                        case WinLoseCondition.ResponseEventType.Lose:
                            {
                                competitivePlayer.ComplexCheckIfLost();
                                break;
                            }
                        case WinLoseCondition.ResponseEventType.Win:
                            {
                                competitivePlayer.ComplexCheckIfWon();
                                break;
                            }
                    }
                }
            }
        }
    }

}

public abstract class WinLoseCondition
{
    public enum ResponseEventType
    {
        Win,
        Lose
    }
    public ResponseEventType winOrLoseEvent;
    public bool isAdequateCondition;
    public bool isPartOfMultiCondition;
    public bool isResolved;
    public UnityEvent[] AdditionalEventToRespondWithIfTriggered;

    public void TriggerAllCustomEvents()
    {
        foreach (var item in AdditionalEventToRespondWithIfTriggered)
        {
            item?.Invoke();
        }
    }

    //public abstract bool CheckForCondition(float value = 0);
}


public class QualitativeWinLoseCondition : WinLoseCondition
{
    public UnityEvent triggerEvent;
}

[Serializable]
public class QuantitativeWinLoseCondition : WinLoseCondition
{

    public float thresholdValue;
    public TriggerConditionType condition;

    public enum TriggerConditionType
    {
        valueIsOver,
        valueIsOverOrEqual,
        valueIsBelow,
        valueIsBelowOrEqual
    }

    bool EventTriggerCheck(float currentValue)
    {
        switch (condition)
        {
            case TriggerConditionType.valueIsOver:
                return currentValue > thresholdValue;
            case TriggerConditionType.valueIsOverOrEqual:
                return currentValue >= thresholdValue;
            case TriggerConditionType.valueIsBelow:
                return currentValue < thresholdValue;
            case TriggerConditionType.valueIsBelowOrEqual:
                return currentValue <= thresholdValue;
            default: return false;
        }
    }

    //returns if it has been resolved and executes any additional events related to it;
 public bool CheckForCondition(float currentValue)
    {
        if (EventTriggerCheck(currentValue))
        {
            isResolved = true;
            TriggerAllCustomEvents();
            return true;
        }
        else
        return false;
    }

}


