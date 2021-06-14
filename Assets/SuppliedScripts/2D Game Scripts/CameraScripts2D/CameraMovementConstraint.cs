using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * 
 */

[RequireComponent(typeof(Camera))]
public class CameraMovementConstraint : MonoBehaviour
    {
        /// Public Properties
        public SpriteRenderer fullView;

        [SerializeField]
        bool constraintOnX;
        [SerializeField]
        bool constraintOnY;

        float displayAspectRatioWH;
        Bounds fullViewAreaBounds;

        Vector2 safeMin;
        Vector2 safeMax;
        Camera Camera;
        /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


        ///  private Fields


        ///  Unity CallBacks Methods
        void Awake()
        {
            Camera = GetComponent<Camera>();
            fullViewAreaBounds = fullView.bounds;
            displayAspectRatioWH = Screen.width / (float) Screen.height;
        }

        void Start()
        {
            CalculateFrame();
        }

        private void LateUpdate()
        {
            ConstrainCameraWithinBounds();
        }

    ///  Private Methods
    void CalculateFrame()
        {
            safeMin.x = fullViewAreaBounds.min.x + Camera.orthographicSize * displayAspectRatioWH;
            safeMax.x = fullViewAreaBounds.max.x - Camera.orthographicSize * displayAspectRatioWH;

            safeMin.y = fullViewAreaBounds.min.y + Camera.orthographicSize;
            safeMax.y = fullViewAreaBounds.max.y - Camera.orthographicSize;
        }

        void ConstrainCameraWithinBounds()
        {
        //for cameras that can zoom, you need to redo this at every frame;
            CalculateFrame();
            transform.position = ConstrainMovement(transform.position);
        }

        Vector3 ConstrainMovement(Vector3 potentialPosition)
        {
      
        float x, y;
        Vector3 finalPosition = transform.position;

        if (constraintOnX && constraintOnY)
        {
             x = Mathf.Clamp(potentialPosition.x, safeMin.x, safeMax.x);
             y = Mathf.Clamp(potentialPosition.y, safeMin.y, safeMax.y);
            finalPosition = new Vector3(x, y, transform.position.z);
        }
        else if (constraintOnX)
        { 
             x = Mathf.Clamp(potentialPosition.x, safeMin.x, safeMax.x);
            finalPosition = new Vector3(x, transform.position.y, transform.position.z);
        }
        else if (constraintOnY)
        { 
            y = Mathf.Clamp(potentialPosition.y, safeMin.y, safeMax.y);
            finalPosition = new Vector3(transform.position.x, y, transform.position.z);

        }
        return finalPosition;

    }


    //for use in editor view
    [ContextMenu("ZoomToFullViewHeight")]
    void ZoomToFullViewHeight()
    {
        Camera = GetComponent<Camera>();
        Camera.orthographicSize = fullViewAreaBounds.size.y / 2;
    }

    //for use in editor view -- this thing doesn't draw the screen height properly from the editor...
    [ContextMenu("ZoomToFullViewWidth")]
    void ZoomToFullViewWidth()
    {
        float maxWidth = fullViewAreaBounds.size.x;
        float aspectRatioHW = (float)Screen.height / (float)Screen.width;
        float maxHeight = aspectRatioHW * maxWidth;
        Camera = GetComponent<Camera>();
        Camera.orthographicSize = maxHeight / 2;
    }
}
