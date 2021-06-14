using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CanvasGroup))]
public class AutoHideTimedUI : MonoBehaviour
{

    public bool shouldFadeIn;
    public float fadeInDuration;
    
    public float stayActiveDuration;

    public bool shouldFadeOut;
    public float fadeOutDuration;


    CanvasGroup canvasGroup;
    float timeStamp;
    bool startedCountdown;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        if (shouldFadeIn)
        {
            StartCoroutine(FadeIn());
        }
        else
        {
            canvasGroup.alpha = 1;
            timeStamp = Time.time;
            startedCountdown = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startedCountdown)
        AutoHideAfter();
    }


    void AutoHideAfter()
    {
        if (Time.time > timeStamp + stayActiveDuration)
        {
            if (shouldFadeOut)
                StartCoroutine(FadeOut());
            else
            {
                startedCountdown = false; 
                gameObject.SetActive(false);
            }
        }
    }



    IEnumerator FadeIn()
    {
        for (float i = 0; i < fadeInDuration; i += Time.deltaTime)
        {
            canvasGroup.alpha = (i / fadeInDuration);
            yield return null;
        }
        canvasGroup.alpha = 1;
        timeStamp = Time.time;
        startedCountdown = true;
    }

    IEnumerator FadeOut()
    {
        for (float i = 0; i < fadeOutDuration; i += Time.deltaTime)
        {
            canvasGroup.alpha = 1 - (i / fadeOutDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
        startedCountdown = false;
        gameObject.SetActive(false);
    }
}
