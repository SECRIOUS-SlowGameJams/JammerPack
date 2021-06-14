using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DontDestroy : MonoBehaviour
{
    public static DontDestroy dontDestroyManager;

    private void Awake()
    {
        if (dontDestroyManager != null)
        {
            Destroy(this.gameObject);
        }
        else
            dontDestroyManager = this;
        DontDestroyOnLoad(dontDestroyManager);
    }
}
