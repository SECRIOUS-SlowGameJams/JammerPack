using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DisplayInfoOnUI : MonoBehaviour
{
    public dynamic Text;

    public InfoText infoText;

    public TextVariable textVariable;
    public FloatVariable floatdata;

    public TextMeshProUGUI frame;

    private void Update()
    {

    }

}


[Serializable]
public class FloatReference
{
    public bool useConstantValue;
    public float ConstantValue;
    public FloatVariable Variable;

    public float Value
    {
        get { return useConstantValue ? ConstantValue : Variable.value; }
        set
        {
            if (useConstantValue)
                ConstantValue = value;
            else
                Variable.value = value;
        }
    }
}

[Serializable]
public class FloatVariable
{
    public float value;
}

[Serializable]
public class KeyValue
{
    public float value;
}


[Serializable]
public class TextVariable 
{
    public string value;
}

[Serializable]
public class InfoText : UnityEvent<string>
{

}