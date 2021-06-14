using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//player has no control at all
[Serializable]
public class InertState : BaseAbstractPlayerState
{
    //can do nothing
    public override void EnterState(PlayerStateController playerStateController)
    {
        base.EnterState(playerStateController);
        //override look around from base
        fPLookAround.enabled = false;
        interactableDetector.enabled = false;     
        reachDetector.enabled = false;
    }
    public override void FixedUpdate(PlayerStateController playerStateController)
    {
       //player has no control 
    }

    public override void Update(PlayerStateController playerStateController)
    {

        //transitions
        //cannot transition on your own. 
        //triggered transition with call from playercontroller.
        
    }


}
