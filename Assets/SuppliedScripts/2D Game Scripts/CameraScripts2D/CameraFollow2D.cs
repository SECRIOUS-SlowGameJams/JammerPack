using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//if you want to combine this script with camera movement constraints, please call the constraint method on LateUpdate after FocusCamera.
public class CameraFollow2D : MonoBehaviour
{

    /// Public Properties
    public Transform target;
    public MovementAxis followAxis;

    void LateUpdate()
    {
        CameraFollow();
    }

    ///  Private Methods
    void CameraFollow()
    {
        switch (followAxis)
        {
            case MovementAxis.x_Axis_1D:
                transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
                break;
            case MovementAxis.y_Axis_1D:
                transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
                break;
            case MovementAxis.xy_Axis_2D:
                transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
                break;
        }
    }

}