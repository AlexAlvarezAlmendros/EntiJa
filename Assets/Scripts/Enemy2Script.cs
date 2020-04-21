using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Script : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject parentObj;

    public GameObject Camara;
    public int EnemyHP = 1;
    public float enemySpeed;

    private bool isGoingRight;

    // Start is called before the first frame update
    void Start()
    {
        Camara = GameObject.FindWithTag("MainCamera");
        if(!isGoingRight)
        {
            transform.position = startPoint.transform.position;
        }
        else
        {
            transform.position = endPoint.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGoingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, enemySpeed * Time.deltaTime);
            if(transform.position == endPoint.transform.position)
            {
                isGoingRight = true;
            }
            
        }

        if(isGoingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.transform.position, enemySpeed * Time.deltaTime);
            if(transform.position == startPoint.transform.position)
            {
                isGoingRight = false;
            }
            
        }
    }
    public void takeDamage(int _damage)
    {
        EnemyHP = EnemyHP - _damage;
    }

    void FixedUpdate()
    {
        if (EnemyHP <= 0)
        {
            GameController.instance.hiscore += 100;
            //cameraScript = CameraController.GetComponent<CameraControl>();
            //cameraScript.auidoS.clip = SoundManager.Instance.Enemy2Death;
            //cameraScript.auidoS.Play();

            Destroy(parentObj); //DESTROY
        }
        else if (this.transform.position.x <= Camara.transform.position.x - 10 ||
        this.transform.position.y <= Camara.transform.position.y - 10)
        {
            Destroy(parentObj); //DESTROY
        }
    }
}
