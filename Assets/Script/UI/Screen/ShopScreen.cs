using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    [SerializeField]private Text coinText;
    private GameControl myGame;
    private void Awake() {
        myGame=FindObjectOfType<GameControl>();
    }
    void Update()
    {
        coinText.text=PlayerPrefs.GetInt("coins").ToString();
    }

    public void backDeathScreen(){
        myGame.shopsScreen.gameObject.SetActive(false);
    }
}
