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

namespace SECRIOUS.BoardGame
{
    public class BoardPlayerAvatarUI : MonoBehaviour
    {

        public BoardPlayerData boardPlayerData;

        public Image playerSelectionFrame;
        public Image playeravatarImage;
        public TMP_InputField playerNameFrame;
        public Button playerButton;

        BoardPlayerAvatarUIManager BoardPlayerManager;

        private void Awake()
        {
            playerButton.onClick.AddListener(SetMeAsActive);
            BoardPlayerManager = FindObjectOfType<BoardPlayerAvatarUIManager>();
            playerNameFrame.onSelect.AddListener(OnTextFrameSelectResponse);
            playerNameFrame.onEndEdit.AddListener(SetPlayerName);
        }

        public void SetPlayerColour(Color color)
        {
            boardPlayerData.playerColour = color;
            playeravatarImage.color = boardPlayerData.playerColour;
        }

        void SetPlayerName(string name)
        {
            boardPlayerData.playerName = name;
        }

        void OnTextFrameSelectResponse(string text)
        {
            playerNameFrame.placeholder.gameObject.SetActive(false);
        }
        void SetMeAsActive()
        {
            BoardPlayerManager.SetActivePlayer(this);
            playerSelectionFrame.enabled = true;
        }

        public void SetMeAsInactive()
        {
            playerSelectionFrame.enabled = false;
        }
    }
}