using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WoodPoisionControl : MonoBehaviour
{
    [SerializeField]private float speedFallDown;
    [SerializeField]private float initial_y;
    [SerializeField]private SupportWoodPosion parentWood;
    //private CatcherControl anubis;

    private bool ok=false;
    private float rotZ=0;

    private float rotZ_initial;
    private void Start() {
        rotZ_initial=transform.rotation.z;
    }
    // private void Awake() {
    //     anubis=FindObjectOfType<CatcherControl>();
    // }
    public void ResetWood() {
        ok=false;
        transform.position=new Vector3(transform.position.x,initial_y + parentWood.transform.position.y,transform.position.z);
        transform.rotation=Quaternion.Euler(0,0,rotZ_initial);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player" || other.gameObject.tag=="Catcher"){
            ok=true;
        }
    }
    private void Update() {
        if(ok){
            rotZ += Time.deltaTime*speedFallDown;
            if(transform.rotation.z <= 30 ){
            transform.rotation = Quaternion.Euler(0,0,rotZ);
            }
            if(transform.position.y > initial_y - 100){
                transform.position=new Vector3(transform.position.x,transform.position.y - Time.deltaTime * speedFallDown * 0.03f,transform.position.z);
            }
        }
    }


}
