using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    public float moveSpeed;

    void FixedUpdate()
    {
        float delta = Time.deltaTime * 1000;

        //CAMERA RELOCATION
        if (Player.transform.position.x < transform.position.x - 300)
        {
            transform.position = new Vector3(Player.transform.position.x - 300, transform.position.y, 0);
        }
        else if (Player.transform.position.x > transform.position.x + 290)
        {
            transform.position = new Vector3(Player.transform.position.x + 290, transform.position.y, 0);
        }
        if (Player.transform.position.y < transform.position.y - 300)
        {
            transform.position = new Vector3(transform.position.x, Player.transform.position.y - 300, 0);
        }
        else if (Player.transform.position.y > transform.position.y + 290)
        {
            transform.position = new Vector3(transform.position.x, Player.transform.position.y + 290, 0);
        }

        //MOVEMENT / TP PLAYER
        if (Player.transform.position.x <= this.transform.position.x -1)
        {
            Player.transform.position.x = this.transform.position.x - 1;
        }
        else if (Player.transform.position.x >= this.transform.position.x + 1)
        {
            Player.transform.position.x = this.transform.position.x + 1;
        }
    }
}
