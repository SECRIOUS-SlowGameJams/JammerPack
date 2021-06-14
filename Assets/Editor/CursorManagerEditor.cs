using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CursorManager))]
public class CursorManagerEditor : Editor
{
    bool groupEnabled = false;

    SerializedProperty simpleCursor;
    SerializedProperty specialCursor;
    SerializedProperty cursorHotSpotStyle;

    SerializedProperty spawnParticleOnClick;
    SerializedProperty ClickParticleSystem;

    SerializedProperty spawnParticleOnMove;
    SerializedProperty TrailParticleSystem;

    SerializedProperty spawnDistanceFromCamera;

    SerializedProperty debugMode;

    private void OnEnable()
    {
        simpleCursor = serializedObject.FindProperty("simpleCursor");
        specialCursor = serializedObject.FindProperty("specialCursor");
        cursorHotSpotStyle = serializedObject.FindProperty("cursorHotSpotStyle");
        spawnParticleOnClick = serializedObject.FindProperty("spawnParticleOnClick");
        spawnParticleOnMove = serializedObject.FindProperty("spawnParticleOnMove");


        ClickParticleSystem = serializedObject.FindProperty("ClickParticleSystem");
        TrailParticleSystem = serializedObject.FindProperty("TrailParticleSystem");
        spawnDistanceFromCamera = serializedObject.FindProperty("spawnDistanceFromCamera");

        debugMode = serializedObject.FindProperty("debugMode");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        CursorManager cursorManager = (CursorManager)target;

        EditorGUILayout.PropertyField(simpleCursor, new GUIContent("Simple Cursor"));
        EditorGUILayout.PropertyField(specialCursor, new GUIContent("Special Cursor"));
        EditorGUILayout.PropertyField(cursorHotSpotStyle, new GUIContent("Cursor Hotspot"));

        groupEnabled = EditorGUILayout.BeginFoldoutHeaderGroup(groupEnabled, "Optional Settings");
        //  groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);

        if (groupEnabled)
        {

            EditorGUILayout.PropertyField(spawnParticleOnClick, new GUIContent("Spawn Particle " +
                "οn Click"));

        if (spawnParticleOnClick.boolValue)
        EditorGUILayout.PropertyField(ClickParticleSystem, new GUIContent("Click Particle") );

        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(spawnParticleOnMove, new GUIContent("Spawn Particle on Move"));

       if (spawnParticleOnMove.boolValue)
        {
            EditorGUILayout.PropertyField(TrailParticleSystem, new GUIContent("Trail Particle"));
        }
 
        
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(spawnDistanceFromCamera, new GUIContent("Distance from Camera"));
      

        }

        EditorGUILayout.Separator();

        EditorGUILayout.PropertyField(debugMode, new GUIContent("Debug mouse data"));

        EditorGUILayout.EndFoldoutHeaderGroup();

       // EditorGUILayout.EndToggleGroup();


        serializedObject.ApplyModifiedProperties();
    }


}
