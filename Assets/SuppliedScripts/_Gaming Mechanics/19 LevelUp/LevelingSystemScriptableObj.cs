
using System;
using UnityEngine;

namespace SECRIOUS._Gaming_Mechanics
{
    /*
     * Used with the leveling system to store values and perform calculations.
     */

    [CreateAssetMenu(fileName = "LevelingSystem", menuName = "ScriptableObjects/LevelingSystem", order = 1)]
    public class LevelingSystemScriptableObj : ScriptableObject
    {
        //Create an array with the same size as your levels, add XP threshold for each level and either an increasing index or a keyword for the respective level.
        public LevelClass[] Levels;


        public string CalculateLevel(int currentXP)
        {
            //not likely case to happen, since starting level XP should normally be "0" and it's unlikely to have a system with XP penalties so as to ever get in the negative range, 
            //but included for comprehensive reasons
            if (currentXP < Levels[0].xp)
                return Levels[0].level;


            for (int i = 0; i < Levels.Length - 1; i++)
            {
                //threshold value is INCLUDED (>=)
                if (currentXP >= Levels[i].xp && currentXP < Levels[i + 1].xp)
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
                    //exlude max level 
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
        //this can very well be a number, but it's set as string to accomodate for Leveling systems with honorary words. Convert to int where necessary if using a numeric system.
        public string level;
        //this is the xp threshold to *enter* this lvl 
        public int xp;

        
        //add any other field or method you deem suitable here

    }
}