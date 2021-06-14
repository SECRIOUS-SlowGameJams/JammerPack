using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement2D : MonoBehaviour
{

#pragma warning disable 0649
    [SerializeField]
    PhysicalAttributes physicalAttributes;
    [SerializeField]
    ControlSettings controlSettings;
    [SerializeField]
    LayerMask allGroundSurfaces;
#pragma warning restore 0649

    Rigidbody2D rigidbody_2D;

    [SerializeField]
    bool canMove;
    bool isMoving;

    [SerializeField]
    Vector2 playerInputVector;

    private void Awake()
    {
        rigidbody_2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    private void Update()
    {
        playerInputVector = ListenToPlayerInput(controlSettings.movementAxis);
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (IsGrounded())
            { Move(); }
        }
    }



    Vector2 ListenToPlayerInput(MovementAxis movementAxis)
    {
        Vector2 playerInputVector;
        switch (movementAxis)
        {
            case MovementAxis.x_Axis_1D:
                playerInputVector = new Vector2(Input.GetAxis("Horizontal"), 0);
                return playerInputVector;
            case MovementAxis.y_Axis_1D:
                playerInputVector = new Vector2(0, Input.GetAxis("Vertical"));
                return playerInputVector;
            case MovementAxis.xy_Axis_2D:
                playerInputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                return playerInputVector;
            default:
                return Vector2.zero;
        }      
    }

    //for use with physics engine at fixed update
    Vector2 CalculateDisplacementVectorFromPlayerInput(Vector2 playerInputVector)
    {
        if (Input.GetKey(controlSettings.runKeyModifier))
            return playerInputVector * physicalAttributes.runSpeed * Time.fixedDeltaTime;
        else
            return playerInputVector * physicalAttributes.moveSpeed * Time.fixedDeltaTime;
    }

    private void Move()
    {
        if (playerInputVector.x == 0)
        {
            if (isMoving)
            {
                isMoving = false;
                BroadcastMessage("StoppedMoving", SendMessageOptions.DontRequireReceiver);
            }
            return;
        }
        else
            isMoving = true;

        if (playerInputVector.x > 0)
        { BroadcastMessage("MoveRight", SendMessageOptions.DontRequireReceiver); }
        else if (playerInputVector.x < 0)
        { BroadcastMessage("MoveLeft", SendMessageOptions.DontRequireReceiver); }

        Vector2 newPosition = rigidbody_2D.position + CalculateDisplacementVectorFromPlayerInput(playerInputVector);
        rigidbody_2D.MovePosition(newPosition);
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, allGroundSurfaces);
    }
}

[Serializable]
public struct ControlSettings
{
    public KeyCode runKeyModifier;
    public KeyCode jumpKey;
    public MovementAxis movementAxis;
}

public enum MovementAxis
{
    x_Axis_1D,
    y_Axis_1D,
    xy_Axis_2D,
}

[Serializable]
public struct PhysicalAttributes
{
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;

}

public interface IMovementListener
{
     void MoveLeft();
     void MoveRight();
     void StoppedMoving();
}