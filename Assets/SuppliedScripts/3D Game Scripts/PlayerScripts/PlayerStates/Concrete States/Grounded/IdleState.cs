using UnityEngine;
using System.Collections;



//has full control, but is not doing anything
public class IdleState : BaseAbstractPlayerState
{
    public override void EnterState(PlayerStateController playerStateController)
    {
        //nothing happens right now.
    }

    public override void FixedUpdate(PlayerStateController playerStateController)
    {
        //nothing happens here ever.
    }

    public override void Update(PlayerStateController playerStateController)
    {
        //

        //Transitions:

        //from idle state I can enter Walking state
        playerStateController.ToWalking();

        //from idle state I can enter Running state
        playerStateController.ToRunning();

        //from idle state I can enter Jumping state
        playerStateController.ToJumping();

    }

}
