using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    private AudioSource soundSource;
    public static SoundControl instance {get;private set;}
    void Awake()
    {
        soundSource=GetComponent<AudioSource>();
        if(instance == null)
            instance=this;
            DontDestroyOnLoad(gameObject);
        if(instance != null && instance != this){
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip x){
        soundSource.PlayOneShot(x);
    }
    public void SetMusic(bool status){
        transform.GetChild(0).gameObject.SetActive(status);
    }
    public void StopSound(){
        soundSource.Stop();
    }
    
    

}
