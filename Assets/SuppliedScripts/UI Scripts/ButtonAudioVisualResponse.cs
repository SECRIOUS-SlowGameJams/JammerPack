using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SECRIOUS.Audio;


//This script is used to play a visual or audio response when a Selectable UI element is handled.
[RequireComponent(typeof(Selectable))]
public class ButtonAudioVisualResponse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

#pragma warning disable 649
    [SerializeField]
    AudioClip takesFocus;
    [SerializeField]
    public AudioClip losesFocus;
    [SerializeField]
    public AudioClip[] confirmOnClick;
    [SerializeField]
    public AudioClip cancelOnClick;
    [Header("ButtonMode")]
    [SerializeField]
    bool isConfirm = true;
    [SerializeField]
    bool silentExit = false;
    [SerializeField]
    bool mute = false;
    [SerializeField]
    Texture2D specialCursorTexture;

#pragma warning restore 649
    Selectable selectable;
    AudioManagerSoundEffects audioManagerSoundEffects;
    CursorManager cursorManager;
    void Awake()
    {
        selectable = GetComponent<Selectable>();
        cursorManager = FindObjectOfType<CursorManager>();
        audioManagerSoundEffects = FindObjectOfType<AudioManagerSoundEffects>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
       if (selectable.interactable)
        {
            if (!mute)
            {
                if (takesFocus != null)
                    audioManagerSoundEffects.PlaySoundEffectPriority(takesFocus);
            }
            //default version
            //
            //differentiated version
            if (specialCursorTexture != null)
                cursorManager.CursorChange(specialCursorTexture);
            else
                cursorManager.SpecialCursor();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectable.interactable)
        {
            if (!mute)
            {
                if (!silentExit)
                {
                    if (losesFocus != null)
                        audioManagerSoundEffects.PlaySoundEffectPriority(losesFocus);
                }
            }

            //default version
            //            //differentiated version
            if (specialCursorTexture != null)
                cursorManager.CursorChange(cursorManager.PreviousCursor);
            else
                cursorManager.SimpleCursor();
            //differentiated version
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       if (selectable.interactable)
        {
            if (!mute)
            {
                if (isConfirm)

                    if (confirmOnClick.Length != 0)
                    {
                        int randomIndex = UnityEngine.Random.Range(0, confirmOnClick.Length);
                        audioManagerSoundEffects.PlaySoundEffectPriority(confirmOnClick[randomIndex]);
                    }
            }
            else
            {
                if (cancelOnClick != null)
                {
                    audioManagerSoundEffects.PlaySoundEffectPriority(cancelOnClick);
                }
            }
        }
    }
}

