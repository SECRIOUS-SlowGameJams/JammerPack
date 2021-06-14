using UnityEngine;
using System.Collections;

public class JumpingState : BaseAbstractPlayerState
{

    public override void EnterState(PlayerStateController playerStateController)
    {
        base.EnterState(playerStateController);
        Jump3(playerStateController);
    }


    public override void FixedUpdate(PlayerStateController playerStateController)
    {
       
    }

    public override void Update(PlayerStateController playerStateController)
    {
        //Transitions:
        //I can enter Free Falling when the velocity of the rigidbody is negative? 
        if (FallingCheck())
        {
            playerStateController.ToFreeFalling();
        }

    }

    bool FallingCheck()
    {
        //Should I check the velocity of the rigidbody?
        if (rigidbody.velocity.y < 0 )
            return true;
        else
            return false;
    }


    void Jump1(float force)
    {
      
            //jump up 
        rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        //and navigate in mid air with controls ?

    }

    void Jump2(PlayerStateController playerStateController)
    {
        //jump on look axis
        Transform cameraOrientation = fPLookAround.followCamera.transform;

        rigidbody.AddForce((cameraOrientation.forward + Vector3.up) * playerStateController.jumpingForce, ForceMode.Impulse);
    }

    void Jump3(PlayerStateController playerStateController)
    {
        //Vector3 existingVelocity = rigidbody.velocity;
        //Vector3 jumpingVelocity = existingVelocity + new Vector3(0, playerStateController.jumpingForce, 0);

        //rigidbody.velocity = jumpingVelocity;

        //Jump1(playerStateController.jumpingForce/2);
        Jump2(playerStateController);
    }

}
