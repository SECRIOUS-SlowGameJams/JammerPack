using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//for npc and items;
    public abstract class InteractiveObjectTemplate : MonoBehaviour
    {
    /// Public Properties
    public bool hasInteracted;
    public bool canInteractOnlyOnce;

    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649

    public abstract void ItemSeenResponse();

    public abstract void ItemReachedResponse();


    public abstract void ItemUnSeenResponse();

    public abstract void ItemOutOfReachResponse();


    ///  Public Methods
    public virtual void Interact()
    {
        if (canInteractOnlyOnce)
        {
            if (hasInteracted) return;
        }
    }

    ///  Private Methods

}
