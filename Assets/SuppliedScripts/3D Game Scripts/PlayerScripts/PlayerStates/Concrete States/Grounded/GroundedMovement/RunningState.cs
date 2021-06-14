using UnityEngine;
using System.Collections;

public class RunningState : GroundedMovementState
{
    public override void EnterState(PlayerStateController playerStateController)
    {
        speed = playerStateController.runningSpeed;
        base.EnterState(playerStateController);
    }
    public override void Update(PlayerStateController playerStateController)
    {
        base.Update(playerStateController);

        //put audio here
        //maybe put some camera bob here?


        //Transitions:

        //from Running state I can enter Idle state
        playerStateController.ToIdle();

        //from Running state I can enter Walking state
        playerStateController.ToWalking();

        //from Running state I can enter Jumping state
        playerStateController.ToJumping();

    }


    public override void FixedUpdate(PlayerStateController playerStateController)
    {
        //this is the main state behaviour - movement from base script with running speed override
        base.FixedUpdate(playerStateController);
    }


}
