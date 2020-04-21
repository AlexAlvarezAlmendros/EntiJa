using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicmenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
