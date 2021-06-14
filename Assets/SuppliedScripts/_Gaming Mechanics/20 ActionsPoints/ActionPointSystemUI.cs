using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


namespace SECRIOUS._Gaming_Mechanics
{
    /*
     * This script visualizes in text format data from the ActionPointSystem script. 
     * In this occasion, it displays the currently available action points. 
     * Expand it to show all possible actions or even more elaborately, propose action combinations based on cost/affordability.
     */

    [RequireComponent(typeof(ActionPointSystem))]
    public class ActionPointSystemUI : MonoBehaviour
    {
        /// Public Properties
        public TextMeshProUGUI actionPointsFrame;

        /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


        ///  private Fields
        ActionPointSystem actionPointSystem;

        ///  Unity CallBacks Methods
        void Awake()
        {
            actionPointSystem = GetComponent<ActionPointSystem>();
        }


        void Update()
        {
            DisplayInfoOnUi();
        }

        ///  Public Methods
        void DisplayInfoOnUi()
        {
            if (actionPointsFrame != null)
                actionPointsFrame.text = "Action points: " + actionPointSystem.pointPool.actionPointsLeftThisTurn.ToString() + "/" + actionPointSystem.pointPool.actionPointPoolSize.ToString();
        }


        ///  Private Methods

    }
}