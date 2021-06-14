using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//This script implements a timer. You can set the duration, reduce or extend it on the fly, pause, stop and reset the counter. 
//You can also assign events to be triggered when the timer's up. 
//See TimePressureControls for connecting this script to UI elements.
public class TimePressure : MonoBehaviour
{
    //timecounter's duration
    public float timeCounter;
    //current timeshot
    public float t;
    //what to trigger when the timer's up
    public UnityEvent eventWhenTimerIsUp;

    [SerializeField]
    bool isCounting;

    // Update is called once per frame
    void Update()
    {
        if (isCounting)
        {
            DoTimeCounting();
        }
    }

    ///  Public Methods

    public void SetCounter(float seconds)
    {
        timeCounter = seconds;
        SendMessage("OnTimerSet", SendMessageOptions.DontRequireReceiver);
    }

    public void ResetCounter()
    {
        timeCounter = 0;
    }


    public void StartCounter()
    {
        isCounting = true;
        SendMessage("OnTimerStart", SendMessageOptions.DontRequireReceiver);
    }

    public void StopCounter()
    {
        isCounting = false;
        t = 0;
        SendMessage("OnTimerStop", SendMessageOptions.DontRequireReceiver);
    }

    public void TogglePauseCounter(bool tof)
    {
        isCounting = !tof;
        if (isCounting)
            SendMessage("OnTimerUnpause", SendMessageOptions.DontRequireReceiver);
        else
            SendMessage("OnTimerPause", SendMessageOptions.DontRequireReceiver);
    }

    public void AddTimeToCounter(float seconds)
    {
        SetCounter(timeCounter + seconds);
    }
    public void RemovedTimeToCounter(float seconds)
    {
        SetCounter(timeCounter - seconds);
    }

    //private methods
    void DoTimeCounting()
    {
        t += Time.deltaTime;
        CheckForTimeOver();

        void CheckForTimeOver()
        {
            if (t >= timeCounter)
            {
                eventWhenTimerIsUp?.Invoke();
                StopCounter();
            }
        }
    }


}

interface ITimePressureListener
{
    void OnTimerSet();
    void OnTimerStart();
    void OnTimerPause();
    void OnTimerUnpause();
    void OnTimerStop();
}