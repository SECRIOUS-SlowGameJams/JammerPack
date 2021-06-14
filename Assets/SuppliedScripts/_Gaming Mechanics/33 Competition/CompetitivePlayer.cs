using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SECRIOUS.Utilities;

/*
 * 
 */


    public class CompetitivePlayer : MonoBehaviour
    {
    /// Public Properties
    public CompetitivePlayerData CompetitivePlayerData;

    public List<WinLoseCondition> complexWinConditions;
    public List<WinLoseCondition> complexLoseConditions;

    [HideInInspector]
    public bool finishedThisMatch;

    public event Action<CompetitivePlayerData> playerWonEvent;
    public event Action<CompetitivePlayerData> playerLostEvent;

    public TextVariable playerName;
    [ContextMenu("InitializePlayerName")]
    public void InitializePlayerName()
    {
        playerName = new TextVariable();
    }

    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


    ///  private Fields


    private void Awake()
    { 
        RegisterAllComplexConditions();
    }

    void RegisterAllComplexConditions()
    {
        WinLoseConditionManager[] allWinLoseConditionManagers = GetComponentsInChildren<WinLoseConditionManager>();
        complexWinConditions = new List<WinLoseCondition>();
        complexLoseConditions = new List<WinLoseCondition>();

        for (int i = 0; i < allWinLoseConditionManagers.Length; i++)
        {
            for (int z = 0; z < allWinLoseConditionManagers[i].WinOrLoseConditions.Length; z++)
            {
                if (allWinLoseConditionManagers[i].WinOrLoseConditions[z].isPartOfMultiCondition)
                {
                    if (allWinLoseConditionManagers[i].WinOrLoseConditions[z].winOrLoseEvent == WinLoseCondition.ResponseEventType.Lose)
                        complexLoseConditions.Add(allWinLoseConditionManagers[i].WinOrLoseConditions[z]);
                    else
                        complexWinConditions.Add(allWinLoseConditionManagers[i].WinOrLoseConditions[z]);
                }
            }
        }
    }

    public void ComplexCheckIfWon()
    {
        for (int i = 0; i < complexWinConditions.Count; i++)
        {
            if (!complexWinConditions[i].isResolved)
                return;
        }
        PlayerWon();
    }

    public void ComplexCheckIfLost()
    {
        for (int i = 0; i < complexLoseConditions.Count; i++)
        {
            if (!complexLoseConditions[i].isResolved)
                return;
        }
        PlayerLost();
    }


    public void PlayerWon()
    {
        RecordWin();
        playerWonEvent?.Invoke(CompetitivePlayerData);
    }

    public void RecordWin()
    {
        Debug.Log("Player " + CompetitivePlayerData.PlayerName + " won.");
        CompetitivePlayerData.matchesWonSoFar += 1;
        finishedThisMatch = true;
    }

    public void PlayerLost()
    {
        RecordLoss();
        playerLostEvent?.Invoke(CompetitivePlayerData);
    }

    public void RecordLoss()
    {
        Debug.Log("Player " + CompetitivePlayerData.PlayerName + " lost.");
        CompetitivePlayerData.matchesLostSoFar += 1;
        finishedThisMatch = true;
    }
}


[Serializable]
public class CompetitivePlayerData : PlayerData
{
    public int matchesWonSoFar;
    public int matchesLostSoFar;
}

[Serializable]
public class PlayerData
{
    public string PlayerName;
}