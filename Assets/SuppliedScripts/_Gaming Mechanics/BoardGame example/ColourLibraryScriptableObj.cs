using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ColourLibrary", menuName = "ScriptableObjects/ColourLibrary", order = 1)]
public class ColourLibraryScriptableObj : ScriptableObject
{
    [Serializable]
    public class Entry
    {
        public string name;
        public Color color;
    }

    public List<Entry> colors = new List<Entry>();

    public Color GetColor(string name)
    {
        var entry = colors.Find(c => c.name == name);
        if (entry != null)
        {
            return entry.color;
        }
        return Color.white;
    }
}