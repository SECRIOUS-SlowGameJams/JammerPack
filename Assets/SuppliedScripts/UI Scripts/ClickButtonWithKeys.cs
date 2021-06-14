using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickButtonWithKeys : MonoBehaviour
{
    public KeyCode[] keys;
    Button thisButton;
    // Start is called before the first frame update
private void Awake()
    {
        thisButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        ClickWithKeys();
    }

    void ClickWithKeys()
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                thisButton.onClick.Invoke();
            }
        }
    
    }
}
