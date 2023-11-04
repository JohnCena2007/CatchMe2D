using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextConTrol : MonoBehaviour
{
    [SerializeField]public Text amounOfCoin;
    [SerializeField]public Text score;
    private GameControl myGame;
    public int coinCounter;
    public float scoreCounter;
    private void Awake() {
        myGame=FindObjectOfType<GameControl>();
        amounOfCoin.text="0";
        score.text="0 m";
    }

    void Update()
    {
        if(myGame.die){
            coinCounter=0;
            scoreCounter=0;
        }
        amounOfCoin.text=coinCounter.ToString();
        score.text=Mathf.Round(scoreCounter/1.5f).ToString() + "m";
    }
    public void addCoin(int x){
        coinCounter+=x;
    }

}
