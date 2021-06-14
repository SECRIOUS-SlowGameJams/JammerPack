using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * 
 */
[RequireComponent(typeof(Toggle))]
public class ΤοggleModification : MonoBehaviour
    {

    Toggle toggle;

        ///  Unity CallBacks Methods
        void Awake()
        {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(SwapSprites);
         }

        ///  Private Methods
        void SwapSprites(bool tof)
    {
        toggle.targetGraphic.gameObject.SetActive(!tof);
        toggle.graphic.gameObject.SetActive(tof);
    }

    }