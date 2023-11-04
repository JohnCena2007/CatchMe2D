using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickUpSound;
    [SerializeField] private float powerBarToGive; //0.07 1 qua
    private TextConTrol textManage;
    private PowerUp power;
    void Start()
    {
        textManage=FindObjectOfType<TextConTrol>();
        power=FindObjectOfType<PowerUp>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            gameObject.SetActive(false);
            SoundControl.instance.PlaySound(coinPickUpSound);
            PlayerPrefs.SetInt("coins",PlayerPrefs.GetInt("coins") +1);
            textManage.addCoin(1);
            power.addPower(powerBarToGive);
        }
    }

}
