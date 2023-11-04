using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{
    public Transform platformGenerationPoint;
    public ObjectPooler[] platformsPoolerControl;
    public ObjectPooler[] propsPoolerControl;
    public ObjectPooler[] obstaclesPoolerControl;
    public ObjectPooler[] platformAirsControl;
    public ObjectPooler[] portalControl;




    private CoinGenerate coinControl;
    private float[] platformWidths;
    private bool checkPosionBehindAndNow; //Kiem tra độc, nếu đằng sau có độc thì sẽ không spawn prop
    private bool notSpawnPosion =false;
    private bool notSpawnAnything=false;

    private int countOfSpawn=0;
    private int needBlock=0;
    private int needPosion=0;
    private int needBlockCounter;



    private int lastIndextSelect=0; //Để cho các prop dc spam sẽ không lập lại 2 lần liên tiếp
    private int prop_Or_Obstacle=0; //0 là prop, 2 là obstacle
    void Start()
    {
        coinControl=FindObjectOfType<CoinGenerate>();
        platformWidths=new float[platformsPoolerControl.Length];
        for (int i = 0; i < platformWidths.Length; i++)
        {
            platformWidths[i]=platformsPoolerControl[i].poolObject.GetComponent<BoxCollider2D>().size.x;
        }
    }

    
    void Update()
    {
        if(transform.position.x < platformGenerationPoint.position.x){
            //Generate Platform
            int platFormSelect=Random.Range(0,platformsPoolerControl.Length);
            if(countOfSpawn==0)
                platFormSelect=0; //Để cho lần đầu tiên spawn platform thì luôn là platform ô gạch để di chuyển
            countOfSpawn++;
            
            if(notSpawnPosion){
                while(platformsPoolerControl[platFormSelect].getPoolObject().gameObject.tag=="KillPlatform"){
                    platFormSelect=Random.Range(0,platformsPoolerControl.Length);
                }
                notSpawnPosion=false;
            }

            if(needBlock>0){
                platFormSelect=0;
                needBlockCounter=needBlock;
                needBlock--;
            }else if(needPosion>0){
                needPosion--;
                while(platformsPoolerControl[platFormSelect].getPoolObject().gameObject.tag!="KillPlatform"){
                    platFormSelect=Random.Range(0,platformsPoolerControl.Length);
                }
            }
            needBlockCounter--;

            transform.position=new Vector3(transform.position.x + platformWidths[platFormSelect]/2,transform.position.y,transform.position.z);
            GameObject newPlatform=platformsPoolerControl[platFormSelect].getPoolObject();
            newPlatform.transform.position=transform.position;
            newPlatform.transform.rotation=transform.rotation;
            newPlatform.SetActive(true);
        
            if(newPlatform.gameObject.tag == "KillPlatform")
                checkPosionBehindAndNow=true;

            //Choose object spawn in the next time is prop or obstacle
            prop_Or_Obstacle = Random.Range(0,2);
            //Generate portal
            if(Random.Range(0,100) < 20){
                prop_Or_Obstacle=3;
            }
            if(countOfSpawn > 0 && countOfSpawn % 5 ==0){
                if(Random.Range(0,100) < 85){
                    prop_Or_Obstacle=2;
                }
            }

            if(notSpawnAnything){
                notSpawnAnything=false;
            }else{
                switch(prop_Or_Obstacle){
                    case 0:
                        //Generate Prop
                        int propSelect=Random.Range(0,propsPoolerControl.Length);
                        if(newPlatform.gameObject.tag != "KillPlatform" && !checkPosionBehindAndNow){
                            if(Random.Range(0,100) < 70){
                                while(lastIndextSelect == propSelect){
                                    propSelect=Random.Range(0,propsPoolerControl.Length);
                                }
                                GameObject newProp= propsPoolerControl[propSelect].getPoolObject();
                                newProp.transform.position=new Vector3(transform.position.x - platformWidths[platFormSelect]/2,newProp.GetComponent<YPosition>().Postion_Y,transform.position.z);
                                //Vì prop lấy vị trí "tranfrom.pos.x + platformWidths[platFormSelect]/2 " để spam nên
                                //ta cần trừ đi "platformWidths[platFormSelect]/2" để nó spawn ngay giữa platform
                                newProp.SetActive(true);
                                lastIndextSelect=propSelect;
                            }
                        }
                        //Generate coin
                            if(Random.Range(0,100) < coinControl.TanSoSpawnCoin && needBlockCounter < -2 && newPlatform.gameObject.tag != "KillPlatform" ){
                                coinControl.spawnCoin(new Vector3(transform.position.x - platformWidths[platFormSelect]/2
                                ,transform.position.y,transform.position.z));
                            }
                            
                        break;
                    
                    case 1:
                        //Generate Obstacle
                        int obstacleSelect=Random.Range(0,obstaclesPoolerControl.Length);
                        if(newPlatform.gameObject.tag != "KillPlatform" && !checkPosionBehindAndNow){
                            if(Random.Range(0,100) < 50){
                                GameObject newObstacle= obstaclesPoolerControl[obstacleSelect].getPoolObject();
                                newObstacle.transform.position=new Vector3(transform.position.x - platformWidths[platFormSelect]/2,newObstacle.GetComponent<YPosition>().Postion_Y,transform.position.z);
                                newObstacle.SetActive(true);
                            }
                        }
                        break;
                    
                    case 2:
                        //Generate Platform Air
                        int platformAirSelect= Random.Range(0,platformAirsControl.Length);
                            if(newPlatform.gameObject.tag != "KillPlatform"){
                                notSpawnPosion=true;
                                notSpawnAnything=true;
                                GameObject newPlatformAir = platformAirsControl[platformAirSelect].getPoolObject();
                                if(newPlatformAir.gameObject.name.Contains("needBlock") || newPlatformAir.gameObject.name.Contains("needPosion"))
                                    newPlatformAir.transform.position=new Vector3(transform.position.x + platformWidths[platFormSelect],newPlatformAir.GetComponent<YPosition>().Postion_Y,transform.position.z);
                                else
                                    newPlatformAir.transform.position=new Vector3(transform.position.x - platformWidths[platFormSelect]/2,newPlatformAir.GetComponent<YPosition>().Postion_Y,transform.position.z);
                                newPlatformAir.SetActive(true);

                                if(newPlatformAir.gameObject.name.Contains("needBlock"))
                                    needBlock=3;
                                else if(newPlatformAir.gameObject.name.Contains("needPosion"))
                                    needPosion=3;
                            }
                        break;
                    case 3:
                        //Generate Portal
                        int portalSelect= Random.Range(0,portalControl.Length);
                        if(newPlatform.gameObject.tag != "KillPlatform" && !checkPosionBehindAndNow){
                                GameObject newPortal= portalControl[portalSelect].getPoolObject();
                                newPortal.transform.position=new Vector3(transform.position.x - platformWidths[platFormSelect]/2,transform.position.y + 3.6f,transform.position.z);
                                newPortal.SetActive(true);
                        }
                        break;
                }
            }
            transform.position=new Vector3(transform.position.x +platformWidths[platFormSelect]/2,transform.position.y,transform.position.z);
            if(newPlatform.gameObject.tag != "KillPlatform")
                checkPosionBehindAndNow=false;
        }
    }
    public void resetPlatformControl(){
        notSpawnPosion=false;
        notSpawnAnything=false;
        checkPosionBehindAndNow=false;
        countOfSpawn=0;
        needBlock=0;
        needPosion=0;
        needBlockCounter=0;
    }
}
//Độ cao mà box in air phải trên 35.4