using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SECRIOUS.Utilities;

/// <summary>
/// This script implements a camera zoom in fixed increments. 
/// This is useful for having predefined zoom views for your camera. 
/// If you'd like a smoother camera zoom, please set the zoom levels to a high enough value for your scene set-up.
/// This script is set up for an orthographic camera. 
/// You can apply the same logic to a perspective camera by substituting orthographic size with field of view.
/// </summary>



[RequireComponent(typeof(Camera))]
public class CameraSmartZoom : MonoBehaviour
{
    [SerializeField]
    public ZoomSettings zoomSettings;


    Camera Camera;

    bool OrthoMode;
    float initialValue;
    float targetValue;
    float zoomDuration;

    bool DoZoomIn;
    bool isZooming;
    float t;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        OrthoMode = Camera.orthographic;
        CalculateZoomIncrementValue();
        CalculateZoomDuration();
        RegisterInitialValue();
    }

    //Start methods
    void CalculateZoomIncrementValue()
    {
        zoomSettings.zoomIncrement = (zoomSettings.sizeBounds.y - zoomSettings.sizeBounds.x) / zoomSettings.zoomLevels;
    }

    void CalculateZoomDuration()
    {
        zoomDuration = zoomSettings.zoomIncrement / zoomSettings.zoomSpeed;
    }

    void RegisterInitialValue()
    {
        if (OrthoMode)
            initialValue = Camera.orthographicSize;
        else
            initialValue = transform.position.y;
    }



    // Update is called once per frame
    void Update()
    {
        if (!isZooming)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                DoZoomIn = true;
                SmartZoom();
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                DoZoomIn = false;
                SmartZoom();
            }
        }
    }

    //Update methods
    void SmartZoom()
    {
        RegisterInitialValue();
        CalculateTargetValue();
        StartCameraZoom();
    }

    void CalculateTargetValue()
    {
        if (DoZoomIn)
            targetValue = initialValue + zoomSettings.zoomIncrement;
        else
            targetValue = initialValue - zoomSettings.zoomIncrement;
    }

    void StartCameraZoom()
    {
        if (OrthoMode)
        {
            ZoomOrthoCamera();
        }
        else
            ZoomPerspectiveCameraOnY();
    }

    void ZoomPerspectiveCameraOnY()
    {
        if (BoundsCheck.IsWithinBounds(zoomSettings.sizeBounds, targetValue))
        {
            isZooming = true;
            t = 0;
        }
    }

    void ZoomOrthoCamera()
    {
        if (BoundsCheck.IsWithinBounds(zoomSettings.sizeBounds, targetValue))
        {
            isZooming = true;
            t = 0;
        }
    }


    private void LateUpdate()
    {
        SmoothZoomFunction();
    }

    //Late update Methods
    void SmoothZoomFunction()
    {
        if (isZooming)
        {
            if (OrthoMode)
            {
              Camera.orthographicSize = Mathf.Lerp(initialValue, targetValue, t / zoomDuration);
            }
            else 
            { 
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(initialValue, targetValue, t / zoomDuration), transform.position.z);
            }
            t += Time.fixedDeltaTime;
            CameraZoomStop();
        }
    }

    void CameraZoomStop()
    {
        if (OrthoMode)
        {
            if (Mathf.Approximately(Camera.orthographicSize, targetValue))
            {
                Camera.orthographicSize = targetValue;
                isZooming = false;
            }
        }
        else
        {
            if (Mathf.Approximately(transform.position.y, targetValue))
            {
                transform.position = new Vector3(transform.position.x, targetValue, transform.position.z);
                isZooming = false;
            }
        }
    }



}


[Serializable]
public struct ZoomSettings
{
    [Tooltip("min and max values of orthographic size or of transform Y-height if in perspective mode")]
    public Vector2 sizeBounds;
    [Tooltip("amount of fixed zoom positions")]
    public int zoomLevels;
    public float zoomSpeed;
    [HideInInspector]
    public float zoomIncrement;
}