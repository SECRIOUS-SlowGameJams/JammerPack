
using System;
using UnityEngine;


/*
 * 
 */
[CreateAssetMenu(fileName = "LevelingSystem", menuName = "ScriptableObjects/LevelingSystem", order = 1)]
public class LevelingSystemScriptableObj : ScriptableObject
{

    public LevelClass[] Levels;


    public string CalculateLevel(int currentXP)
    {
        if (currentXP < Levels[0].xp)
            return Levels[0].level;

        for (int i = 0; i < Levels.Length -1; i++)
        {
            if (currentXP >= Levels[i].xp && currentXP < Levels[i+1].xp)
            {
                return Levels[i].level;
            }
        }

        if (currentXP >= Levels[Levels.Length - 1].xp)
            return Levels[Levels.Length - 1].level;

        return "";
        }

    public int ReturnXPForNextLevel(string currentLevel)
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            if (Levels[i].level == currentLevel)
            {
                if (i == Levels.Length - 1)
                    return Levels[i].xp;
                return Levels[i + 1].xp;
            }
        }
       return 0;
    }

}

[Serializable]
public class LevelClass
{
    public string level;
    //this is the xp threshold to enter this lvl
    public int xp;
}