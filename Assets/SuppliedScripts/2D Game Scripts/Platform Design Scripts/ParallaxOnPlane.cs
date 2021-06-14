using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxOnPlane : MonoBehaviour
{
    public float IntensityModifier;

    /// Public Properties
    public Camera Camera;

    public Vector3 initialPosition;
    public Vector3 initialCameraPosition;

    [Tooltip("Assumes camera-plane distance on Z-Axis.")]
    public float planeDistance;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialCameraPosition = Camera.transform.position;
        planeDistance = (initialCameraPosition - initialPosition).z;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDisplacement();
    }

    void CalculateDisplacement()
    {
        Vector3 currentPositionalDifference = Camera.transform.position - initialCameraPosition;
        Vector3 displacementVector = new Vector3(currentPositionalDifference.x, currentPositionalDifference.y, 0) * IntensityModifier * planeDistance;
        transform.position = initialPosition + displacementVector;
    }

}
