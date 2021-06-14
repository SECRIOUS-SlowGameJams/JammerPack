using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SECRIOUS.BoardGame
{
    public class BoardSystemManager : MonoBehaviour
    {
        public Button EndTurnButton;
        public Button dieRollButton;
        BoardPlayerAvatarUIManager boardPlayerManager;

        public GameObject boardPlayerAvatarPrefab;
        public Transform boardPlayerContainer;

        public BoardPlayerAvatarSprite activePlayer;

        public int turnsSoFar;
        public int index;

        public GameObject[] tiles;

        private void Awake()
        {
            boardPlayerManager = FindObjectOfType<BoardPlayerAvatarUIManager>();
            boardPlayerManager.BoardPlayerInitiationCompletedEvent += InitiateBoardWithUIData;
            EndTurnButton.onClick.AddListener(EndGameTurn);
            dieRollButton.onClick.AddListener(AssignDieResult);
        }
        // Start is called before the first frame update
        void Start()
        {
            turnsSoFar = 0;
            index = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void StartGameTurns()
        {

        }

        public void EndGameTurn()
        {
            turnsSoFar++;
            index++;
            boardPlayerManager.SetActivePlayer(boardPlayerManager.allPlayers[index]);
        }

        void AssignDieResult()
        {
            //boardPlayerManager.activePlayer. = dieRollButton.GetComponent<SimpleDieRoller>().rollResult;
        }

        void InitiateBoardWithUIData()
        {
            foreach (var item in boardPlayerManager.allPlayers)
            {
                GameObject newPlayerMeeple = Instantiate(boardPlayerAvatarPrefab, boardPlayerContainer);
                BoardPlayerAvatarSprite playerAvatarBoard = newPlayerMeeple.GetComponent<BoardPlayerAvatarSprite>();
                playerAvatarBoard.ReceiveBoardPlayerData(item.boardPlayerData);
                playerAvatarBoard.PlaceOnTile(tiles[0].transform);
            }
        }


    }

}