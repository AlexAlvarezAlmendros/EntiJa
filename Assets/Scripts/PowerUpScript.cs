using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUp { NULL, Energy, Boost, Shield, PW4 };

public class PowerUpScript : MonoBehaviour
{
    public PowerUp powerUpType = PowerUp.NULL;

    public GameObject Camara;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        Camara = GameObject.FindWithTag("MainCamera");

        switch (powerUpType)
        {
            case PowerUp.Energy:

                break;
            case PowerUp.Boost:
                //animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AnimationChangeScene");
                break;
            case PowerUp.Shield:

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
