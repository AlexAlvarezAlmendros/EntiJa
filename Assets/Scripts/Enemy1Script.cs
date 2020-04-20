using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    public GameObject Camara;

    public int EnemyHP = 1;
    public float jumpStrengh = 1;
    public float jumpLengh = 1;
    public float jumpDelay = 1000;
    public float jumpTmp = 0;
    public bool jumped = true;

    private Vector2 velocity;
    private bool grounded;
    private bool isjumping = false;
    private int JumpedID;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private BoxCollider2D boxCollider;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        Camara = GameObject.FindWithTag("MainCamera");
        JumpedID = Animator.StringToHash("Jumped");
    }

    void FixedUpdate()
    {
        float delta = Time.deltaTime * 1000;
        float deltasmall = Time.deltaTime * 100;

        if (grounded)
        {
            velocity.y = 0;

            if (jumped == false)
            {
                rigidBody.AddForce(Vector2.right * -jumpLengh * deltasmall, ForceMode2D.Impulse);
                rigidBody.AddForce(Vector2.up * jumpStrengh * deltasmall, ForceMode2D.Impulse);
                jumped = true;
                isjumping = true;
            }
            else
            {
                isjumping = false;
            }
        }
        velocity.y += Physics2D.gravity.y * Time.deltaTime;

        velocity.x = velocity.x - 20;

        if (EnemyHP <= 0)
        {
            GameController.Instance.hiscore += 100;
            //cameraScript = CameraController.GetComponent<CameraControl>();
            //cameraScript.auidoS.clip = SoundManager.Instance.Enemy2Death;
            //cameraScript.auidoS.Play();

            Destroy(gameObject); //DESTROY
        }
        //else if (this.transform.position.x <= Camara.transform.position.x - 10 ||
        //this.transform.position.y <= Camara.transform.position.y - 10) 
        //     {
        //        Destroy(gameObject); //DESTROY
        //     }

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
        grounded = false;
        foreach (Collider2D hit in hits)
        {
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);
            if (colliderDistance.isOverlapped)
            {

                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }
            if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
            {
                grounded = true;
            }
        }

        if (jumped == true && jumpTmp <= jumpDelay) { jumpTmp += delta; }
        else if (jumpTmp >= jumpDelay) { jumped = false; jumpTmp = 0; }
    }


}
