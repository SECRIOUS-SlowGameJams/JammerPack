using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * 
 */

namespace SECRIOUS.Utilities
{ 
public static class BoundsCheck
{

    public static bool IsWithinBounds(Bounds bounds, Vector3 value)
    {
        return bounds.Contains(value);
    }


    public static bool IsWithinBounds(Vector2 bounds, float value)
    {
        return ((value > bounds.x) && (value < bounds.y));
    }

    public static bool ExceededMaximum(Vector2 bounds, float value)
    {
        return (value > bounds.y);
    }

    public static bool ExceededMinimum(Vector2 bounds, float value)
    {
        return (value < bounds.x);
    }
}

[Serializable]
//this is just a more informative version of Bounds for use in the editor
public class Bounds_Extended
    {

    public Bounds_Extended(Bounds Bounds)
    {
        bounds = Bounds;
        InitializeValues();
    }

    public Bounds bounds;

    public Vector2 size;
    public Vector2 min;

    public float minX;
    public float minY;

    public float maxX;
    public float maxY;

    public void InitializeValues()
    {
        minX = bounds.center.x - bounds.extents.x;
        minY = bounds.center.y - bounds.extents.y;

        maxX = bounds.center.x + bounds.extents.x;
        maxY = bounds.center.y + bounds.extents.y;
        size = bounds.size;
        min = bounds.min;
    }

}
}