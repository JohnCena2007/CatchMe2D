using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WoodAirControl : MonoBehaviour
{
    [SerializeField]private float speedFallDown;
    [SerializeField]private SupportWoodAir parentWood;
    private float initial_y;

    private bool ok;
    private float rotZ=0;

    private float rotZ_initial;
    private void Start() {
        rotZ_initial=transform.rotation.z;
        initial_y =transform.position.y;
    }
    public void ResetWood() {
        ok=false;
        transform.position=new Vector3(transform.position.x,GetComponent<YPosition>().Postion_Y,transform.position.z);
        transform.rotation=Quaternion.Euler(0,0,rotZ_initial);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            ok=true;
        }
    }
    private void Update() {
        if(parentWood.canFallDown && ok){
            rotZ += Time.deltaTime*speedFallDown;
            if(transform.rotation.z <= 30 ){
            transform.rotation = Quaternion.Euler(0,0,rotZ);
            }
            if(transform.position.y > GetComponent<YPosition>().Postion_Y - 100){
                transform.position=new Vector3(transform.position.x,transform.position.y - Time.deltaTime * speedFallDown * 0.03f,transform.position.z);
            }
        }
    }


}
