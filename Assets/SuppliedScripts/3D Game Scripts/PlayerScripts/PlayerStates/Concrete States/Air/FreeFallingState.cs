using UnityEngine;
using System.Collections;

public class FreeFallingState : BaseAbstractPlayerState
{
    Transform feet;

    public override void EnterState(PlayerStateController playerStateController)
    {
        base.EnterState(playerStateController);
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        //I should keep this at true, since otherwise I land on my face :P 
        rigidbody.freezeRotation = true;


        //I should reenable the usual camera script
        fPLookAround.enabled = true;
    }

    public override void FixedUpdate(PlayerStateController playerStateController)
    {
        //Transitions:
        //I exit FreeFalling when I have landed. I enter Idle and then free to move again.
        //Since Physics is included, should this be at FixedUpdate rather than Update?
        if (groundCheck.isGrounded)
        {
            ZeroOutForces();
            playerStateController.ToGroundIdleState();
        }
    }

    public override void Update(PlayerStateController playerStateController)
    {
        //we are just falling here. No controls. 
    }




    //I want a super smooth landing
    void ZeroOutForces()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
