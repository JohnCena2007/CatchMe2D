using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    private TextConTrol txt;
    [SerializeField]private Text textScore;
    [SerializeField]private Text textCoin;

    private GameControl myGame;

    private void Awake() {
        txt=FindObjectOfType<TextConTrol>();
        myGame=FindObjectOfType<GameControl>();
    }
    public void backToMenu(){
        myGame.die=false;
        SoundControl.instance.SetMusic(true);
        FindObjectOfType<TextConTrol>().coinCounter=0;
        SceneManager.LoadScene(0);
    }
    public void deathReset(){
        gameObject.SetActive(false);
        Time.timeScale=1;
        FindObjectOfType<TextConTrol>().coinCounter=0;
        FindObjectOfType<GameControl>().Reset();
    }
    public void goShop(){
        myGame.shopsScreen.gameObject.SetActive(true);
    }
    private void OnEnable() {
        textScore.text=txt.score.text.Substring(0,txt.score.text.Length-1);
        textCoin.text="Coins: " + PlayerPrefs.GetInt("coins").ToString();
    }
}
