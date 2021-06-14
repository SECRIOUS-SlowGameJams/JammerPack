using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CameraFreeMovement2D))]
public class CameraFreeMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CameraFreeMovement2D camera = (CameraFreeMovement2D)target;

        if (GUILayout.Button(new GUIContent("Focus camera on next selected gameobject")))
        {
            Selection.selectionChanged += FocusCamera;

            void FocusCamera()
            { 
                camera.gameObject.transform.position =
                new Vector3(Selection.activeGameObject.transform.position.x, Selection.activeGameObject.transform.position.y,
                camera.gameObject.transform.position.z);
            }
        }

    }
}
