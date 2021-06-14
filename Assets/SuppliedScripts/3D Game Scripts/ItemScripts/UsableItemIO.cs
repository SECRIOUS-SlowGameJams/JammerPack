using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UsableItemIO : InteractiveItemTemplate
{
    /// Public Properties

    public string itemName;

    public UnityEvent itemUseEvent;
    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649

    ///  private Fields

    ///  Unity CallBacks Methods
    public override void Awake()
    {
        base.Awake();
    }

    public override void Interact()
    {
        base.Interact();
        itemUseEvent?.Invoke();
        hasInteracted = true;
    }

}

