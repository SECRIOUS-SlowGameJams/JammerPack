using UnityEngine;
using System.Collections;
using System;


[Serializable]
public abstract class BaseAbstractPlayerState
{
    protected Rigidbody rigidbody;
    public FPCamera fPLookAround;
    public GroundCheck groundCheck;

    public PlayerViewPointRaycaster interactableDetector;
    public ReachDetector reachDetector;
    public InteractFeature interactFeature;

    //public abstract void EnterState(PlayerStateController playerStateController);
    public virtual void EnterState(PlayerStateController playerStateController)
    {
        rigidbody = playerStateController.GetComponentInChildren<Rigidbody>();
        fPLookAround = playerStateController.GetComponentInChildren<FPCamera>();

        groundCheck = playerStateController.GetComponentInChildren<GroundCheck>();
        interactableDetector = playerStateController.GetComponentInChildren<PlayerViewPointRaycaster>();
        reachDetector = playerStateController.GetComponentInChildren<ReachDetector>();
        interactFeature = playerStateController.GetComponentInChildren<InteractFeature>();

        fPLookAround.enabled = true;
        interactableDetector.enabled = true;
        reachDetector.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public abstract void Update(PlayerStateController playerStateController);
    public abstract void FixedUpdate(PlayerStateController playerStateController);
}
