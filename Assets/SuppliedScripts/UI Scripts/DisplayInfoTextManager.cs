using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInfoTextManager : MonoBehaviour
{
    public TextMeshProUGUI descriptionFrame;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayItemName(string name)
    {
        descriptionFrame.text = name;
    }

    public void DisplayItemDescription(string text)
    {

    }

    public void EmptyOutFrame()
    {
        descriptionFrame.text = "";
    }
}
