using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDuplicator    : MonoBehaviour
{
    public GameObject clone;
    public Transform container;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clone()
    {
        Clone(clone, container);
    }

     void Clone(GameObject clone, Transform parent)
    {
        Instantiate(clone, parent);
    }

}
