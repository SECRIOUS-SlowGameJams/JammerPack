using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * 
 */
[CreateAssetMenu(fileName = "NarrativeItem", menuName = "ScriptableObjects/NarrativeItem", order = 1)]
public class NarrativeItemScriptableObject : ScriptableObject
    {
    public string itemName;
    [TextArea]
    public string[] itemDescription;
}
