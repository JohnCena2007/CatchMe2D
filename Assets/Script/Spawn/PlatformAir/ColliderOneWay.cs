using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderOneWay : MonoBehaviour
{
    private PlayerMove player;
    private void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        player=FindObjectOfType<PlayerMove>();

        if(player.transform.position.y > GetComponent<YPosition>().Postion_Y){
            GetComponent<BoxCollider2D>().enabled=true;
        }else{
            GetComponent<BoxCollider2D>().enabled=false;
        }
    }
}
