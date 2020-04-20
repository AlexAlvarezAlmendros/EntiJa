﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUp { Energy, Boost, Shield, PW4 };

public class PowerUpScript : MonoBehaviour
{
    public PowerUp powerUpType = PowerUp.Shield;

    public GameObject Camara;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        switch (powerUpType)
        {
            case PowerUp.Energy:

                break;
            case PowerUp.Boost:
                //animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AnimationChangeScene");
                break;
            case PowerUp.Shield:

                break;
            case PowerUp.PW4:

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //GameManager.Instance.HP++;
            //Destroy(collision.gameObject);
            //GameManager.Instance.score += 100;
        }
    }
}
