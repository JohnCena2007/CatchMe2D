using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroy : MonoBehaviour
{
    private GameObject platformDestructionPoint;
    void Start()
    {
        platformDestructionPoint=GameObject.Find("PlatformDestructionPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <platformDestructionPoint.transform.position.x){
            gameObject.SetActive(false);
        }
    }
}
