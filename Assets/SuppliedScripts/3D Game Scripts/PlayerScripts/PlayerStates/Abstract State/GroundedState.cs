using UnityEngine;
using System.Collections;

public abstract class GroundedState : BaseAbstractPlayerState
{
    public override void EnterState(PlayerStateController playerStateController)
    {
        base.EnterState(playerStateController);
        interactableDetector.enabled = true;
        reachDetector.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }


}
