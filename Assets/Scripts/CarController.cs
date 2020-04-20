﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CarController : MonoBehaviour
{
    private float speed = 5;
    private float walkAcceleration = 75;
    private float groundFriction = 70;
    private float jumpForce = 4;
    
    private Animator anim;
    private Vector2 velocity;
    private BoxCollider2D collider;
    private Rigidbody2D rig;
    public GameObject cam;

    public GameObject laser;

    private int GroundingID;
    private int JumpedID;
    private int FlyingID;

    void Start()
    {
        GameController.Instance.setEnergy(30);
        transform.position = new Vector3(-4.44f, -3.16f, 0f);
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();

        GroundingID = Animator.StringToHash("Grounding");
        JumpedID = Animator.StringToHash("Jumped");
        FlyingID = Animator.StringToHash("Flying");

        rig = GetComponent<Rigidbody2D>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
    }

    private void FixedUpdate()
    {
        bool isGrounding = animator.GetBool(GroundingID);
        bool hasJumped = animator.GetBool(JumpedID);
        bool isFlying = animator.GetBool(FlyingID);

        if (transform.position.x < cam.transform.position.x - 3.65f)
        {
            transform.position = new Vector3(cam.transform.position.x - 3.65f, transform.position.y, transform.position.z);
        }

        if (isGrounding && Input.GetButtonDown("Jump"))
        {
            rig.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
        }

        if (Input.GetMouseButtonDown(1))
        {

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

        float energy = GameController.Instance.getEnergy();
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
