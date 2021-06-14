using UnityEngine;
using System.Collections;

public class WalkingState : GroundedMovementState
{
    
    public override void EnterState(PlayerStateController playerStateController)
    {
        speed = playerStateController.walkingSpeed;
        base.EnterState(playerStateController);
    }
    public override void Update(PlayerStateController playerStateController)
    {
        base.Update(playerStateController);
        //put audio here
        //maybe put some camera bob here?

        //Transitions:

        //from Walking state I can enter Idle state
        playerStateController.ToIdle();

        //from Walking state I can enter Running state
        playerStateController.ToRunning();

        //from Walking state I can enter Jumping state
        playerStateController.ToJumping();

    }


    public override void FixedUpdate(PlayerStateController playerStateController)
    {
        //this is the main state behaviour - movement from base script with walking speed override
        base.FixedUpdate(playerStateController);
    }

}
