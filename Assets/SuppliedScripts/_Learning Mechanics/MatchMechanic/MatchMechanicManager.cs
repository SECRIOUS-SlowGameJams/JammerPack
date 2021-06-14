using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
/*
 * 
 */


    public class MatchMechanicManager : MonoBehaviour
    {
    /// Public Properties
    public TextMeshProUGUI statusdisplayFrame;
    public UnityEvent matchMechanicCompleteEvent;


    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


    ///  private Fields
    int matchedElements;
    MatchMechanicMovableElement[] elementGameObjectArray;
    List<MatchDetails> elementDataClassList;

    ///  Unity CallBacks Methods
    void Awake()
        {
        RegisterAllElements();
        SubscribeToStatusUpdates();
        }

    private void Start()
    {
        DisplayCurrentStatus();
    }


    ///  Public Methods



    ///  Private Methods
    void RegisterAllElements()
    {
        elementGameObjectArray = FindObjectsOfType<MatchMechanicMovableElement>();
        elementDataClassList = new List<MatchDetails>();
        foreach (var item in elementGameObjectArray)
        {
            elementDataClassList.Add(item.matchDetails);
        }
    }

    void SubscribeToStatusUpdates()
    {
        foreach (var item in elementGameObjectArray)
        {
            item.MatchMechaniElementMatchedEvent += UpdateCurrentStatus;
        }
    }

    void UpdateCurrentStatus(MatchDetails matchDetails = null)
    {
        matchedElements = 0;
        foreach (var item in elementDataClassList)
        {
            if (item.isMatched)
                matchedElements++;
        }
        DisplayCurrentStatus();
        CheckForCompletion();
    }

    void DisplayCurrentStatus()
    {
        statusdisplayFrame.text = matchedElements + "/" + elementDataClassList.Count;
    }


    void CheckForCompletion()
    {
        if (matchedElements == elementDataClassList.Count)
        {
            matchMechanicCompleteEvent?.Invoke();
        }
    }


    }
