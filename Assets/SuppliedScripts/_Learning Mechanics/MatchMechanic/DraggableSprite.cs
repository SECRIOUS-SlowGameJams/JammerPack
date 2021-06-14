using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * 
 */

    public class DraggableSprite : MonoBehaviour
    {
    /// Public Properties
    [HideInInspector]
    public bool isDragging;

    /// Serialized Fields for Editor
#pragma warning disable 0649
    [SerializeField]
     bool dragOnX;
    [SerializeField]
    bool dragOnY;
#pragma warning restore 0649


    ///  private Fields
    Vector2 initialdisplacement;

    ///  Private Methods
    private void OnMouseDown()
    {
        initialdisplacement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnMouseDrag()
    {
        isDragging = true;
        Vector2 currentPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - initialdisplacement;
        if (dragOnX && dragOnY)
        { transform.position = new Vector3(currentPosition.x, currentPosition.y, transform.position.z); }
        else if (dragOnX)
        {  transform.position = new Vector3(currentPosition.x, transform.position.y, transform.position.z); }
        else if (dragOnY)
        { transform.position = new Vector3(transform.position.x, currentPosition.y, transform.position.z); }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

}