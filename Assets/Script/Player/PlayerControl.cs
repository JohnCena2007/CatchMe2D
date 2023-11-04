using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject[] players;

    private void Awake() {
        players[0].GetComponent<PlayerMove>().Start();
        buttons[0].GetComponent<NumberOfPlayer>().Awake();
        for (int i = 0; i < players.Length; i++)
        {
            if(i  == PlayerPrefs.GetInt("playerSelect")){
                players[i].gameObject.SetActive(true);
            }else{
                players[i].gameObject.SetActive(false);
            }
        }
    }
    public void changeCharacter(int x){
        buttons[PlayerPrefs.GetInt("lastButton")].GetComponent<Outline>().enabled=false; // Hủy outline của button cũ
        PlayerPrefs.SetInt("playerSelect",x);
        PlayerPrefs.SetInt("lastButton",x);     //Cập nhật lại lastbutton để lần sau hủy tiếp

        for (int i = 0; i < players.Length; i++)
        {
            if(i  == PlayerPrefs.GetInt("playerSelect")){
                players[i].gameObject.SetActive(true);
            }else{
                players[i].gameObject.SetActive(false);
            }
        }
    }
}
