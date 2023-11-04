using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    public Sprite imageInitial;
    private Vector3 initial_scale;
    private Animator ani;
    private BoxCollider2D box;
    private PlayerMove player;
    [SerializeField] LayerMask playerLayer;
    void Awake()
    {
        initial_scale=transform.localScale;
        ani=GetComponent<Animator>();
        box=GetComponent<BoxCollider2D>();
    }
    private void Update() {
        player=FindObjectOfType<PlayerMove>();

        if(isPlayer() && player.activePower)
            Destroy();
    }
    private void OnEnable() {
        GetComponent<BoxCollider2D>().enabled=true;
        transform.localScale=initial_scale;
        this.gameObject.GetComponent<SpriteRenderer>().sprite=imageInitial;
    }
    public void Destroy(){
        transform.localScale=new Vector3(6,6,6);
        ani.SetTrigger("destroyNow");
        GetComponent<BoxCollider2D>().enabled=false;
    }
    
    public void Deactive(){
        this.gameObject.GetComponent<SpriteRenderer>().sprite=imageInitial;
        gameObject.SetActive(false);
    }
    private bool isPlayer(){
        RaycastHit2D hit=Physics2D.BoxCast(box.bounds.center,box.bounds.size,0,Vector2.right,0.1f,playerLayer);
        return  hit.collider!=null;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Catcher"){
            Destroy();
        }
    }
}
