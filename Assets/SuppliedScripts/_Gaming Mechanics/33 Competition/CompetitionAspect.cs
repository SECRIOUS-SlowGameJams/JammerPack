using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using SECRIOUS.Utilities;

/*
 * 
 */

public class CompetitionAspect : MonoBehaviour
{
    [HideInInspector]
    public CompetitivePlayer competitivePlayer;
    [HideInInspector]
    public keyValueChangedEvent keyValueChangedEvent;

    public Vector2 keyvalueRange;
    public float currentValue;
    public bool startsWithMaxValue;
    public bool maxIsCap;
    public bool minIsCap;


    public virtual void Awake()
    {
        competitivePlayer = GetComponentInParent<CompetitivePlayer>();
    }


    public virtual void Start()
    {
        InitializeValue();
    }


    public void InitializeValue()
    {
        if (startsWithMaxValue)
            currentValue = keyvalueRange.y;
        ValueStatusChange();
    }

    public void IncreaseKeyValue(float addedValue)
    {
        currentValue += addedValue;
        if (maxIsCap)
            currentValue = Mathf.Clamp(currentValue, keyvalueRange.x, keyvalueRange.y);
        else
        {
            keyvalueRange = new Vector2(keyvalueRange.x, currentValue);
        }
        ValueStatusChange();
    }

    public void DecreaseKeyValue(float removedValue)
    {
        currentValue -= removedValue;
        if (minIsCap)
        currentValue = Mathf.Clamp(currentValue, keyvalueRange.x, keyvalueRange.y);
        else
        {
            keyvalueRange = new Vector2(currentValue, keyvalueRange.y);
        }
        ValueStatusChange();
    }

    public void ValueStatusChange()
    {
        keyValueChangedEvent?.Invoke(currentValue);
    }

}


[System.Serializable]
public class keyValueChangedEvent : UnityEvent<float>
{
}