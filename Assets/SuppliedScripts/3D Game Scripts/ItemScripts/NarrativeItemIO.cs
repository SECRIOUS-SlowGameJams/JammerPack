using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * 
 */


public class NarrativeItemIO : InteractiveItemTemplate
{
    /// Public Properties
    /// 

    public NarrativeItemScriptableObject narrativeItemData;
    public int lastStoryIndex;

    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


    //  public virtual void Awake()
    public override void Awake()
    {
        base.Awake();
    }

   
     public override void Interact()
    {
        base.Interact();
        //show story somehow
        
        //increase index 
        lastStoryIndex++;
        lastStoryIndex = Mathf.Clamp(lastStoryIndex , 0, narrativeItemData.itemDescription.Length - 1);
        CheckIfDone();
    }

    ///  Public Methods

    ///  Private Methods
    void CheckIfDone()
    {
        if (lastStoryIndex == narrativeItemData.itemDescription.Length - 1)
        {
            hasInteracted = true;
        }
    }

}


