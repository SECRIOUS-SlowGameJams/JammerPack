using System;
using UnityEngine;


/*
 * 
 */

namespace SECRIOUS.BoardGame
{
    [Serializable]
    public class BoardPlayerData
    {
        public string playerName;
        public Color playerColour;

        public int movementPoints;
        public int tilePosition;

        //add more as you see fit
    }
}