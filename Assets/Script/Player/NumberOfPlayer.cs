using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOfPlayer : MonoBehaviour
{
    [SerializeField]public int numberSelect;
    [SerializeField]private AudioClip pickSound;
    public int price;
    private PlayerControl playerControl;
    public void Awake() {
        playerControl=FindObjectOfType<PlayerControl>();

        if(!PlayerPrefs.HasKey("lastButton")){
            if(numberSelect==0){
                transform.GetComponent<Outline>().enabled=true;
                PlayerPrefs.SetString("number" + numberSelect,"true");
                PlayerPrefs.SetInt("playerSelect",numberSelect);
                PlayerPrefs.SetInt("lastButton",numberSelect);

                for (int i = 0; i < playerControl.players.Length; i++)
                {
                    if(i  == PlayerPrefs.GetInt("playerSelect")){
                        playerControl.players[i].gameObject.SetActive(true);
                    }else{
                        playerControl.players[i].gameObject.SetActive(false);
                    }
                }
            }
        }else{
            if(numberSelect == PlayerPrefs.GetInt("lastButton")){
                transform.GetComponent<Outline>().enabled=true;
                PlayerPrefs.SetString("number" + numberSelect,"true");
                PlayerPrefs.SetInt("playerSelect",numberSelect);
                PlayerPrefs.SetInt("lastButton",numberSelect);
            }
        }
    }

    public void checkButton(){
        if(PlayerPrefs.GetString("number" + numberSelect) == "true" && PlayerPrefs.GetInt("lastButton") != numberSelect){
            //transform.GetChild(0).GetComponent<Text>().text="Equiped";
            SoundControl.instance.PlaySound(pickSound);
            transform.GetComponent<Outline>().enabled=true;
            playerControl.changeCharacter(numberSelect);
        }
        else if(PlayerPrefs.GetInt("coins") >= price){
                PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - price);
                PlayerPrefs.SetString("number" + numberSelect,"true");
        }
    }
    private void Update() {
        if(PlayerPrefs.GetString("number" + numberSelect) == "true")
            transform.GetChild(0).GetComponent<Text>().text="Equip";
    }

    public void changeText(int number){
        if(number==numberSelect)
            transform.GetChild(0).GetComponent<Text>().text="Equiped";
    }


}
