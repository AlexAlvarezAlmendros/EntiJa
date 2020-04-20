using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    public GameObject Camara;

    public int EnemyHP = 1;
    public float jumpStrengh = 100;
    public float jumpDelay = 1000;
    private float jumpTmp = 0;
    private bool jumped = true;
    private int JumpedID;

    private Rigidbody2D rigidBody;
    private Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        JumpedID = Animator.StringToHash("Jumped");
        enabled = false;
    }

    void FixedUpdate()
    {
        float delta = Time.deltaTime * 1000;

        if (EnemyHP == 0)
        {
            GameController.Instance.hiscore += 100;
            //cameraScript = CameraController.GetComponent<CameraControl>();
            //cameraScript.auidoS.clip = SoundManager.Instance.Enemy2Death;
            //cameraScript.auidoS.Play();

            this.transform.position = new Vector3(this.transform.position.x, -13, 0);
            enabled = false; //DISABLE

        }
        else if (this.transform.position.x <= Camara.transform.position.x -13)
        {
            this.transform.position = new Vector3(this.transform.position.x, -15, 0);
            enabled = false; //DISABLE
        }

        if (jumped == true && jumpTmp < jumpDelay) { jumpTmp += delta; }
        else if (jumpTmp >= jumpDelay) { jumped = false; jumpTmp = 0; }
        
        if (jumped == false)
        {
            rigidBody.AddForce(Vector2.right * -jumpStrengh * delta, ForceMode2D.Impulse);
            rigidBody.AddForce(Vector2.up * -jumpStrengh * delta, ForceMode2D.Impulse);
            jumped = true;
        }
    }


}
