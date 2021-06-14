using UnityEngine;
using System.Collections;

public abstract class GroundedMovementState : GroundedState
{
    public float speed;


    public override void EnterState(PlayerStateController playerStateController)
    {
        base.EnterState(playerStateController);
    }

    public override void Update(PlayerStateController playerStateController)
    {
    
        //put audio here
        //maybe put some camera bob here?

         playerStateController.ToIdle();
    }


    public override void FixedUpdate(PlayerStateController playerStateController)
    {
           Move(playerStateController);
    }



    void Move(PlayerStateController playerStateController)
    {

        Vector3 forward = playerStateController.transform.forward * Input.GetAxis("Vertical");
        Vector3 sideways = playerStateController.transform.right * Input.GetAxis("Horizontal");
        Vector3 translation = (forward + sideways).normalized;
        Vector3 finalTranslation = translation * speed * Time.fixedDeltaTime;

        Vector3 previousPosition = playerStateController.transform.position;
        Vector3 finalPosition = rigidbody.position + finalTranslation;
        rigidbody.MovePosition(finalPosition);
    }

   

}
