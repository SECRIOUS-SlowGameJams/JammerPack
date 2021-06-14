using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SECRIOUS.Utilities;
using TMPro;
/*
 * This script is a bridge to tie UI elements to the TimePressure script. 
 * You can display info from the Time Pressure script on UI or you can call functions on the Time Pressure script from the UI.  
 */

[RequireComponent(typeof(TimePressure))]
public class TimePressureControls : MonoBehaviour, ITimePressureListener
{
    /// Public Properties
    public Button StartButton;
    public Toggle PauseToggle;
    public Button StopButton;


    public CanvasGroup extraButtonSet;
    public Button AddTimeButton;
    public Button RemoveTimeButton;

    public TMP_InputField TimeCounterInputField;
    public TMP_InputField ExtraTimeCounterInputField;
    public TextMeshProUGUI CurrentTimeDisplay;

    public CanvasGroup isOnDisplay;
    public CanvasGroup isSetDisplay;

    /// Serialized Fields for Editor
#pragma warning disable 0649
    [SerializeField]
    float disabledAlpha;
#pragma warning restore 0649


    ///  private Fields
    TimePressure timePressure;

    ///  Unity CallBacks Methods
    void Awake()
    {
        timePressure = GetComponent<TimePressure>();

        if (timePressure.timeCounter == 0) DisableAllControls();
        isOnDisplay.alpha = disabledAlpha;


        StartButton.onClick.AddListener(StartTimerFromUI);
        StopButton.onClick.AddListener(StopTimerFromUI);
        PauseToggle.onValueChanged.AddListener(TogglePauseTimerFromUI);
        TimeCounterInputField.onEndEdit.AddListener(SetCounterFromUI);

        AddTimeButton.onClick.AddListener(AddTimeToTimer);
        RemoveTimeButton.onClick.AddListener(RemoveTimeToTimer);
        extraButtonSet.alpha = disabledAlpha;
    }


    void Update()
    {
        CurrentTimeDisplay.text = (TimeConverter.FormatTimeSpan(timePressure.t));
    }


    ///  Public Methods
    void StartTimerFromUI()
    {
        timePressure.StartCounter();
    }

    void StopTimerFromUI()
    {
        timePressure.StopCounter();
    }

    void TogglePauseTimerFromUI(bool tof)
    {
        timePressure.TogglePauseCounter(tof);
    }

    void SetCounterFromUI(string timeinMMSSFormat)
    {
        float timeInSeconds = TimeConverter.ParseFormatedTimeSpan(timeinMMSSFormat);
        timePressure.SetCounter(timeInSeconds);
    }

    void AddTimeToTimer()
    {
        float seconds = TimeConverter.ParseFormatedTimeSpan(ExtraTimeCounterInputField.text);
        timePressure.AddTimeToCounter(seconds);
        TimeCounterInputField.SetTextWithoutNotify(TimeConverter.FormatTimeSpan(timePressure.timeCounter));
    }

    void RemoveTimeToTimer()
    {
        float seconds = TimeConverter.ParseFormatedTimeSpan(ExtraTimeCounterInputField.text);
        timePressure.RemovedTimeToCounter(seconds);
        TimeCounterInputField.SetTextWithoutNotify(TimeConverter.FormatTimeSpan(timePressure.timeCounter));
    }

    ///  Private Methods
    void DisableAllControls()
    {
        StartButton.interactable = false;
        StopButton.interactable = false;
        PauseToggle.interactable = false;
        extraButtonSet.alpha = disabledAlpha;
        isSetDisplay.alpha = disabledAlpha;
    }

    #region ITimePressureListener interface
    //these are message responses to the timepressure script
    public void OnTimerSet()
    {
        if (timePressure.timeCounter != 0)
        {
            StartButton.interactable = true;
            PauseToggle.interactable = false;
            StopButton.interactable = false;
            isSetDisplay.alpha = 1;
        }
        else
        {
            StartButton.interactable = false;
            PauseToggle.interactable = false;
            StopButton.interactable = false;
            isSetDisplay.alpha = disabledAlpha;
        }
    }

    public void OnTimerStart()
    {
        StartButton.interactable = false;
        PauseToggle.interactable = true;
        StopButton.interactable = true;
        extraButtonSet.alpha = 1;
        isOnDisplay.alpha = 1;
    }

    public void OnTimerPause()
    {
        StopButton.interactable = false;
    }

    public void OnTimerUnpause()
    {
        StopButton.interactable = true;
    }

    public void OnTimerStop()
    {
        StartButton.interactable = true;
        PauseToggle.interactable = false;
        StopButton.interactable = false;
        isOnDisplay.alpha = disabledAlpha;
        extraButtonSet.alpha = disabledAlpha;
    }

    #endregion

}
