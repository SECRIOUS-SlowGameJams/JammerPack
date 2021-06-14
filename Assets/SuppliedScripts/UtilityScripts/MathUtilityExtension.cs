using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * 
 */

namespace SECRIOUS.Utilities
{
    public static class MathUtilityExtension
    {
        public static int Modulus(int a, int n)
        {
            int result = a % n;
            if ((result < 0 && n > 0) || (result > 0 && n < 0))
            {
                result += n;
            }
            return result;
        }


        public static float FormatFloatToDecimals(float input, int decimalPoints)
        {
            float multiplier = Mathf.Pow(10, decimalPoints);

            return (float)Math.Round(input * multiplier) / multiplier;
        }

    }
}