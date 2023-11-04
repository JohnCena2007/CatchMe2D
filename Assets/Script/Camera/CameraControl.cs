using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private PlayerMove player;
    private Vector3 lastPlayerPos;
    private float distanceBetween;
    void Start()
    {
        player=FindObjectOfType<PlayerMove>();
        lastPlayerPos.x=player.transform.position.x;
        Debug.Log("HI");
        Debug.Log("ad");
    }

    void Update()
    {
        player=FindObjectOfType<PlayerMove>();
        transform.position= new Vector3(player.transform.position.x+12,transform.position.y,transform.position.z);
        // distanceBetween=transform.position.x - lastPlayerPos.x;
        // Debug.Log(transform.position);
        // transform.position =new Vector3(transform.position.x + distanceBetween,transform.position.y,transform.position.z);
        // lastPlayerPos.x=player.transform.position.x;
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private PlayerControl player;
    private Vector3 lastPlayerPos;
    private float distanceMove;
    // Start is called before the first frame update 
    void Start()
    {
        player=FindObjectOfType<PlayerControl>();
        lastPlayerPos=player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceMove=player.transform.position.x - lastPlayerPos.x;
        transform.position=new Vector3(transform.position.x + distanceMove,transform.position.y,transform.position.z);
        lastPlayerPos=player.transform.position;
    }
}
*/