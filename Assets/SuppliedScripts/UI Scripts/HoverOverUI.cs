using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float cursorHoverOverDurationBeforePopUp;
    public GameObject popupOnHoverOver;
    
    [SerializeField]
    bool shouldPlayOnce = false;
    
    float timestamp;
    bool hasPlayedthisTime = false;
    bool hasPlayedOnce = false;
    bool isHovering;

    void OnEnable()
    {
        if (popupOnHoverOver != null)
        {
            DisableDescriptionPopUp();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isHovering)
        {
            if (shouldPlayOnce)
            {
                if (hasPlayedOnce)
                { return; }
            }

            if (hasPlayedthisTime)
            {
                return;
            }
            else
            {
                ShowAfterHoverOverDuration();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        timestamp = Time.time;
    }

    void ShowAfterHoverOverDuration()
    {
            if (Time.time > timestamp + cursorHoverOverDurationBeforePopUp)
            {
                EnableDescriptionPopUp();
            }
    }

    void EnableDescriptionPopUp()
    {
        popupOnHoverOver.SetActive(true);
        hasPlayedOnce = true;
        hasPlayedthisTime = true;
    }

    void DisableDescriptionPopUp()
    {
        popupOnHoverOver.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        hasPlayedthisTime = false;
        DisableDescriptionPopUp();
    }
}
