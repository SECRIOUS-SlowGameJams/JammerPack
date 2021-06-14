using System;
using UnityEngine;

namespace SECRIOUS.Utilities
{ 
public static class TimeConverter
{
    /// <summary>
    /// Returns the provided seconds in "mm\:ss" format.
    /// </summary>
    public static string FormatTimeSpan(float secondsum)
    {
        ///124.5f
        int minutes = Mathf.FloorToInt(secondsum / 60);
        //2
        int seconds = Mathf.FloorToInt(secondsum - minutes * 60);
        //4.5
        TimeSpan time = new TimeSpan(0, minutes, seconds);

        string timeSpanValue = time.ToString(@"mm\:ss");
        return timeSpanValue;
    }

    /// <summary>
    /// Converts string inputs from "mm\:ss" and "m\:ss" format to seconds.
    /// </summary>
    public static float ParseFormatedTimeSpan(string inputedTime)
    {
        TimeSpan time;
        if (TimeSpan.TryParseExact(inputedTime, @"mm\:ss", null, out time))
            return (float)time.TotalSeconds;
        else if (TimeSpan.TryParseExact(inputedTime, @"m\:ss", null, out time))
            return (float)time.TotalSeconds;
        else
            return 0;
    }

}
}