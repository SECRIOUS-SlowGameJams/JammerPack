using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SECRIOUS.Utilities;
using TMPro;

/*
 * 
 */

    public class GameTurnsMechanic : MonoBehaviour
    {
    /// Public Properties

    public int roundIndex;
    public TextMeshProUGUI currentRoundFrame;

    [Space]
    public int playersTurnIndex;
    public TextMeshProUGUI currentTurnFrame;

    [Space]
    public List<GameTurnPlayer> allPlayers;
    public GameTurnPlayer activePlayer;

    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


    ///  private Fields


    ///  Unity CallBacks Methods
    void Awake()
        {
       
        foreach (var item in allPlayers)
        {
            item.declareEndOfTurnEvent.AddListener(PassTurnToNextPlayer);
        }
    }

        void OnEnable()
        {

        }

        void Start()
        {
        StartGame();
        }

        void Update()
        {
        }

        void OnDisable()
        {
        }


        ///  Public Methods
         public void StartGame()
    {
        roundIndex = 0;
        playersTurnIndex = 0;

        foreach (var item in allPlayers)
        {
            item.TransitionToState(item.inActiveTurnState);
        }

        activePlayer = allPlayers[0];
        activePlayer.InitializeTurn();
        DisplayDataOnUI();
    }


        ///  Private Methods


    void PassTurnToNextPlayer(GameTurnPlayer gameTurnPlayer)
    {
        playersTurnIndex = allPlayers.IndexOf(activePlayer) + 1;
        if (playersTurnIndex == allPlayers.Count)
        {
            playersTurnIndex = 0;
            roundIndex++;
        }

        activePlayer = allPlayers[playersTurnIndex];
        activePlayer.InitializeTurn();
        DisplayDataOnUI();
    }

    void DisplayDataOnUI()
    {
        if (currentRoundFrame != null) currentRoundFrame.text = "Current round is " + roundIndex;
        if (currentTurnFrame != null) currentTurnFrame.text = "Current player turn is " + (playersTurnIndex + 1);
    }


    }
