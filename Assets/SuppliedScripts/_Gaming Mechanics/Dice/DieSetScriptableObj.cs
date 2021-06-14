using UnityEngine;
using System;

//you can create different dice set for each player, class or game 

[CreateAssetMenu(fileName = "DieSet", menuName = "ScriptableObjects/DieSet", order = 1)]
public class DieSetScriptableObj : ScriptableObject
{
    public Die[] dies;

    [Serializable]
    public class Die
    {
        public Texture2D image;
        public DieType die;
    }
}

public enum DieType
{
    d4 = 4,
    d6 = 6,
    d8 = 8,
    d12 = 12,
    d20 = 20
}