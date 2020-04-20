using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    public float moveSpeed;
    private void Start()
    {
        
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
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
