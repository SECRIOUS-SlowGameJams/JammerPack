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


    public class CardDeckPlayer : CompetitionAspect
    {
    /// Public Properties
 
    public int CardsInHand;
    public int StartingCardsInHand;



    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


    ///  private Fields


    ///  Unity CallBacks Methods
    public override void Start()
    {
        base.Start();
        CardsInHand = StartingCardsInHand;
    }

    ///  Public Methods
    public void PlayCard()
    {
        CardsInHand--;
    }

    public void DiscardCard()
    {
        CardsInHand--;
    }

    public void DrawCard()
    {
        CardsInHand++;
        DecreaseKeyValue(1);
    }
    ///  Private Methods



}
