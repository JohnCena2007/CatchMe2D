using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loxo : MonoBehaviour
{ 
    [SerializeField]private float jump_height;
    [SerializeField]private float move_distance;
    [SerializeField]private float speedMove;

    private PlayerMove player;
    private Vector3 player_pos;
    private bool jumpping=false;
    private float thoigian;

    private bool ok1=false;
    private bool ok2=false;
    private float gravity;
    private bool khongLapLai=false;
    void Start()
    {
        player=FindObjectOfType<PlayerMove>();
        gravity= player.GetComponent<Rigidbody2D>().gravityScale;
    }

    private void Update() {
        player=FindObjectOfType<PlayerMove>();

        thoigian+=Time.deltaTime;
        if(jumpping){
            if(player.transform.position.x < player_pos.x + move_distance){
                player.transform.Translate(Time.deltaTime*speedMove*0.5f,0,0);
            }else{
                ok1=true;
            }

            if(player.transform.position.y < player_pos.y + jump_height*2){
                if(player.transform.position.x <= player_pos.x + move_distance/2){
                    player.transform.Translate(0,Time.deltaTime*speedMove,0);
                }else{
                    if(player.transform.position.y > player_pos.y + 0.5f){
                        player.transform.Translate(0,-Time.deltaTime*speedMove*0.7f,0);
                    }else{
                        ok2=true;
                    }
                }
            }
            if(player.vaCham){
                jumpping=false;
                ok1=false;
                ok1=false;
                //player.GetComponent<Rigidbody2D>().gravityScale=gravity;
            }

        }

        if(ok1 && ok2){
            jumpping=false;
            ok1=false;
            ok1=false;
            thoigian=0;
            //player.GetComponent<Rigidbody2D>().gravityScale=gravity;
        }
        if(thoigian >0.8f && khongLapLai){
            khongLapLai=false;
            if(PlayerPrefs.GetInt("playerSelect") == 0)
                player.GetComponent<Animator>().Play("player_fall");
            else
                player.GetComponent<Animator>().Play("player_fall" + PlayerPrefs.GetInt("playerSelect").ToString());
            //player.GetComponent<Rigidbody2D>().gravityScale=gravity;
            jumpping=false;
            ok1=false;
            ok1=false;
            thoigian=0;
        }

    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            if(player.activePower==false){
                thoigian=0;
                jumpping=true;
                player_pos=player.transform.position;
                //player.GetComponent<Rigidbody2D>().gravityScale=0;
            }
        }
    }
}
