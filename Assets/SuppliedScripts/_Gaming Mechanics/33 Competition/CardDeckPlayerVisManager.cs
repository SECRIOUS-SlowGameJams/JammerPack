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


    public class CardDeckPlayerVisManager : MonoBehaviour
    {
        /// Public Properties
        public CardDeckPlayer cardDeckPlayer;

        public TextMeshProUGUI cardsLeftInDeck;

      public TextMeshProUGUI handsize;

        /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


        ///  private Fields


        ///  Unity CallBacks Methods
        void Awake()
        {
        cardDeckPlayer.keyValueChangedEvent.AddListener(DisplayCardsLeft);
        }

        void OnEnable()
        {
        }

        void Start()
        {
        }

        void Update()
        {
        handsize.text = cardDeckPlayer.CardsInHand.ToString();
        }


        void OnDisable()
        {
        }


        ///  Public Methods



        ///  Private Methods
        void DisplayCardsLeft(float value)
    {
        cardsLeftInDeck.text = value.ToString();
    }


    }
