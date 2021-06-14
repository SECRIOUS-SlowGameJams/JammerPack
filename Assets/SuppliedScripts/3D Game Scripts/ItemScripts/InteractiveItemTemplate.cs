using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
 * 
 */


public class InteractiveItemTemplate : InteractiveObjectTemplate
{

    public bool pingIfInReach;
    public bool glowIfInSight;

    public AudioClip detectedAudio;
    [HideInInspector]
    public AudioSource audioSource;

    public GameObject visualFXparticle;
    [HideInInspector]
    public GameObject currentVisualFX;

    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


    //  public virtual void Awake()
    public virtual void Awake()
    {
        if (pingIfInReach)
        audioSource = GetComponent<AudioSource>();
    }

    public override void ItemSeenResponse()
    {
        if (glowIfInSight)
            GenerateVisualResponse();
    }

    public override void ItemReachedResponse()
    {
        if (pingIfInReach)
        {
            GenerateSoundResponse();
        }
    }


    public override void ItemUnSeenResponse()
    {
        CeaseVisualResponse();
    }

    public override void ItemOutOfReachResponse()
    {
        if (pingIfInReach)
        {
            CeaseSoundResponse();
        }
    }


    public override void Interact()
    {
    }

    ///  Public Methods

    ///  Private Methods


    void GenerateSoundResponse()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(detectedAudio);
        }
    }

    void CeaseSoundResponse()
    {
        audioSource.Stop();
    }

    void GenerateVisualResponse()
    {
        if (currentVisualFX == null)
        {
            GenerateParticleSystem();
        }
    }

    void CeaseVisualResponse()
    {
        if (currentVisualFX != null)
        { Destroy(currentVisualFX); }
    }

    void GenerateParticleSystem()
    {
        currentVisualFX = Instantiate(visualFXparticle, transform);
    }

}