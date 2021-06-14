using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DieRollerMultiUI : MonoBehaviour
{
    //set this scriptable to use the die types and textures you like
    public DieSetScriptableObj DieSetScriptableObj;
    //this is the initial die type 
    public DieType dieType;
    public Button die;
    public int rollResult;

    //show result on UI
    public TextMeshProUGUI rollResultFrame;

    //option expansion
    [SerializeField]
    Image image;
    bool isMultiDie;
    public TMP_Dropdown dropdown;


    private void Awake()
    {
        image = GetComponent<Image>();
        //die.onClick.AddListener(Roll);

        if (dropdown != null)
        dropdown.onValueChanged.AddListener(ChangeDieType);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isMultiDie)
        UpdateVisual();  
    }

    public void Roll()
    {
        rollResult = UnityEngine.Random.Range(1, (int)dieType + 1);
        rollResultFrame.text = rollResult.ToString();
    }


    public void ChangeDieType(int index)
    {
        string valueName = dropdown.options[index].text;
        valueName = valueName.ToLower();

        if (Enum.TryParse<DieType>(valueName, out dieType))
        {
            UpdateVisual();
        }
    }

    void UpdateVisual()
    {
        foreach (var die in DieSetScriptableObj.dies)
        {
            if (die.die == dieType)
            {
                Sprite newSprite = Sprite.Create(die.image, image.sprite.rect, image.sprite.pivot);
                image.sprite = newSprite;
            }
        }
    }

    public void DisableDieRoll()
    {
        die.interactable = false;
    }
    public void EnableDieRoll()
    {
        die.interactable = true;
    }
}


