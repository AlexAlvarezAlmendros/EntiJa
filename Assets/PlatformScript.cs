using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public GameObject Camara;

    // Start is called before the first frame update
    void Start()
    {
        Camara = GameObject.FindWithTag("MainCamera");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.position.x < Camara.transform.position.x - 30)
        {
            Destroy(gameObject);
        }
    }
}
