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
namespace SECRIOUS.BoardGame {

    public class BoardPlayerAvatarSprite : MonoBehaviour
    {
        /// Public Properties
        /// 

        public BoardPlayerData boardPlayerData;

        /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


        ///  private Fields
        SpriteRenderer playerAvatarSprite;

        ///  Unity CallBacks Methods
        void Awake()
        {
            playerAvatarSprite = GetComponent<SpriteRenderer>();
        }


        ///  Public Methods
        public void PlaceOnTile(Transform transform)
        {
            transform.position = transform.position;
        }


        ///  Private Methods
        public void ReceiveBoardPlayerData(BoardPlayerData incomingBoardPlayerData)
        {
            boardPlayerData = incomingBoardPlayerData;
            playerAvatarSprite.color = boardPlayerData.playerColour;
        }

    }
}