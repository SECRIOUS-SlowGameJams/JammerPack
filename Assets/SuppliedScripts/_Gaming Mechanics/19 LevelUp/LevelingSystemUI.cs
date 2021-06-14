using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/*
 * 
 */

namespace SECRIOUS._Gaming_Mechanics
{
    
    [RequireComponent(typeof (LevelingSystem))]
    public class LevelingSystemUI : MonoBehaviour, ILevelingSystemListener
    {
        /// Public Properties

        //If you'd like to choose a leveling system on another gameobject, 
        //remove the RequireComponent attribute and make this field public to set it in the editor or connect it in any way suitable for you;
        LevelingSystem levelingSystem;

        [Tooltip("UI display")]
        public TextMeshProUGUI lvlFrame;
        public TextMeshProUGUI xpFrame;
       
        /// Serialized Fields for Editor
#pragma warning disable 0649

        [Header("Choose a display format")]
        [SerializeField]
        public bool displayXpTillLastLvl;
        [SerializeField]
        public bool displayLevelAsRank;
#pragma warning restore 0649

        public void Awake()
        {
            levelingSystem = GetComponent<LevelingSystem>();
        }

        public void Start()
        {
            //initialize view
            UpdateXPonUI();
            UpdateLvlOnUI();
        }

        //runtime responses
        public void OnXPchanged()
        {
            UpdateXPonUI();
        }

        public void OnLevelUp()
        {
            UpdateLvlOnUI();
        }

        //tie this to message listener
        void UpdateXPonUI()
        {
            if (xpFrame != null)
            {
                if (displayXpTillLastLvl)
                {
                    xpFrame.text = "XP: " + levelingSystem.CurrentXP + "/" + levelingSystem.LS_SO.Levels[levelingSystem.LS_SO.Levels.Length - 1].xp;
                }
                else
                    xpFrame.text = "XP: " + levelingSystem.CurrentXP + "/" + levelingSystem.LS_SO.ReturnXPForNextLevel(levelingSystem.CurrentLevel);
            }
        }

        //tie this to message listener
        void UpdateLvlOnUI()
        {
            if (lvlFrame != null)
                if (displayLevelAsRank)
                {
                    lvlFrame.text = "Rank: " + levelingSystem.CurrentLevel;
                }
                else
                    lvlFrame.text = "Level: " + levelingSystem.CurrentLevel + "/" + levelingSystem.LS_SO.Levels[levelingSystem.LS_SO.Levels.Length - 1].level;
          
        }

    }
}

