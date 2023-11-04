using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]private Transform playGame;
    [SerializeField]private Transform instuct;
    [SerializeField]private Transform title;
    [SerializeField]private Transform creator;


    private void Awake() {
        transform.GetChild(3).gameObject.SetActive(false);
        playGame.gameObject.SetActive(true);
        instuct.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
        creator.gameObject.SetActive(true);
    }
    public void entryInstructDetail(){
        playGame.gameObject.SetActive(false);
        instuct.gameObject.SetActive(false);
        title.gameObject.SetActive(false);

        transform.GetChild(3).gameObject.SetActive(true);
    }
    public void exitInstructDetail(){
        playGame.gameObject.SetActive(true);
        instuct.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(false);
    }
    public void loadGame(){
        SceneManager.LoadScene(1);
    }
}

    
