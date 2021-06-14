using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(PlayerAction))]
public class PlayerActionDrawer : PropertyDrawer
{
    SerializedProperty actionName;
    SerializedProperty actionPointCost;
    SerializedProperty action;
    SerializedProperty executeWithKeys;
    SerializedProperty actionkey;
    SerializedProperty executeWithButton;
    SerializedProperty button;
    SerializedProperty canTrigger;

    float totalHeight;
    Color color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.4f, 0.6f), Random.Range(0.8f, 1.0f));
    Color defaultColor;
    
    bool tof;


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return totalHeight;
    }

    [ContextMenu("Change Color")]
public Color SetActionColor()
    {    
       return Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.4f, 0.6f), Random.Range(0.8f, 1.0f));
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        actionName = property.FindPropertyRelative("actionName");
        actionPointCost = property.FindPropertyRelative("actionPointCost");
        action = property.FindPropertyRelative("action");
        executeWithKeys = property.FindPropertyRelative("executeWithKeys");
        actionkey = property.FindPropertyRelative("actionkey");
        executeWithButton = property.FindPropertyRelative("executeWithButton");
        button = property.FindPropertyRelative("button");
        canTrigger = property.FindPropertyRelative("canTrigger");

        label = EditorGUI.BeginProperty(position, label, property);



        int indentLevel = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0; 
        
        defaultColor = GUI.color;
        
        
        Rect colorbox = new Rect(position.x, position.y, 40, 20);
        color = EditorGUI.ColorField(colorbox, color);
     
        GUI.color = color;


        // GUI.backgroundColor = new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1), 1);



        
        float yValue = position.y+20;

        Rect newRect = new Rect(position.x, yValue, position.width, 
            EditorGUI.GetPropertyHeight(actionName, label, true));
        EditorGUI.PropertyField(newRect, actionName, new GUIContent("Name"));

        yValue += newRect.height + EditorGUIUtility.standardVerticalSpacing;



        newRect = new Rect(position.x, yValue, position.width,
        EditorGUI.GetPropertyHeight(actionPointCost, label, true));
        EditorGUI.PropertyField(newRect, actionPointCost, new GUIContent("Point Cost"));
        yValue += newRect.height + EditorGUIUtility.standardVerticalSpacing;

        newRect = new Rect(position.x, yValue, position.width,
            EditorGUI.GetPropertyHeight(action, label, true));
        EditorGUI.PropertyField(newRect, action, new GUIContent("Action"));
        yValue += newRect.height;

        newRect = new Rect(position.x, yValue, position.width,
        EditorGUI.GetPropertyHeight(executeWithKeys, label, true));
        EditorGUI.PropertyField(newRect, executeWithKeys, new GUIContent("Execute With Keys"));
        yValue += newRect.height + EditorGUIUtility.standardVerticalSpacing;

        if (executeWithKeys.boolValue)
        {
            newRect = new Rect(position.x, yValue, position.width,
       EditorGUI.GetPropertyHeight(actionkey, label, true));
            EditorGUI.PropertyField(newRect, actionkey, new GUIContent("Key"));
            yValue += newRect.height + EditorGUIUtility.standardVerticalSpacing;
        }

        newRect = new Rect(position.x, yValue, position.width,
        EditorGUI.GetPropertyHeight(executeWithButton, label, true));
        EditorGUI.PropertyField(newRect, executeWithButton, new GUIContent("Execute With UI Button"));
        yValue += newRect.height + EditorGUIUtility.standardVerticalSpacing;

        if (executeWithButton.boolValue)
        {
            newRect = new Rect(position.x, yValue, position.width,
       EditorGUI.GetPropertyHeight(button, label, true));
            EditorGUI.PropertyField(newRect, button, new GUIContent("Button"));
            yValue += newRect.height + EditorGUIUtility.standardVerticalSpacing;
        }


        newRect = new Rect(position.x, yValue, position.width, 20);
        tof = EditorGUI.BeginFoldoutHeaderGroup(newRect, tof, new GUIContent("Run-time data"));
        yValue += newRect.height + EditorGUIUtility.standardVerticalSpacing;

        if (tof)
        {
          
            newRect = new Rect(position.x, yValue, position.width, 20);
            EditorGUI.PropertyField(newRect, canTrigger, new GUIContent("Can use"));
            yValue += newRect.height + EditorGUIUtility.standardVerticalSpacing;
        }

        EditorGUI.EndFoldoutHeaderGroup();

        GUI.color = defaultColor;
        EditorGUI.indentLevel = indentLevel;

        totalHeight = yValue - position.y;

        EditorGUI.EndProperty();

    }



}
