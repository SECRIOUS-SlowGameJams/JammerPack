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

    public class CompetitionMechanicUI : MonoBehaviour
    {
    /// Public Properties
    CompetitionMechanic competitionMechanic;

    public Button AddNewTeamButton;

    public Transform teamContainer;
    public GameObject teamPrefab;

    public GameObject memberPrefab;

        /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


        ///  private Fields


        ///  Unity CallBacks Methods
        void Awake()
        {
        competitionMechanic = FindObjectOfType<CompetitionMechanic>();
        AddNewTeamButton.onClick.AddListener(CreateTeamUI);
        }

        void OnEnable()
        {
        }

        void Start()
        {
        }

        void Update()
        {
        }

        void OnDisable()
        {
        }


        ///  Public Methods



        ///  Private Methods
        public void CreateTeamUI()
    {
        Instantiate(teamPrefab, teamContainer);
        
    }

    }
