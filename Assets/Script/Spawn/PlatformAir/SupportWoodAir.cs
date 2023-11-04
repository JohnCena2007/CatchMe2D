using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportWoodAir : MonoBehaviour
{
    public WoodAirControl[] woodChildControl;
    private PlayerMove player;
    public bool canFallDown;

    //Thay đổi skip này khi mà platform water in air dc enable thì nó sẽ get child của nó và gọi đến hàm
    //on enable của thg này nhưng thay onenable bằng tên khác

    //Trong platform air sẽ có 1 thg wood ông quản lý tất cả thg wood cha, trong thg wood cha là một 
    //đống wood con. Khi mà platform air onenealbe thì nó sẽ getchild(0) là wood ông, script của wood ông
    //sẽ có tất cả [serifialaz]supportWoodPosion của wood cha, wood ông sẽ dùng vòng for để gọi 
    //tất cả hàm onenable nhưng với tên khác của wood cha
    //80 0.5440359
    public void resetWoodInChild() {
        for (int i = 0; i < woodChildControl.Length; i++)
        {
            woodChildControl[i].ResetWood();
        }
    }
    void Awake()
    {
        //player=FindObjectOfType<PlayerMove>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            player.transform.Translate(Time.deltaTime * 2,0,0);
        }
    }
    private void Update() {
        player=FindObjectOfType<PlayerMove>();

        if(GetComponent<BoxCollider2D>().enabled){
            canFallDown=true;
        }else{
            canFallDown=false;
        }
    }
}
