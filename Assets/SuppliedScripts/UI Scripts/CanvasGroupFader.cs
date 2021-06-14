using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFader : MonoBehaviour
{

    [SerializeField]
    bool fadeOnEnable = false;

    public float startingAlpha;
    public float endAlpha;

    [SerializeField]
    bool bringToFront = false;
    [SerializeField]
    bool sendToBack = false;

    public float fadeDuration;
    public CanvasGroup canvasgroup;


    // Use this for initialization
    private void Awake()
    {
        canvasgroup = GetComponent<CanvasGroup>();

        if (bringToFront)
        {
            //sets this on top of everything else;
            transform.SetAsLastSibling();
        }
        else if (sendToBack)
        {
            transform.SetAsFirstSibling();
        }
    }

    void OnEnable()
    {
        if (fadeOnEnable)
        {
            PreSetFade();
        }
    }


    [ContextMenu("RunFade")]
    public void PreSetFade()
    {
        StartCoroutine(FadeAlphaFromTo(canvasgroup, startingAlpha, endAlpha, fadeDuration));
    }


    void FadeFromBlack()
    {
        StartCoroutine(FadeAlphaFromTo(canvasgroup, 1, 0, fadeDuration));
    }

    public void FadeFromBlack(float duration)
    {
        StartCoroutine(FadeAlphaFromTo(canvasgroup, 1, 0, duration));
    }

    void FadeToBlack()
    {
        StartCoroutine(FadeAlphaFromTo(canvasgroup, 0, 1, fadeDuration));
    }

    public void FadeToBlack(float duration)
    {
        StartCoroutine(FadeAlphaFromTo(canvasgroup, 0, 1, duration));

    }
    public void BlinkFader(float stayWithBlack)
    {
        IEnumerator FadeFromTo()
        {
            FadeToBlack();
            yield return new WaitForSeconds(fadeDuration + stayWithBlack);
            FadeFromBlack();
        }

        StartCoroutine(FadeFromTo());
    }

    public static IEnumerator FadeAlphaFromTo(CanvasGroup canvasGroup, float startingAlpha, float endAlpha, float durationInSeconds)
    {
        //in case it wasn't, set alpha:
        canvasGroup.alpha = startingAlpha;

        for (float t = 0; t < durationInSeconds; t += Time.deltaTime)
        {
            float currentalpha = Mathf.Lerp(startingAlpha, endAlpha, t / durationInSeconds);
            canvasGroup.alpha = currentalpha;
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}

