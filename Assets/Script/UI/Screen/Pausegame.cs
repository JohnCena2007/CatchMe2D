using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausegame : MonoBehaviour
{  
    private GameControl myGame;
    private void Awake() {
        myGame=FindObjectOfType<GameControl>();
    }
    public void PauseGame(){
        gameObject.SetActive(true);
        Time.timeScale=0;
    }
    public void ResumeGame(){
        Time.timeScale=1;
        gameObject.SetActive(false);
    }
    public void pauseReset(){
        gameObject.SetActive(false);
        Time.timeScale=1;
        FindObjectOfType<TextConTrol>().coinCounter=0;
        FindObjectOfType<GameControl>().Reset();
    }
    public void QuitToMain(){
        myGame.die=false;
        SoundControl.instance.SetMusic(true);
        FindObjectOfType<TextConTrol>().coinCounter=0;
        Time.timeScale=1;
        SceneManager.LoadScene(0);
    }
}
