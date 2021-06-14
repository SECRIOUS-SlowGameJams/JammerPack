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

[RequireComponent(typeof(DraggableSprite))]
public class MatchMechanicMovableElement : MonoBehaviour
{
    /// Public Properties
    public MatchDetails matchDetails;

    //just in case its needed;
    public event Action<MatchDetails> MatchMechaniElementMatchedEvent;
 
    bool hasTestedMatch;
    DraggableSprite draggableSprite;

    private void Awake()
    {
        draggableSprite = GetComponent<DraggableSprite>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        MatchMechanicTileElement matchMechanicTileElement = collision.GetComponent<MatchMechanicTileElement>();
        if (matchMechanicTileElement != null)
        {
            //check the moment you leave the element
            if (!draggableSprite.isDragging)
            {
                //only check once
                if (!hasTestedMatch)
                    DoMatchCheck(matchMechanicTileElement);
            }
        }    
    }


    void DoMatchCheck(MatchMechanicTileElement matchingTile)
    {
        if (matchingTile.tileMatchingValue == matchDetails.matchValue)
        {
            matchDetails.isMatched = true;
            //snap to tile if its correct
            transform.position = new Vector3(matchingTile.transform.position.x, matchingTile.transform.position.y, transform.position.z);
        }
        else
        {
            matchDetails.isMatched = false;
            matchDetails.failAttempts++;
        }
        MatchMechaniElementMatchedEvent?.Invoke(matchDetails);
        hasTestedMatch = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        matchDetails.isMatched = false;
        hasTestedMatch = false;
        //if you removed a rightly placed one
        if (matchDetails.isMatched)
        { 
        matchDetails.failAttempts++;
        MatchMechaniElementMatchedEvent?.Invoke(matchDetails);
        }
    }

}

[Serializable]
public class MatchDetails
    {
    public MatchValue matchValue;
    public bool isMatched;
    public int failAttempts;
    }


public enum MatchValue
{
    blue,
    red,
    green,
    yellow
}
