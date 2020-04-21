using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    public float moveSpeed;
    private float actualMoveSpeed;

    private void Start()
    {
        actualMoveSpeed = moveSpeed / 100;
    }
    private void Update()
    {
        float delta = Time.deltaTime * 100;
        transform.position = new Vector3(transform.position.x + actualMoveSpeed * delta, transform.position.y, transform.position.z);
    }
    void FixedUpdate()
    {
        float delta = Time.deltaTime * 1000;
        
        //MOVEMENT / TP PLAYER
        //if (Player.transform.position.x <= this.transform.position.x - 4.95)
        //{
        //    Player.transform.position = new Vector3(Player.transform.position.y, this.transform.position.x - 1, 0);
        //}
        //else if (Player.transform.position.x >= this.transform.position.x + 5.2)
        //{
        //    Player.transform.position = new Vector3(Player.transform.position.y, this.transform.position.x + 1, 0);
        //}
    }
}
