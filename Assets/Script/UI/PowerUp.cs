using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField] public Image powerZero;
    [SerializeField] public Image powerFull;
    [SerializeField] private Transform player;
    public float moveDistance;
    public bool decrease=false;
    private float powerFullCounter=0;


    void Update()
    {
        if(decrease==false){
            if(powerFull.fillAmount<=0.7f){
                powerFull.fillAmount=powerFullCounter;
            }
            else if(powerFull.fillAmount >0.7f){
                powerFull.fillAmount=1;
                powerFull.color=Color.red;
            }
        }

        if(decrease){
            powerFullCounter=0;
            if(powerFull.fillAmount>0){
                powerFull.fillAmount -= Time.deltaTime;
            }else{
                decrease=false;
            }
        }
    }
    public void addPower(float x){
        if(decrease==false)
            powerFullCounter+=x;
    }
    public void resetPowerFull(){
        powerFull.fillAmount=0;
        decrease=false;
        powerFullCounter=0;
        powerFull.color=Color.white;
    }

}
