using UnityEngine;
using UnityEditor;

public class MenuItems
{
    //To create a hotkey you can use the following special characters: % (ctrl on Windows, cmd on macOS), # (shift), & (alt). 

    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }


}


public class CustomEditorWindowSample : EditorWindow
{
    [MenuItem("Tools/Custom Editor Window")]
    static void ShowCustomWindow()
    {
        EditorWindow.GetWindow(typeof(CustomEditorWindowSample));

    }


}