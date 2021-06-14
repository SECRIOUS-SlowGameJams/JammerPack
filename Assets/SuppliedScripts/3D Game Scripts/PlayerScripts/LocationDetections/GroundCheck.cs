using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

    public class GroundCheck : MonoBehaviour
    {
    /// Public Properties
    public LayerMask layerMask;

    public Transform feet;

    public bool isGrounded;

    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


    ///  private Fields

    ///  Unity CallBacks Methods
    private void Update()
    {
        
        //raycast downwards
    }


}
