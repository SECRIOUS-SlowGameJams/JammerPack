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


    public class CombatPlayerVisManager : MonoBehaviour
    {
    /// Public Properties
    public CompetitivePlayer competitivePlayer;

    public CompetitionAspect competitionAspect;

    public TextMeshProUGUI playerName;

    public Slider healthBar;
    public TextMeshProUGUI healthInfo;

    public TextMeshProUGUI outComeFrame;

    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


    ///  private Fields


    ///  Unity CallBacks Methods
    void Awake()
    {
        playerName.text = competitivePlayer.CompetitivePlayerData.PlayerName;
       
        competitionAspect.keyValueChangedEvent.AddListener(DisplayHealthAsBar);
        competitionAspect.keyValueChangedEvent.AddListener(DisplayHealthAsText);

        //healButton.onClick.AddListener(RegainHealthTest);
        //dealDamageButton.onClick.AddListener(TakeDamageTest);

        //#
        //dmgInput.onEndEdit.AddListener(AssigndmgAmountFromUI);
        //healInput.onEndEdit.AddListener(AssignhealAmountFromUI);

        outComeFrame.gameObject.SetActive(false);
    }

    ///  Private Methods
    void DisplayHealthAsBar(float health)
    {
        healthBar.value = health / (float) competitionAspect.keyvalueRange.y;
    }

    void DisplayHealthAsText(float health)
    {
        healthInfo.text = health.ToString() + " / " + competitionAspect.keyvalueRange.y.ToString();
    }

    //ONLY FOR THE SAMPLE

    public float dmgAmountFromUI;

    public void AssigndmgAmountFromUI(string value)
    {
        dmgAmountFromUI = float.Parse(value);
    }
    public void TakeDamageTest()
    {
        competitionAspect.DecreaseKeyValue(dmgAmountFromUI);
    }

    public float healAmountFromUI;

    public void AssignhealAmountFromUI(string value)
    {
        healAmountFromUI = float.Parse(value);
    }

    public void RegainHealthTest()
    {
        competitionAspect.IncreaseKeyValue(healAmountFromUI);
    }

    public void DisplayWinMessage()
    {
        DisplayWinMessage(competitivePlayer.CompetitivePlayerData);

    }
    public void DisplayLoseMessage()
    {
        DisplayLoseMessage(competitivePlayer.CompetitivePlayerData);
    }

    void DisplayWinMessage(CompetitivePlayerData competitivePlayer)
    {
        outComeFrame.gameObject.SetActive(true);
        outComeFrame.text = "Player " + competitivePlayer.PlayerName + " won.";
    }

   public void DisplayLoseMessage(CompetitivePlayerData competitivePlayer)
    {
        outComeFrame.gameObject.SetActive(true);
        outComeFrame.text = "Player " + competitivePlayer.PlayerName + " lost.";
    }


}
