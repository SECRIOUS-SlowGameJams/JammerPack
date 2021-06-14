using UnityEngine;
using System.Collections;



//can only look around
public class ImmobileState : BaseAbstractPlayerState
{
    public override void EnterState(PlayerStateController playerStateController)
    {
        base.EnterState(playerStateController);
        fPLookAround.enabled = true;
    }

    public override void FixedUpdate(PlayerStateController playerStateController)
    {
        //nothing happens here ever.
    }

    public override void Update(PlayerStateController playerStateController)
    {
        //

    }

}
