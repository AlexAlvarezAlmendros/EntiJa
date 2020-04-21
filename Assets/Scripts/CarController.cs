using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CarController : MonoBehaviour
{
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

    public Slider slider;
    public Text scoreText;

    public Animator animator;
    private Vector2 velocity;
    private BoxCollider2D collider;
    private Rigidbody2D rig;
    public GameObject cam;
    public GameObject explosion;

    public GameObject PrefabEnemySP;
    public GameObject PrefabPowerUpSp;
    public GameObject PrefabPlatformSP;

    public Animator ShieldOverlay;

    private int GroundingID;
    private int JumpedID;
    private int FlyingID;
    private int isDeadID;
    private int ShieldID;

    private bool once = false;
    private bool audioplaying;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("GameMusic");
        audioplaying = false;
        giveEnergy(100);
        transform.position = new Vector3(-3.877f, -3.883f, 0f);
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();


        GroundingID = Animator.StringToHash("Grounding");
        JumpedID = Animator.StringToHash("Jumped");
        FlyingID = Animator.StringToHash("Flying");
        isDeadID = Animator.StringToHash("isDead");
        ShieldID = Animator.StringToHash("Active");

        rig = GetComponent<Rigidbody2D>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        GameController.instance.GameON = true;
        GameController.instance.hiscore = 0;
        GameController.instance.energy = 100;

        Instantiate(PrefabEnemySP, this.transform.position, Quaternion.identity);
        Instantiate(PrefabPowerUpSp, this.transform.position, Quaternion.identity);
        //Instantiate(PrefabPlatformSP, this.transform.position, Quaternion.identity);
    }
    private void Update()
    {
        if (GameController.instance.lives <= 0 || this.transform.position.y == -5.87)
        {
            animator.SetBool(isDeadID, true);
            //DEATH ANIMATION
            GameController.instance.GameON = false;
            GameController.instance.lives = 1;
            FindObjectOfType<AudioManager>().Stop("Fly");
            FindObjectOfType<AudioManager>().Stop("GameMusic");
            SceneManager.LoadScene("GameOver");
        }

        bool isGrounding = animator.GetBool(GroundingID);
        if (isGrounding && Input.GetKey(KeyCode.Space)) //JUMP
        {
            rig.AddForce(jumpForce * transform.up * Time.deltaTime * 10, ForceMode2D.Impulse);
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

        if (invulnerableTime == true && invulnetableTmp <= invulnerableDelay)
        {
            invulnetableTmp += Time.deltaTime * 10;
        }
        else { invulnerableTime = false; invulnetableTmp = 0; }

        if (boosted == true && boostTmp <= boostDelay)
        {
            boostTmp += Time.deltaTime * 10;
        }
        else { boosted = false; boostTmp = 0; }
        if (!isGrounding && !audioplaying)
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
        scoreText.text = GameController.instance.hiscore + " km";
        bool isFlying = animator.GetBool(FlyingID);

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
                    GameController.instance.lives = 2;
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
        if (coll.gameObject.tag.Equals("Enemy") && invulnerableTime == false)
        {
            invulnerableTime = true;
            GameObject clone = (GameObject)Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(clone, 1.0f);
            GameController.instance.lives--;

            if (isShielded)
            {
                ShieldOverlay.SetBool(ShieldID, false);
            }
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            animator.SetBool(GroundingID, false);
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
}
