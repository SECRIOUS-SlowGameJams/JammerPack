using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFreeMovement2D : MonoBehaviour
{
    [SerializeField]
    bool canMove;
    public float moveSpeed;
    public MovementAxis movementAxis;
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        Move();
    }

    void Move()
    {
        Vector3 playerInput = ListenToPlayerInput(movementAxis) * moveSpeed * Time.deltaTime;
        Vector3 potentialPosition = transform.position + playerInput;
        transform.position = potentialPosition;
    }

    Vector3 ListenToPlayerInput(MovementAxis movementAxis)
    {
        Vector3 playerInputVector;
        switch (movementAxis)
        {
            case MovementAxis.x_Axis_1D:
                playerInputVector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                return playerInputVector;
            case MovementAxis.y_Axis_1D:
                playerInputVector = new Vector3(0, Input.GetAxis("Vertical"), 0);
                return playerInputVector;
            case MovementAxis.xy_Axis_2D:
                playerInputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
                return playerInputVector;
            default:
                return Vector2.zero;
        }
    }
    
    public void ToggleCameraMovement (bool tof)
    {
        canMove = tof;
    }

}
