using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportWoodPosion : MonoBehaviour
{
    public WoodPoisionControl[] woodChildControl;
    private PlayerMove player;
    private CatcherControl anubis;
    private BoxCollider2D box;
    private bool isPlayerCollider;
    private bool isCatcherCollider;
    //Thay đổi skip này khi mà platform water in air dc enable thì nó sẽ get child của nó và gọi đến hàm
    //on enable của thg này nhưng thay onenable bằng tên khác

    //Trong platform air sẽ có 1 thg wood ông quản lý tất cả thg wood cha, trong thg wood cha là một 
    //đống wood con. Khi mà platform air onenealbe thì nó sẽ getchild(0) là wood ông, script của wood ông
    //sẽ có tất cả [serifialaz]supportWoodPosion của wood cha, wood ông sẽ dùng vòng for để gọi 
    //tất cả hàm onenable nhưng với tên khác của wood cha

    private void OnEnable() {
        box.enabled=true;
        isPlayerCollider=false;
        isCatcherCollider=false;
        for (int i = 0; i < woodChildControl.Length; i++)
        {
            woodChildControl[i].ResetWood();
        }
    }
    void Awake()
    {
        anubis=FindObjectOfType<CatcherControl>();
        box=GetComponent<BoxCollider2D>();
    }
    private void Update() {
        player=FindObjectOfType<PlayerMove>();
        if(isCatcherCollider && isPlayerCollider){
            player.transform.Translate(Time.deltaTime,0.01f,0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            player.transform.Translate(Time.deltaTime * 2,0,0);
            isPlayerCollider=true;
        }
        if(other.gameObject.tag == "Catcher"){
            isCatcherCollider=true;
            box.enabled=false;
            anubis.dieByPosionSupport();
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
            isPlayerCollider=false;
        if(other.gameObject.tag == "Catcher")
            isCatcherCollider=false;
    }
}
