using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Script : MonoBehaviour
{
    public GameObject Camara;

    public int EnemyHP = 1;
    public float movementSpeed;
    public float maxZigZag;
    private float maxZigZagTmp;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private BoxCollider2D boxCollider;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        Camara = GameObject.FindWithTag("MainCamera");
    }

    void FixedUpdate()
    {
        float delta = Time.deltaTime * 1000;
        float deltasmall = Time.deltaTime * 100;

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
    }
    
}
