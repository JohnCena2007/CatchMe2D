using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherControl : MonoBehaviour
{
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private Transform appearPoint;
    public GameControl myGame;
    public Transform playerOriginal;
    [Header ("Sound")]
    public AudioClip impactSound;


    private PlayerMove player;
    private float jumpPow;
    public float speed;

    private Rigidbody2D body;
    private BoxCollider2D box;
    private Animator ani;

    private Vector3 initial_pos;
    public bool stopUpdate=false;
    public bool moveBack=false;
    void Start()
    {
        jumpPow=playerOriginal.GetComponent<PlayerMove>().jumpPower;
        speed=playerOriginal.GetComponent<PlayerMove>().speed;

        body=GetComponent<Rigidbody2D>();
        box=GetComponent<BoxCollider2D>();
        ani=GetComponent<Animator>();

        initial_pos=transform.position;
        myGame=FindObjectOfType<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        player=FindObjectOfType<PlayerMove>();
        if(!myGame.die){
            ani.SetBool("onGround",isGround());
            if(stopUpdate == false){
                speed=player.speed;
                body.velocity=new Vector2(speed,body.velocity.y);
            }
            if(moveBack){
                //Debug.Log("come3");
                body.velocity=new Vector2(speed-5f,body.velocity.y);
                if(transform.position.x < player.transform.position.x - 30 || Input.GetKeyDown(KeyCode.X)){
                    //Debug.Log("come4");
                    moveBack=false;
                    stopUpdate=false;
                    transform.position = new Vector3(0,-30,0);
                    myGame.updateTrueOrFalse(true);
                    //Debug.Log("come5");
                }

            }


            if(transform.position.x < player.transform.position.x - 14){
                transform.Translate(speed * Time.deltaTime * 0.1f,0,0);
            }
            else if(transform.position.x > player.transform.position.x - 14)
                transform.Translate(-speed * Time.deltaTime * 0.5f,0,0);

            if(player.isGround() && !this.isGround()){
                transform.Translate(0,-speed * Time.deltaTime * 0.3f,0);
            }
        }
    }
    public void Jump(){
        body.velocity= new Vector2(body.velocity.x,jumpPow);
    }

    public bool isGround(){
        ani.SetBool("roll",false);
        RaycastHit2D hit=Physics2D.BoxCast(box.bounds.center,box.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return  hit.collider!=null;
    }
    public void resetCatcher(){
        stopUpdate=false;
        moveBack=false;
        GetComponent<Rigidbody2D>().gravityScale=playerOriginal.GetComponent<PlayerMove>().initial_Gravity  ;
        GetComponent<SpriteRenderer>().enabled=true;
        GetComponent<CatcherControl>().enabled=true;
        GetComponent<BoxCollider2D>().enabled=true;
        GetComponent<Animator>().enabled=true;
        transform.position=initial_pos;
    }
    public void appearOrNot(bool status){
        //Reset Catcher if he dead
        stopUpdate=false;
        moveBack=false;
        GetComponent<Rigidbody2D>().gravityScale=playerOriginal.GetComponent<PlayerMove>().initial_Gravity  ;
        GetComponent<SpriteRenderer>().enabled=true;
        GetComponent<CatcherControl>().enabled=true;
        GetComponent<BoxCollider2D>().enabled=true;
        GetComponent<Animator>().enabled=true;

        //Debug.Log("come1");
        myGame.stillDoing=true;
        if(status == false){
            //Debug.Log("come2");
            stopUpdate=true;
            moveBack=true;
        }else{
            transform.position= new Vector3(appearPoint.transform.position.x,appearPoint.transform.position.y,0);
            transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("appear");
            myGame.updateTrueOrFalse(false);
        }
    }
    public void dieByPosionSupport(){
        StartCoroutine(dieByPosion());
    }
    private IEnumerator dieByPosion(){
        GetComponent<Rigidbody2D>().velocity=new Vector2(5,0);
        stopUpdate=true;
        yield return new WaitForSeconds(0.5f);
        SoundControl.instance.PlaySound(impactSound);
        GetComponent<Rigidbody2D>().gravityScale=0;
        GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
        GetComponent<SpriteRenderer>().enabled=false;
        transform.GetChild(1).GetComponent<Animator>().SetTrigger("spawn");
        GetComponent<BoxCollider2D>().enabled=false;
        myGame.stillDoing=false;
        GetComponent<CatcherControl>().enabled=false;
    }
}
