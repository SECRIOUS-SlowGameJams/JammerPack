using UnityEngine;
using System.Collections;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SECRIOUS.Effects
{
    public class TypeWriterInstance : MonoBehaviour
    {
        public string message;
        public bool autoStart;
        
        string contents = "";

        //allows passing values to private variable from other scripts, but includes checks...
        //Can also include formatting styles. (to lower case, to string formats, etc
        public string Message
        {
            get { return contents; }
            set
            {
                if (contents.Length > 0)
                    contents = value;
            }

        }
        [SerializeField]
        float charsPerSecond;
        [SerializeField]
        float fastCharsPerSecond;

        float pauseSecondsBetweenChars;

        bool hasSpedUp = false;

        public event Action TypeWriterFinishedEvent;
        public event Action  TypeWriterStartedEvent;
        [SerializeField]
        TextMeshProUGUI textFrame;

        // Use this for initialization
        void Awake()
        {
            if (charsPerSecond == 0)
            {
                charsPerSecond = TypeWriterEffectMap.charsPerSecond;
            }
            if (fastCharsPerSecond == 0)
            {
                fastCharsPerSecond = TypeWriterEffectMap.fastCharsPerSecond;
            }
            ResetSpeed();
        }

        private void OnEnable()
        {
            if (autoStart)
            {
                StartTypeWriterEffect(message);
            }
        }

        void ResetFrame()
        {
            textFrame.text = "";
        }


        public void SpeedUp()
        {
            if (hasSpedUp)
            {
                StopCoroutine(coroutine);
                textFrame.text = contents;
                TypeWriterFinishedEvent?.Invoke();
            }
            else
            {
                pauseSecondsBetweenChars = 1 / fastCharsPerSecond;
               // Debug.Log("Sped things up");
                hasSpedUp = true;
            }
        }

        void ResetSpeed()
        {
            pauseSecondsBetweenChars = 1 / charsPerSecond;
            hasSpedUp = false;
        }
        Coroutine coroutine;
        void StartTypeWriterEffect(string message)
        {
            ResetFrame();
            ResetSpeed();
            contents = message;
            coroutine = StartCoroutine(TypeWriterEffectRoutine(message, textFrame));
            TypeWriterStartedEvent?.Invoke();
        }

        IEnumerator TypeWriterEffectRoutine(string textToDisplay, TextMeshProUGUI textMeshProUGUI)
        {
            string initialText = textMeshProUGUI.text;
            string partialText;
            int maxIndex = textToDisplay.Length;
            int currentIndex = 0;

            while (currentIndex < maxIndex + 1)
            {
                partialText = textToDisplay.Substring(0, currentIndex);
                textMeshProUGUI.text = initialText + partialText;
                currentIndex++;
                yield return new WaitForSeconds(pauseSecondsBetweenChars);
            }
            //I want to know when this has finished.
            coroutine = null;
            TypeWriterFinishedEvent?.Invoke();
        }


    }

}