using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ButtonURLink : MonoBehaviour
{
    Button button;
    public string url;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenURL);
    }


    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
