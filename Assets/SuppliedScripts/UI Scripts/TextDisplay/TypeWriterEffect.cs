    using UnityEngine;
using System.Collections;
using TMPro;


namespace SECRIOUS.Effects { 
public static class TypeWriterEffect
{

    public static IEnumerator TypeWriterEffectRoutine(string fullText, float charsPerSecond, TextMeshProUGUI textMeshProUGUI)
    {
        //I want to know when this has finished.
        float pauseSecondsBetweenChars = 1 / charsPerSecond;
        string partialText;
        int maxIndex = fullText.Length;
        int currentIndex = 0;

        while (currentIndex < maxIndex)
        {
            partialText = fullText.Substring(0, currentIndex);
            textMeshProUGUI.text = partialText;
            currentIndex++;
            yield return new WaitForSeconds(pauseSecondsBetweenChars);
        }
        
        Debug.Log("Typewriter Coroutine has finished.");
    }


}



}