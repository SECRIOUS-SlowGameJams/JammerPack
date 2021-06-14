using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

//put this on Player Camera! works with layermasking...
public class PlayerViewPointRaycaster : MonoBehaviour
{
    //other than the layer
    public LayerMask layerMask;

    public bool hasObjectInSight;

    public static event Action<GameObject> ObjectDetectedEvent;
    public static event Action<GameObject> ObjectLostEvent;

    //I have reference to the item I have detected in 2 places, here
    public GameObject objectInFocus;

#pragma warning disable 0649
    [SerializeField]
    float detectionDistance;
#pragma warning restore 0649

    // Update is called once per frame
    void Update()
    {
        RayCaster(layerMask);
    }

    Ray RayCaster(LayerMask layerMask)
    {
        //from the camera's position
        Vector3 startingPosition = transform.position;
        //forward from camera's orientation
        Vector3 direction = transform.forward;

       // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

       Ray ray = new Ray(startingPosition, direction);

        RaycastHit raycastHit;

        //Show me that ray in blue
        Debug.DrawRay(startingPosition, direction * detectionDistance, Color.blue, 1f, true);
        //Debug.DrawRay(ray.origin, ray.direction, Color.blue);

        //If I hit anything in the layermask declared within the distance limit
        if (Physics.Raycast(ray, out raycastHit, detectionDistance, layerMask))
        {
            //Show me that ray in red
            Debug.DrawRay(startingPosition, direction * detectionDistance, Color.red, 1f, true);
           // Debug.DrawRay(ray.origin, ray.direction, Color.red);

            //If this is the first item or is not the same item with previous frame
            if (raycastHit.collider.gameObject != objectInFocus)
            {
                Debug.Log("This is a new item you are looking at: " + raycastHit.collider.gameObject.name);
                //raise an event
                if (ObjectDetectedEvent != null)
                {
                    ObjectDetectedEvent(raycastHit.collider.gameObject);
                }
                //store the new item
                objectInFocus = raycastHit.collider.gameObject;
                hasObjectInSight = true;
                // Debug.Log("I remember this item: " + objectInFocus.name);
            }
        }
        //else there is no item I m looking at.
        else
        {
            if (objectInFocus != null)
            {
                if (ObjectLostEvent != null)
                {
                    ObjectLostEvent(objectInFocus);
                    Debug.Log("Lost sight of object " + objectInFocus.name);
                }
            }
            objectInFocus = null;
            hasObjectInSight = false;
        }
        return ray;
    }



    void RayDebug()
    {
        //from the camera's position
        Vector3 startingPosition = transform.position;
        //forward from camera's orientation
        Vector3 direction = transform.forward;
        Debug.DrawRay(startingPosition, direction * detectionDistance, Color.blue, 1f, true);
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying == false)
        {
            RayDebug();
        }
#endif

    }
}

