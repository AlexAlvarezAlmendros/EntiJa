using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CarController : MonoBehaviour
{
    //VARIABLES
    private float speed = 2f;
    private float walkAcceleration = 75f;
    private float groundFriction = 70f;

    public float jumpForce = 4f;
    public float flyForce = 1f;
    public float fuelConsumedXSec = 1f;
    public float flyDelay = 4f;
    private float flyTmp = 0f;
    private bool canFly = false;

    public float invulnerableDelay;
    public bool invulnerableTime = false;
    private float invulnetableTmp;

    public float boostDelay;
    public bool boosted = false;
    private float boostTmp;

    private PowerUpScript powerUpScript;

    //GETTERS
    private Animator animator;
    private BoxCollider2D collider;
    private Rigidbody2D rig;
    private Vector2 velocity;

    public GameObject cam;
    public GameObject explosion;
    public Transform rayPoint;

    //PREFABS SPAWNERS
    public GameObject PrefabEnemySP;
    public GameObject PrefabPowerUpSp;

    //HUD - UI
    public GameObject car1;
    public Text carHP;
    public Animator ShieldOverlay;

    public Slider slider;
    public Animator eBarAnimator;

    public GameObject score;
    private Text scoreText;
    private Animator scoreAnimator;
    private float scoreAnimTmp = 0;

    public GameObject highscoreText;
    private Animator highScoreAnimator;

    //ANIMATION IDS
    private int GroundingID;
    private int JumpedID;
    private int FlyingID;
    private int isDeadID;
    private int ShieldID;
    private int BoostedID;
    private int BoostedEndID;
    private int ScoreWigleID;
    private int HighScoreActiveID;

    //AUDIO
    private bool audioplaying;

    void Start()
    {
        //SETTERS
        GameController.instance.lives = 3;
        FindObjectOfType<AudioManager>().Play("GameMusic");
        audioplaying = false;
        giveEnergy(100);
        transform.position = new Vector3(-3.877f, -3.883f, 0f);

        GameController.instance.GameON = true;
        GameController.instance.hiscore = 0;
        GameController.instance.energy = 100;
        highscoreText.SetActive(false);
        GameController.instance.isrecord = false;

        //GETTERS
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();

        scoreAnimator = score.GetComponent<Animator>();
        scoreText = score.GetComponent<Text>();
        highScoreAnimator = highscoreText.GetComponent<Animator>();

        //ANIAMTION IDS
        GroundingID = Animator.StringToHash("Grounding");
        JumpedID = Animator.StringToHash("Jumped");
        FlyingID = Animator.StringToHash("Flying");
        isDeadID = Animator.StringToHash("isDead");
        ShieldID = Animator.StringToHash("Active");
        BoostedID = Animator.StringToHash("Boosted");
        BoostedEndID = Animator.StringToHash("Exit");
        ScoreWigleID = Animator.StringToHash("ScoreMilestone");
        HighScoreActiveID = Animator.StringToHash("Highscore");

        //SPAWNERS
        Instantiate(PrefabEnemySP, this.transform.position, Quaternion.identity);
        Instantiate(PrefabPowerUpSp, this.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        animator.SetBool(GroundingID, Driving()); 

        carHP.text = GameController.instance.lives.ToString();

        eBarAnimator.SetBool("Boosted", boosted);
        float delta = Time.deltaTime * 10;
        if (GameController.instance.lives <= 0 || this.transform.position.y == -5.87)
        {
            animator.SetBool(isDeadID, true);
            //DEATH ANIMATION
            GameController.instance.GameON = false;
            FindObjectOfType<AudioManager>().Stop("Fly");
            FindObjectOfType<AudioManager>().Stop("GameMusic");
            FindObjectOfType<AudioManager>().Play("MenuMusic");
            SceneManager.LoadScene("GameOver");
        }
        Driving();
        bool isGrounding = animator.GetBool(GroundingID);
        if (isGrounding && Input.GetKey(KeyCode.Space)) //JUMP
        {
            rig.AddForce(jumpForce * transform.up * delta, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.Space) && GameController.instance.energy > 0 && canFly == true) //FLY
        {
            if (boosted == false) { useEnergy(fuelConsumedXSec); }
            rig.velocity = Vector2.up * flyForce;
        }
        if (flyTmp >= flyDelay) { canFly = true; flyTmp = 0; } 
        else if (!isGrounding && canFly == false) 
        {
            flyTmp += Time.deltaTime * 10;
        }
        else if (isGrounding) { canFly = false; }

        if (invulnerableTime == true && invulnetableTmp <= invulnerableDelay) //INVULNERABLITY
        {
            invulnetableTmp += Time.deltaTime * 10;
        }
        else { invulnerableTime = false; invulnetableTmp = 0; }

        if (boosted == true && boostTmp <= boostDelay) //BOOST
        {
            boostTmp += Time.deltaTime * 10;
            if (boostTmp > (boostDelay - boostDelay / 4))
            {
                eBarAnimator.SetBool(BoostedEndID, true);
            }
        }
        else { boosted = false; boostTmp = 0; eBarAnimator.SetBool(BoostedEndID, false); }

        if (!isGrounding && !audioplaying) //AUDIO FLY
        {
            FindObjectOfType<AudioManager>().Play("Fly");
            audioplaying = true;
        }
        else if (isGrounding && audioplaying)
        {
            audioplaying = false;
            FindObjectOfType<AudioManager>().Stop("Fly");
        }
    }
    private void FixedUpdate()
    {
        scoreText.text = GameController.instance.hiscore + " m"; //SORE
        if (GameController.instance.hiscore > scoreAnimTmp + 1000)
        {
            scoreAnimTmp += 1000;
            scoreAnimator.SetTrigger(ScoreWigleID); //WIGLE ANIM
        }
        if (GameController.instance.isrecord == true) { highscoreText.SetActive(true); }

        bool isFlying = animator.GetBool(FlyingID); //FLY

        if (transform.position.x < cam.transform.position.x - 4.5f) //MAX IZQUIERDA
        {
            transform.position = new Vector3(cam.transform.position.x - 4.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > cam.transform.position.x + 4.5f) //MAX DERECHA
        {
            transform.position = new Vector3(cam.transform.position.x + 4.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.y < cam.transform.position.y - 10f) //MAX DOWN
        {
            GameController.instance.lives = 0;
        }

        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundFriction * Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("PowerUp"))
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            powerUpScript = coll.GetComponent<PowerUpScript>();
            switch (powerUpScript.powerUpType)
            {
                case PowerUp.Energy:
                    if (GameController.instance.energy + 20 > 100) { giveEnergy(100 - GameController.instance.energy); }
                    else { giveEnergy(50); }
                    break;
                case PowerUp.Boost:
                    boosted = true;
                    break;
                case PowerUp.Shield:
                    ShieldOverlay.SetBool(ShieldID, true);
                    break;
            }
            Destroy(coll.gameObject);
        }

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            animator.SetBool(GroundingID, true);
        }

        bool isShielded = ShieldOverlay.GetBool(ShieldID);
        if (coll.gameObject.tag.Equals("Enemy") && invulnerableTime == false) //DAMAGE
        {
            FindObjectOfType<AudioManager>().Play("Explosion");
            if (isShielded)
            {
                ShieldOverlay.SetBool(ShieldID, false);
                invulnerableTime = true;
            }
            if (!isShielded)
            {
                invulnerableTime = true;
                GameObject clone = (GameObject)Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(clone, 1.0f);
                GameController.instance.lives--;
            }
        }
    }

    public void giveEnergy(float _energy)
    {
        GameController.instance.energy = GameController.instance.energy + _energy;
        slider.value = GameController.instance.energy;
    }
    public void useEnergy(float _energy)
    {
        GameController.instance.energy = GameController.instance.energy - _energy;
        slider.value = GameController.instance.energy;
    }

    private bool Driving()
    {
        bool driving = false;
        Vector2 endPos = rayPoint.position + Vector3.down * 1.5f;
        RaycastHit2D hit = Physics2D.Linecast(rayPoint.position, endPos, 1 << LayerMask.NameToLayer("Ground"));
        if (hit.collider != null){
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                driving = true;
                return driving;
            }
            else
            {
                driving = false;
                return driving;
            }
        }
        return driving;

    }
}
