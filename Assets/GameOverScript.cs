using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public Text text;

    void Start()
    {
        text.text = GameController.instance.hiscore + "km";
    }

    void Update()
    {
        
    }
}
