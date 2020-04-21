using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CarController : MonoBehaviour
{
    bool isDead = false;

    private float speed = 5f;
    private float walkAcceleration = 75f;
    private float groundFriction = 70f;
    public float jumpForce = 4f;
    public float flyForce = 1f;
    public float fuelConsumedXSec = 1f;
    public float flyDelay = 4f;
    public float flyTmp = 0f;
    public bool canFly = false;

    private Animator animator;
    private Vector2 velocity;
    private BoxCollider2D collider;
    private Rigidbody2D rig;
    public GameObject cam;

    public GameObject laser;
    public Animator laserAnimator;

    private int GroundingID;
    private int JumpedID;
    private int FlyingID;
    private int LaserShotID;

    void Start()
    {
        GameController.Instance.setEnergy(30);
        transform.position = new Vector3(-4.44f, -3.16f, 0f);
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        laserAnimator = laser.GetComponent<Animator>();

        GroundingID = Animator.StringToHash("Grounding");
        JumpedID = Animator.StringToHash("Jumped");
        FlyingID = Animator.StringToHash("Flying");

        rig = GetComponent<Rigidbody2D>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
    }
    private void Update()
    {
        if (GameController.Instance.lives <= 0)
        {
            isDead = true;
            //DEATH ANIMATION
        }

        bool isGrounding = animator.GetBool(GroundingID);
        if (isGrounding && Input.GetKey(KeyCode.Space)) //JUMP
        {
            rig.AddForce(jumpForce * transform.up, ForceMode2D.Impulse); 
        }
        if (Input.GetKey(KeyCode.Space) && GameController.Instance.energy > 0 && canFly == true) //FLY
        {
            GameController.Instance.useEnergy(fuelConsumedXSec);
            rig.velocity = Vector2.up * flyForce;
        }
        if (flyTmp >= flyDelay) { canFly = true; flyTmp = 0; }
        else if (!isGrounding && canFly == false)
        {
            flyTmp += Time.deltaTime * 10;
        }
        else if (isGrounding) { canFly = false; }
        
        float energy = GameController.Instance.getEnergy();
    }
    private void FixedUpdate()
    {
        bool isFlying = animator.GetBool(FlyingID);

        if (transform.position.x < cam.transform.position.x - 3.65f)
        {
            transform.position = new Vector3(cam.transform.position.x - 3.65f, transform.position.y, transform.position.z);
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
        if (coll.gameObject.tag.Equals("Enemy"))
        {
            GameController.Instance.lives--;
        }
        if (coll.gameObject.tag.Equals("Energy"))
        {
            GameController.Instance.setEnergy(20);
        }

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            animator.SetBool(GroundingID, true);
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            animator.SetBool(GroundingID, false);
        }
    }
}