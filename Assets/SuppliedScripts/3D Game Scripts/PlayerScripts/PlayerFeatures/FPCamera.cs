using UnityEngine;
using System.Collections;
using System;
using System.Windows;


//this script affects the camera in first person mode. 
//The camera is a child object to the player gameObject.
//It follows the mouse movement and has zooming options, within a predefined range.
//Movement of the mouse on the X axis (screen coordinate system) rotates the parent gameobject itself (and thus the camera too) around the Y axis  (world coordinate system), 
//while movement of the mouse on the Y axis (screen coordinate system) rotates the camera on the X axis (world coordinate system).
//Finally, the values are clamped so that the camera can't look behind the player.

//The script is inspired by the Brackeys tutorial ' FIRST PERSON MOVEMENT in Unity - FPS Controller '.
//https://www.youtube.com/watch?v=_QajrabyTJc


public class FPCamera : MonoBehaviour
{
    public Camera followCamera;
    public Transform playerTransform;

#pragma warning disable 0649
    [SerializeField]
    float mouseSensitivity;
    [SerializeField]
    float startingFoV;
    [SerializeField]
    float angleRestrictionDownwards;
    [SerializeField]
    float angleRestrictionUpwards;
#pragma warning restore 0649

    [Tooltip("For debugging only")]
    [SerializeField]
    float rotationAroundXaxis;
    [Tooltip("For debugging only")]
    public Vector2 mousePosition;

    float playerRotationAroundY;
    Vector3 cameraRotationEuler;

    private void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {
        followCamera.fieldOfView = startingFoV;
    }

    // Update is called once per frame
    void Update()
    {

        //angles are calculated here:
        PlayerTurnPrep();
        CameraLookAroundPrep();

        mousePosition = Input.mousePosition;
    }


    private void LateUpdate()
    {
        //execute on LateUpdate
        //actual turn of player
        playerTransform.Rotate(Vector3.up * playerRotationAroundY, Space.Self);
        //actual turn of camera
        followCamera.transform.localRotation = Quaternion.Euler(cameraRotationEuler);
        //transform.rotation *= Quaternion.AngleAxis(rotationAroundXaxis, Vector3.right);
        ClampCamera();
    }


    void PlayerTurnPrep()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float rotationAroundYaxis = mouseX * Time.fixedDeltaTime * mouseSensitivity;
        playerRotationAroundY = rotationAroundYaxis;
    }

    void CameraLookAroundPrep()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        rotationAroundXaxis = mouseY * Time.fixedDeltaTime * mouseSensitivity;
        //i want invert controls
        rotationAroundXaxis = -rotationAroundXaxis;
        rotationAroundXaxis = followCamera.transform.localEulerAngles.x + rotationAroundXaxis;

        //this works instead!!!
        if (rotationAroundXaxis > 0 && rotationAroundXaxis < 180)
        {
            rotationAroundXaxis = Mathf.Clamp(rotationAroundXaxis, 0, 180);
        }
        else if (rotationAroundXaxis > 180 && rotationAroundXaxis < 360)
        {
            rotationAroundXaxis = Mathf.Clamp(rotationAroundXaxis, 270, 360);
        }

        cameraRotationEuler = new Vector3(rotationAroundXaxis, 0, 0);
    }


    void ClampCamera()
    {
        if (followCamera.transform.localEulerAngles.x > angleRestrictionDownwards && followCamera.transform.localEulerAngles.x <= 90)
        {
            followCamera.transform.localRotation = Quaternion.Euler(angleRestrictionDownwards, followCamera.transform.localRotation.y, followCamera.transform.localRotation.z);
        }
        
        if (followCamera.transform.localEulerAngles.x >= 270 && followCamera.transform.localEulerAngles.x < angleRestrictionUpwards)
        {
            followCamera.transform.localRotation = Quaternion.Euler(angleRestrictionUpwards, followCamera.transform.localRotation.y, followCamera.transform.localRotation.z);
        }

    }

}
