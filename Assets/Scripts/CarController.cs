﻿using System.Collections;
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
    private float flyTmp = 0f;
    private bool canFly = false;

    private bool invulnerableTime = false;
    public float invulnerableDelay;
    private float invulnetableTmp;

    private PowerUpScript powerUpScript;

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
    private int isDeadID;

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
        isDeadID = Animator.StringToHash("isDead");

        rig = GetComponent<Rigidbody2D>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
    }
    private void Update()
    {
        if (GameController.Instance.lives <= 0)
        {
            animator.SetBool(isDeadID, true);
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

        if (invulnerableTime == true && invulnetableTmp <= invulnerableDelay)
        {
            invulnetableTmp += Time.deltaTime * 10;
        }
        else { invulnerableTime = false; invulnetableTmp = 0; }
        
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
            invulnerableTime = true;
        }
        if (coll.gameObject.tag.Equals("PowerUp"))
        {
            powerUpScript = coll.GetComponent<PowerUpScript>();
            switch (powerUpScript.powerUpType)
            {
                case PowerUp.Energy:
                    GameController.Instance.setEnergy(20);
                    break;
                case PowerUp.Boost:
                    //SET fuelConsumedXSec = 0 FOR X SECONDS
                    break;
                case PowerUp.Shield:
                    GameController.Instance.lives = 2;
                    break;
            }
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