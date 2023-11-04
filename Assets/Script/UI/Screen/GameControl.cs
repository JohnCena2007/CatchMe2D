using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{ 
    [Header ("Catcher")]
    public CatcherControl anubis;

    public Transform GeneratorTop;
    public Transform GeneratorMid;
    private Vector3 platFormPointTop;
    private Vector3 platFormPointMid;

    private PlayerMove player;

    private PlatformDestroy[] platFromDestroyList;
    private PlatformControl[] platFromControlList;

    private TextConTrol textC;

    [Header ("Screen")]
    public Transform deathScreen;
    public Transform pauseScreen;
    public Transform shopsScreen;
    public Transform basicScreen;


    [Header ("Camera")]
    public Transform cameraMain;
    public bool canPause;
    public bool die;
    private bool trueOrFalse=false;
    private float countTime=0;
    public bool stillDoing=false;
    public bool allow=true;
    void Start()
    {
        //player=FindObjectOfType<PlayerMove>();
        //anubis=FindObjectOfType<CatcherControl>();
        platFormPointTop= GeneratorTop.transform.position;
        platFormPointMid= GeneratorMid.transform.position;
        textC=FindObjectOfType<TextConTrol>();

        deathScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);
        shopsScreen.gameObject.SetActive(false);
        SoundControl.instance.SetMusic(true);
    }
    private void Awake() {
        allow=true;
    }

    private void Update() {
        player=FindObjectOfType<PlayerMove>();

        countTime+=Time.deltaTime;
        if(countTime > 30f && trueOrFalse == false && stillDoing == false && !die){
                //Debug.Log("move back");
                anubis.GetComponent<CatcherControl>().enabled=true;
                anubis.appearOrNot(false); //Bien mat
        }
        if(countTime > 30f && trueOrFalse == true && stillDoing == false && !die){
                //Debug.Log("xuat hien");
                anubis.GetComponent<CatcherControl>().enabled=true;
                anubis.appearOrNot(true); //Xuat hien tro lai
        }


        if(Input.GetKeyDown(KeyCode.Escape) && canPause && allow){
            pauseScreen.GetComponent<Pausegame>().PauseGame();
        }

        if(deathScreen.gameObject.activeInHierarchy)
            canPause=false;
        else
            canPause=true;
        
        if(die){
            deathScreen.gameObject.SetActive(true);
            player.GetComponent<SpriteRenderer>().enabled=false;
            player.speed=0;
            cameraMain.GetComponent<CameraControl>().enabled=false;

            anubis.stopUpdate=true;
            anubis.moveBack=false;
            anubis.GetComponent<Animator>().enabled=false; //Tat cai hieu ung run di
            anubis.speed=0;
        }
    }

     public void Reset(){
        die=false;
        allow=true;
        //cameraMain.GetComponent<CameraControl>().enabled=true;
        SoundControl.instance.SetMusic(true);
        cameraMain.transform.position = new Vector3(0,4,-20);
        deathScreen.gameObject.SetActive(false);
        platFromDestroyList=FindObjectsOfType<PlatformDestroy>();
        platFromControlList=FindObjectsOfType<PlatformControl>();
        for (int i = 0; i < platFromDestroyList.Length; i++)
        {
            platFromDestroyList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < platFromControlList.Length; i++)
        {
            platFromControlList[i].resetPlatformControl();
        }

        player.resetPlayer();
        anubis.GetComponent<CatcherControl>().enabled=true;
        anubis.GetComponent<CatcherControl>().resetCatcher();
        anubis.transform.GetChild(1).GetComponent<Animator>().Play("posionbubble_idle");
        stillDoing=false; countTime =0; trueOrFalse=false;

        GeneratorMid.transform.position=platFormPointMid;
        GeneratorTop.transform.position=platFormPointTop;
        player.gameObject.SetActive(true);
}

public void updateTrueOrFalse(bool x){
    stillDoing=false;
    if(x){
        countTime=0;
        trueOrFalse = true;
    }else{
        countTime=0;
        trueOrFalse = false;
    }
}
}