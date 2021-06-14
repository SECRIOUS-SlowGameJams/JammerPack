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


 public class CombatPlayer : CompetitionAspect
{
    /// Public Properties
    /// Public Properties
    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


    ///  private Fields



    ///  Public Methods

    public void TakeDamage(float dmg)
    {
        DecreaseKeyValue(dmg);
    }

    public void RegainHealth(float healthpoints)
    {
        IncreaseKeyValue(healthpoints);
    }


}

