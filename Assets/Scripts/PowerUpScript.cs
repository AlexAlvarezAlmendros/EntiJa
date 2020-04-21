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
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Energy");
                break;
            case PowerUp.Boost:
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Boost");
                break;
            case PowerUp.Shield:
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Shield");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //SFXManager.Instance.PlayAudio(0);
            SFXManager.Instance.PlayInLoop(0);
            Destroy(gameObject);
        }
    }
}
