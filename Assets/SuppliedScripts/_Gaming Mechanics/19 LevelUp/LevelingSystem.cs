using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using SECRIOUS.Utilities;
using TMPro;

namespace SECRIOUS._Gaming_Mechanics
{
    /*
     * This script implements a basic leveling system for the player. 
     * This leveling system is purely nominal - if you want to affect player stats or abilities based on the current level, you need to design and tie that system in. 
     * Provided your game system has a level cap, do the following:
     * 1) Create a scriptable objectof type "LevelingSystemScriptableObj" which holds the amount of levels, the name of the level/rank (or simply its index) and the threshold amount of XP to attain that level.
     * 2) Assign that scriptable object to this component
     * 3) From any source pertinent in your game design, call the function "AddXP" to feed in any concept equivalent to experience points in your game. 
     * 4) Levels and leveling up events are calculated and called automatically.
     */


    public class LevelingSystem : MonoBehaviour
    {
        /// Public Properties
        [Header("Design data")]
        [Tooltip("Set your lvling system here")]
        public LevelingSystemScriptableObj LS_SO;
        [Tooltip("Add any events you want trigggered at lvl up here")]
        public UnityEvent lvlUpEvent;

        public string CurrentLevel { get { return currentLevel; } }
        public int CurrentXP { get { return currentXP; } } 

        /// Serialized Fields for Editor
#pragma warning disable 0649
        [Header("Runtime data")]
        [SerializeField]
        int currentXP;
        [SerializeField]
        string currentLevel;
        [SerializeField]
        bool hasMaxedOut;
        [SerializeField]
        bool shouldCapXP;
#pragma warning restore 0649

        private void Awake()
        {
            //if you'd like to load your XP from disk, add it here.
            //Else will use default starting value, as defined in the Editor (or as type default value if undefined).
            currentLevel = LS_SO.CalculateLevel(currentXP);
        }

        ///  Public Methods
        public void AddXP(int addedXP)
        {
            //if you are not at maximum level, add the XP and check if you level up
            if (!hasMaxedOut)
            {
                currentXP += addedXP;
                SendMessage("OnXPchanged", SendMessageOptions.DontRequireReceiver);
                CheckForLevelUp();
            }
            //if you are at maximum Level, if your design allows for XP to increase byeond max, do add the XP but don't check for Lvl up because it's pointless
            else if (!shouldCapXP)
            {
                currentXP += addedXP;
                SendMessage("OnXPchanged", SendMessageOptions.DontRequireReceiver);
            }
            //otherwise, do nothing 
            //OR add smth here to convert overflow XP into something else.
        }


        ///  Private Methods
        void CheckForLevelUp()
        {
            string lvl = LS_SO.CalculateLevel(currentXP);
            if (lvl != currentLevel)
            {
                currentLevel = lvl;
                lvlUpEvent?.Invoke();
                CheckForLevelMaxed();
                SendMessage("OnLevelUp", SendMessageOptions.DontRequireReceiver);
            }
        }

        void CheckForLevelMaxed()
        {
            if (currentLevel == LS_SO.Levels[LS_SO.Levels.Length - 1].level)
            {
                hasMaxedOut = true;
                if (shouldCapXP)
                    CapXP();           
            }
        }

        void CapXP()
        {
            currentXP = LS_SO.Levels[LS_SO.Levels.Length - 1].xp;
        }    
    }

}

public interface ILevelingSystemListener
{
     void OnXPchanged();
     void OnLevelUp();

}