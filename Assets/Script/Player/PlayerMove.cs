using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header ("Catcher")]
    public CatcherControl anubis;

    [Header ("Camera")]
    [SerializeField]private Transform cameraControl;
    [SerializeField]private LayerMask groundLayer;

    [Header ("GameControl")]
    [SerializeField]private GameControl myGame;
    
    [Header ("Jump")]
    [SerializeField]public float jumpPower;
    [SerializeField]private float jumpExtra;

    [Header ("Speed")]
    [SerializeField]public float speed;
    [SerializeField]private float speedMilestone;
    [SerializeField]private float speedMultiple;
    private float speedMilestoneCount;


    [Header ("Sound")]
    [SerializeField]private AudioClip jumpSound;
    [SerializeField]private AudioClip doubleJumpSound;
    [SerializeField]private AudioClip resetSound;
    [SerializeField]private AudioClip gameOverSound;


    [Header ("Tranfrom Original")]
    public Transform playerOriginal;
    public float speedStore;
    public float speedMilestoneCountStore;
    public float speedMilestoneStore;    
    private bool canRoll;


    private TextConTrol textMange;
    private float initial_Pos_x;

    private PowerUp powerOfPLayer;
    public bool activePower=false;
    private float pos_x_before_active;
    [SerializeField] private Transform dash;


    private float jumpExtraCounter;
    public bool vaCham;

    [SerializeField]private BoxCollider2D box;
    private Rigidbody2D body;
    private Animator ani;

    public float initial_Gravity;
    public Vector3 playerStartPoint;
    
    private float countTime=0;
    private float posX_when_countTime;
    private bool canControl=true;

    private int midOrtop; //1 = Mid, 2=top;

    //Speed mac dinh la 14 dc cai dat o ham Start
    public void Start()
    {
        midOrtop=1;
        box=GetComponent<BoxCollider2D>();
        body=GetComponent<Rigidbody2D>();
        ani=GetComponent<Animator>();

        initial_Gravity=body.gravityScale;
        playerStartPoint =transform.position;


        speedMilestoneCount=speedMilestone;

        speedMilestoneCountStore=speedMilestoneCount;
        speedStore=14;
        speedMilestoneStore=speedMilestone;

        textMange=FindObjectOfType<TextConTrol>();
        initial_Pos_x=transform.position.x;

        powerOfPLayer=FindObjectOfType<PowerUp>();
    }
    private void Awake() {
        canControl=true;
    }


    void Update()
    {
        if(myGame.die==false){
            if(transform.position.y < cameraControl.transform.position.y - 22  || transform.position.y > cameraControl.transform.position.y + 22)
                StartCoroutine(playerOverDistanceOfCamera());
        }
        if(countTime == 0){
            posX_when_countTime=transform.position.x;
        }
        countTime += Time.deltaTime;
        if(countTime >= 0.045f){
            countTime = 0;
            while(transform.position.x == posX_when_countTime && GetComponent<SpriteRenderer>().enabled==true){
                transform.Translate(0.2f, 0.05f, 0);
            }
        }
        if(transform.position.x>speedMilestoneCount){
            speed*=speedMultiple;
            speedMilestone=speedMilestone * speedMultiple * 1.5f;
            speedMilestoneCount+=speedMilestone;
        }

        body.velocity=new Vector2(speed,body.velocity.y);
        ani.SetBool("grounded",isGround());
        textMange.scoreCounter=Mathf.Round(transform.position.x-initial_Pos_x);
        if(isGround()){
            ani.SetBool("doubleJump",false);
            jumpExtraCounter=jumpExtra;
        }

        if(canControl){
            //Jump
            if(Input.GetKeyDown(KeyCode.W)){
                Jump();
            }
            if(Input.GetKeyUp(KeyCode.W) && body.velocity.y >0){
                body.velocity=new Vector2(body.velocity.x,body.velocity.y/2);
            }

            //Roll
            if(Input.GetKeyDown(KeyCode.S) && !isGround()){
                ani.SetBool("roll",true);
                canRoll=true;
            }
            if(canRoll){
                if(isGround4f()){
                    transform.position=new Vector3(transform.position.x,transform.position.y,transform.position.z);
                    body.velocity=new Vector2(body.velocity.x,0);
                    transform.Translate(0,Time.deltaTime* (-0.01f),0);
                    if(!anubis.isGround()){
                        anubis.transform.Translate(0,-25*Time.deltaTime,0);
                        anubis.GetComponent<Animator>().SetBool("roll",true);
                    }
                }
                transform.Translate(0,-20*Time.deltaTime,0);

                if(!anubis.isGround())
                    anubis.transform.Translate(0,-25*Time.deltaTime,0);
                    anubis.GetComponent<Animator>().SetBool("roll",true);
            }
            //Use Power
            if(powerOfPLayer.powerFull.fillAmount > 0.7f){
                if(Input.GetKeyDown(KeyCode.Q)){
                    activePower=true;
                    pos_x_before_active=transform.position.x;
                }
            }
        }
        if(activePower){
            if(transform.position.x < pos_x_before_active + powerOfPLayer.moveDistance){
                transform.Translate(Time.deltaTime*speed,0,0);
                body.gravityScale=0;
                powerOfPLayer.decrease=true;
                dash.GetComponent<Animator>().SetTrigger("dashNow");
                ani.SetTrigger("dash");
                body.velocity= new Vector2(body.velocity.x,0);
            }else{
                if(PlayerPrefs.GetInt("playerSelect") == 0)
                    ani.Play("player_fall");
                else
                    ani.Play("player_fall" + PlayerPrefs.GetInt("playerSelect").ToString());
                body.gravityScale=initial_Gravity;
                powerOfPLayer.powerFull.color=Color.white;
                activePower=false;
            }
        }
    }

    private void Jump(){
        canRoll=false;
        ani.SetBool("roll",false);

        if(PlayerPrefs.GetInt("playerSelect") == 0)
            ani.Play("player_jump");
        else
            ani.Play("player_jump" + PlayerPrefs.GetInt("playerSelect").ToString());

        if(isGround()){
            //ani.SetBool("doubleJump",false);
            ani.SetBool("jump",true);
            body.velocity= new Vector2(body.velocity.x,jumpPower);
            anubis.Jump();
            SoundControl.instance.PlaySound(jumpSound);
        }else{
            if(jumpExtraCounter > 0){
                body.velocity= new Vector2(body.velocity.x,jumpPower);
                SoundControl.instance.PlaySound(doubleJumpSound);
                ani.SetBool("doubleJump",true);
                anubis.Jump();
                jumpExtraCounter--;
            }
        }
    }

    public bool isGround(){
        RaycastHit2D hit=Physics2D.BoxCast(box.bounds.center,box.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return  hit.collider!=null;
    }
    private bool isGround4f(){
        RaycastHit2D hit=Physics2D.Raycast(transform.position,Vector2.down,4f,groundLayer);
        return  hit.collider!=null;
    }

    private bool isObstacleFront(){
        RaycastHit2D hit=Physics2D.BoxCast(new Vector3(box.bounds.center.x,box.bounds.center.y +0.3f
        , box.bounds.center.z), box.bounds.size,0,Vector2.right,0.5f,groundLayer);
        if(hit.collider != null){
            //if (hit.transform.tag == GameObject.FindGameObjectWithTag("Obstacle")){
            if (hit.transform.tag == "Obstacle"){
                return  true;
            }
        }
        return false;
    }
//     private void OnDrawGizmos() {
//         Gizmos.color=Color.red;
//         Gizmos.DrawWireCube(new Vector3(box.bounds.center.x,box.bounds.center.y +0.2f, box.bounds.center.z)
//         ,box.bounds.size);
//    }
    private void OnCollisionEnter2D(Collision2D other) {
        canRoll=false;
        ani.SetBool("roll",false);

        if(activePower){
            if(other.gameObject.tag=="Obstacle"){
                other.gameObject.GetComponent<DestroyObstacle>().Destroy();
            }
        }else{
            if(isObstacleFront()){
                if(PlayerPrefs.GetInt("playerSelect") == 0)
                    ani.Play("player_dieByObstacleFront");
                else
                    ani.Play("player_dieByObstacleFront" + PlayerPrefs.GetInt("playerSelect").ToString());
                StartCoroutine(DieByObstacleFront());
            }
        }
        if(other.gameObject.tag == "PlatformBasic" || other.gameObject.tag == "KillPlatform")
            vaCham=false;
        else
            vaCham=true;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(activePower){
            if(other.gameObject.tag== "Portal"){
                Exchange(midOrtop);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        vaCham=false;
    }

    private void Exchange(int x){
        activePower=false;
        if(PlayerPrefs.GetInt("playerSelect") == 0)
            ani.Play("player_fall");
        else
            ani.Play("player_fall" + PlayerPrefs.GetInt("playerSelect").ToString());
        body.gravityScale=initial_Gravity;
        powerOfPLayer.resetPowerFull();

        if(x == 1){
            midOrtop = 2;
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color=new Color(255,255,255,255);
            anubis.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color=new Color(255,255,255,255);
            cameraControl.transform.position=new Vector3(transform.position.x,40,-20);
            transform.position=new Vector3(transform.position.x,45,transform.position.z);
            transform.GetChild(2).gameObject.GetComponent<Animator>().SetTrigger("appear");
            transform.GetChild(0).gameObject.GetComponent<Animator>().Play("dashbehind_idle");

        }else if(x == 2){
            midOrtop = 1;
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color=Color.green;
            anubis.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color=Color.green;
            cameraControl.transform.position=new Vector3(transform.position.x,4,-20);
            transform.position=new Vector3(transform.position.x,10,transform.position.z);
            transform.GetChild(2).gameObject.GetComponent<Animator>().SetTrigger("appear");
            transform.GetChild(0).gameObject.GetComponent<Animator>().Play("dashbehind_idle");

        }
    }
    public void resetPlayer(){
        if(midOrtop==2)
            midOrtop=1;
            cameraControl.transform.position=new Vector3(transform.position.x,4,-20);
            transform.GetChild(1).GetComponent<SpriteRenderer>().color=Color.green;
            anubis.transform.GetChild(1).GetComponent<SpriteRenderer>().color=Color.green;

        transform.GetChild(0).gameObject.GetComponent<Animator>().Play("dashbehind_idle");
        transform.GetChild(1).gameObject.GetComponent<Animator>().Play("posionbubble_idle");

        //PowerActive
        
        if(PlayerPrefs.GetInt("playerSelect") == 0)
            ani.Play("player_fall");
        else
            ani.Play("player_fall" + PlayerPrefs.GetInt("playerSelect").ToString());

        canControl=true;
        activePower=false;
        powerOfPLayer.resetPowerFull();

        //PosionKill
        SoundControl.instance.StopSound();
        SoundControl.instance.PlaySound(resetSound);
        SoundControl.instance.SetMusic(true);
        GetComponent<BoxCollider2D>().enabled=true; //Để không phát hiện sự va chạm nữa => Tránh việc canKillTrue lần nữa
        GetComponent<SpriteRenderer>().enabled=true;
        body.gravityScale=initial_Gravity;

        //ResetSpeed;
        speed=playerOriginal.GetComponent<PlayerMove>().speedStore;
        speedMilestoneCount=playerOriginal.GetComponent<PlayerMove>().speedMilestoneCountStore;
        speedMilestone=playerOriginal.GetComponent<PlayerMove>().speedMilestoneStore;

        transform.position=playerOriginal.GetComponent<PlayerMove>().playerStartPoint;
        cameraControl.GetComponent<CameraControl>().enabled=true;

    }
    public IEnumerator playerOverDistanceOfCamera(){
        myGame.allow=false;
        SoundControl.instance.SetMusic(false);
        cameraControl.GetComponent<CameraControl>().enabled=false;
        transform.position= playerOriginal.GetComponent<PlayerMove>().playerStartPoint;

        GetComponent<BoxCollider2D>().enabled=false; //Để không phát hiện sự va chạm nữa => Tránh việc canKillTrue lần nữa
        GetComponent<SpriteRenderer>().enabled=false;
        GetComponent<Rigidbody2D>().gravityScale=0;
        GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);

        yield return new WaitForSeconds(0.3f);
        myGame.die=true;
    }
    public IEnumerator DieByObstacleFront(){
        myGame.allow=false;
        //Anubis - de khi player chet thi no k di chuyen
        anubis.stopUpdate=true;
        anubis.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
        anubis.GetComponent<Animator>().enabled=false;

        //Player
        SoundControl.instance.PlaySound(gameOverSound);
        SoundControl.instance.SetMusic(false);
        canControl=false;
        GetComponent<PlayerMove>().speed=0;
        GetComponent<Rigidbody2D>().gravityScale=0;
        GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
        GetComponent<BoxCollider2D>().enabled=false;
        yield return new WaitForSeconds(1);
        myGame.die=true;
    }

}

