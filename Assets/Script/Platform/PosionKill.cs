using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionKill : MonoBehaviour
{
    private GameControl myGame;
    public float speedTranlate_X;
    public AudioClip gameOverSound;
    public AudioClip impactSound;

    [SerializeField]private Transform firstPoint;
    [SerializeField]private Transform LastPoint;
    private PlayerMove player;
    private CatcherControl anubis;

    private bool canTranslatePlayer=false;
    private bool canTranslateAnubis=false;
    private void Start() {
        //player=FindObjectOfType<PlayerMove>();
        myGame=FindObjectOfType<GameControl>();
        anubis=FindObjectOfType<CatcherControl>();
    }
    private void Update() {
        player=FindObjectOfType<PlayerMove>();

        if(canTranslatePlayer){
            player.GetComponent<PlayerMove>().speed=0; //Để cho player k dc di chuyển nữa thêm 1 ô nào nữa, không nó đi ra giữa poision rồi mới chết
            player.transform.Translate(Time.deltaTime*speedTranlate_X,0,0);
            if(player.transform.position.y > transform.position.y - 1){
                player.transform.Translate(0,-Time.deltaTime*5,0);
            }
            else{
                StartCoroutine(SetPosionAnimation());
            }
        }

        if(canTranslateAnubis){
            //anubis.GetComponent<CatcherControl>().speed=0; //Để cho player k dc di chuyển nữa thêm 1 ô nào nữa, không nó đi ra giữa poision rồi mới chết
            anubis.transform.Translate(Time.deltaTime*speedTranlate_X,0,0);
            if(anubis.transform.position.y > transform.position.y - 1){
                anubis.transform.Translate(0,-Time.deltaTime*5,0);
            }
            else{
                StartCoroutine(SetPosionForCatcher());
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(player.transform.position.x < firstPoint.position.x 
        || player.transform.position.x > LastPoint.position.x ){
        }else{
            if(other.gameObject.tag=="Player"){
                if(!player.activePower){
                    player.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
                    canTranslatePlayer=true;
                }
            }
        }

        if(anubis.transform.position.x < firstPoint.position.x  || anubis.transform.position.x > LastPoint.position.x ){
        }else{
            if(other.gameObject.tag=="Catcher"){
                    anubis.stopUpdate=true;
                    anubis.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
                    canTranslateAnubis=true;
                }
        }
    }

    private IEnumerator SetPosionAnimation(){
        myGame.allow=false;
        //Anubis - de khi player chet thi no k di chuyen
        anubis.stopUpdate=true;
        anubis.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
        anubis.GetComponent<Animator>().enabled=false;

        //Player
        canTranslatePlayer=false;
        SoundControl.instance.PlaySound(gameOverSound);
        SoundControl.instance.SetMusic(false);
        player.transform.GetChild(1).GetComponent<Animator>().SetTrigger("spawn");
        player.GetComponent<BoxCollider2D>().enabled=false; //Để không phát hiện sự va chạm nữa => Tránh việc canKillTrue lần nữa
        player.GetComponent<SpriteRenderer>().enabled=false;
        player.GetComponent<Rigidbody2D>().gravityScale=0;
        player.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
        yield return new WaitForSeconds(1);
        myGame.die=true;
    }

    private IEnumerator SetPosionForCatcher(){
        canTranslateAnubis=false;
        SoundControl.instance.PlaySound(impactSound);
        anubis.stopUpdate=true;
        anubis.GetComponent<Rigidbody2D>().gravityScale=0;
        anubis.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
        anubis.GetComponent<SpriteRenderer>().enabled=false;
        anubis.transform.GetChild(1).GetComponent<Animator>().SetTrigger("spawn");
        anubis.GetComponent<SpriteRenderer>().enabled=false;
        anubis.GetComponent<BoxCollider2D>().enabled=false;
        anubis.myGame.stillDoing=false;
        anubis.GetComponent<CatcherControl>().enabled=false;

        yield return new WaitForSeconds(1);
    }
}
