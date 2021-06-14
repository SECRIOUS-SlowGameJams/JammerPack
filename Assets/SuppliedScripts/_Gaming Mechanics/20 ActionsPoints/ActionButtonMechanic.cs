using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * 
 */

[RequireComponent(typeof(Button))]
    public class ActionButtonMechanic : MonoBehaviour
    {
    /// Public Properties
    public ActionPointSystem actionPointsMechanic;
    public PlayerAction playerAction;
    Button button;
        /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


        ///  private Fields


        ///  Unity CallBacks Methods
        void Awake()
        {
        button = GetComponent<Button>();
        button.onClick.AddListener(DoActionFromUI);
        }


        void Update()
        {
           UpdateButtonInteractivity();
        }



        ///  Public Methods



        ///  Private Methods
        void DoActionFromUI()
    {
        actionPointsMechanic.DoAction(playerAction);
    }

        void UpdateButtonInteractivity()
    {

    }

    }
