using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SECRIOUS.Utilities;

namespace SECRIOUS.BoardGame
{
    /*
     * 
     */
    //This script is used to allow players to set up the board game. 
    public class BoardPlayerAvatarUIManager : MonoBehaviour
    {
        /// Public Properties
        public GameObject playerAvatarUIPrefab;

        public ColourLibraryScriptableObj colourLibrary;

        public Button addNewPlayerButton;
        public Button deletePlayerButton;
        public Button completedInitiationButton;
        public Transform playerContainer;

        public int minAmountOfPlayers;
        public int maxAmountOfPlayers;

        public event Action BoardPlayerInitiationCompletedEvent;
        [HideInInspector]
        public List<BoardPlayerAvatarUI> allPlayers;
        [HideInInspector]
        public BoardPlayerAvatarUI activePlayer;

        public int index;
        public Scrollbar scrollBar;
        /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


        ///  private Fields


        List<Color> colorList;

        ///  Unity CallBacks Methods
        void Awake()
        {
            addNewPlayerButton.onClick.AddListener(CreateNewPlayer);
            deletePlayerButton.onClick.AddListener(ConditionalDelete);
            completedInitiationButton.onClick.AddListener(CompletedGameInitialization);

            InitializeColourList();

            UpdateContents();
            DeleteAllPlayers();
        }


        ///  Private Methods
        void CreateNewPlayer()
        {
            BoardPlayerAvatarUI newPlayer = Instantiate(playerAvatarUIPrefab, playerContainer).GetComponent<BoardPlayerAvatarUI>();
            AssignRandomColourToPlayer(newPlayer);
            UpdateContents();
            UIControlStateCheck();
        }


        void AssignRandomColourToPlayer(BoardPlayerAvatarUI newPlayer)
        {
            int size = colorList.Count;
            int index = UnityEngine.Random.Range(0, size);
            newPlayer.SetPlayerColour(colorList[index]);
            colorList.RemoveAt(index);
        }

        void UIControlStateCheck()
        {
            RemovePlayerButtonStateCheck();
            AddPlayerButtonStateCheck();
            CompleteInitializationButtonStateCheck();
        }

        void RemovePlayerButtonStateCheck()
        {
            if (allPlayers.Count == 0)
            {
                deletePlayerButton.interactable = false;
            }
            else
                deletePlayerButton.interactable = true;
        }

        void AddPlayerButtonStateCheck()
        {
            if (allPlayers.Count >= maxAmountOfPlayers)
            {
                addNewPlayerButton.interactable = false;
            }
            else
                addNewPlayerButton.interactable = true;
        }

        void CompleteInitializationButtonStateCheck()
        {
            if (allPlayers.Count < minAmountOfPlayers)
            {
                completedInitiationButton.interactable = false;
            }
            else
                completedInitiationButton.interactable = true;
        }

        public void SetActivePlayer(BoardPlayerAvatarUI playerAvatarUI)
        {
            if (activePlayer != null)
            {
                activePlayer.SetMeAsInactive();
            }
            activePlayer = playerAvatarUI;
            ScrollBarControl();
            //do more here;
        }

        void ConditionalDelete()
        {
            if (activePlayer == null)
                DeleteAllPlayers();
            else
                DeletePlayer(activePlayer);
        }

        void DeletePlayer(BoardPlayerAvatarUI playerAvatarUI)
        {
            colorList.Add(playerAvatarUI.boardPlayerData.playerColour);
            Destroy(playerAvatarUI.gameObject);
            StartCoroutine(UpdateCoroutine());
        }

        private void DeleteAllPlayers()
        {
            foreach (var item in allPlayers)
            {
                Destroy(item.gameObject);
            }
            InitializeColourList();
            StartCoroutine(UpdateCoroutine());
        }

        IEnumerator UpdateCoroutine()
        {
            yield return new WaitForEndOfFrame();
            UpdateContents();
            UIControlStateCheck();
        }
        void UpdateContents()
        {
            allPlayers = playerContainer.gameObject.FetchAllChildren().ReturnASetAsComponent<BoardPlayerAvatarUI>().ToList();
            scrollBar.numberOfSteps = allPlayers.Count;
        }

        void InitializeColourList()
        {
            colorList = new List<Color>();
            foreach (var item in colourLibrary.colors)
            {
                colorList.Add(item.color);
            }
        }

        void CompletedGameInitialization()
        {
            DisableAllUIControls();
            BoardPlayerInitiationCompletedEvent?.Invoke();
        }

        void DisableAllUIControls()
        {
            addNewPlayerButton.interactable = false;
            deletePlayerButton.interactable = false;
            completedInitiationButton.interactable = false;
            foreach (var item in allPlayers)
            {
                item.playerButton.interactable = false;
            }
        }

        void ScrollBarControl()
        {
            index = allPlayers.IndexOf(activePlayer) + 1;
            //this doesnt work well
            if (index == 1)
                scrollBar.value = 0;
            else
                scrollBar.value = 1 - index / (float)scrollBar.numberOfSteps;
        }
    }

}