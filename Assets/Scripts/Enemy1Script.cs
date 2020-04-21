using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    public GameObject Camara;

    public int EnemyHP = 1;
    public float movementSpeed;
    public float jumpStrengh = 4;
    public float jumpLengh = 4;
    public float minJumpDelay = 300f;
    public float maxJumpDelay = 600f;
    public float jumpDelay = 0;
    private float randomJump;

    private float jumpTmp = 0;
    private bool jumped = true;

    private bool grounded;
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

        randomJump = Random.Range(minJumpDelay, maxJumpDelay);
        jumpDelay = randomJump;
    }

    void FixedUpdate()
    {
        
        float delta = Time.deltaTime * 1000;
        float deltasmall = Time.deltaTime * 100;
        if (this.transform.position.y <= Camara.transform.position.y - 4)
        {
            rigidBody.AddForce(Vector2.right * -jumpLengh * deltasmall, ForceMode2D.Impulse);
            rigidBody.AddForce(Vector2.up * jumpStrengh * deltasmall, ForceMode2D.Impulse);
            jumped = true;
        }
        if (EnemyHP <= 0)
        {
            GameController.Instance.hiscore += 100;
            //cameraScript = CameraController.GetComponent<CameraControl>();
            //cameraScript.auidoS.clip = SoundManager.Instance.Enemy2Death;
            //cameraScript.auidoS.Play();

            Destroy(gameObject); //DESTROY
        }
        else if (this.transform.position.x <= Camara.transform.position.x - 10 ||
        this.transform.position.y <= Camara.transform.position.y - 10)
        {
            Destroy(gameObject); //DESTROY
        }

        if (jumped == false && grounded == true)
        {
            rigidBody.AddForce(Vector2.right * -jumpLengh * deltasmall, ForceMode2D.Impulse);
            rigidBody.AddForce(Vector2.up * jumpStrengh * deltasmall, ForceMode2D.Impulse);
            jumped = true;
        }

        if (jumped == true && jumpTmp <= jumpDelay && grounded == true) { jumpTmp += delta; }
        else if (jumpTmp >= jumpDelay) {
            jumped = false;
            randomJump = Random.Range(minJumpDelay, maxJumpDelay);
            jumpDelay = randomJump;
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
        }
    }
}
