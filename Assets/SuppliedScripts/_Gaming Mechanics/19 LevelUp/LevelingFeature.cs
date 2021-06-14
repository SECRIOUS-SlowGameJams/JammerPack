using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using SECRIOUS.Utilities;
using TMPro;
/*
 * 
 */


public class LevelingFeature : MonoBehaviour
{
    /// Public Properties
    [Header("Design data")]
    [Tooltip("Set your lvl system here")]
    public LevelingSystemScriptableObj levelingSystem;
    [Tooltip("Add any events you want trigggered at lvl up here")]
    public UnityEvent lvlUpEvent;

    /// Serialized Fields for Editor
#pragma warning disable 0649
    [Header("Runtime data")]
    [SerializeField]
    int currentXP;
    [SerializeField]
    string currentLevel;
    [SerializeField]
    bool hasMaxedOut;


    [Header("UI display")]
    public TextMeshProUGUI lvlFrame;
    public TextMeshProUGUI xpFrame;
    [Tooltip("Will display current lvl and xp/max xp")]
    public bool rankStyleDisplay;
#pragma warning restore 0649



    private void Awake()
    {
        currentLevel = levelingSystem.CalculateLevel(currentXP);
        DisplayOnUI();
    }

    ///  Public Methods
    public void AddXP(int addedXP)
    {
        if (!hasMaxedOut)
        {
            currentXP += addedXP;
            CheckForLevelUp();
        }
        DisplayOnUI();
    }


    ///  Private Methods
    void CheckForLevelUp()
    {
        string lvl =  levelingSystem.CalculateLevel(currentXP);
        if (lvl != currentLevel)
        {
            currentLevel = lvl;
            if (currentLevel == levelingSystem.Levels[levelingSystem.Levels.Length-1].level)
                hasMaxedOut = true;
            lvlUpEvent?.Invoke();
        }
    }

    void DisplayOnUI()
    {
        if (lvlFrame != null)
            if (rankStyleDisplay)
            {
                lvlFrame.text = "Rank: " + currentLevel;
            }
        else
                lvlFrame.text = "Level: " + currentLevel + "/" + levelingSystem.Levels[levelingSystem.Levels.Length - 1].level;
        if (xpFrame != null)
        {
            if (rankStyleDisplay)
            {
                xpFrame.text = "XP: " + currentXP + "/" + levelingSystem.Levels[levelingSystem.Levels.Length - 1].xp;
            }
            else
                xpFrame.text = "XP: " + currentXP + "/" + levelingSystem.ReturnXPForNextLevel(currentLevel.ToString());
        }



    }
}

