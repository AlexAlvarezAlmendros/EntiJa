using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    public int lives;

    public int hiscore = 0;
    public int record;
    public bool isrecord;

    public float energy;

    public bool GameON;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void FixedUpdate()
    {
        if (hiscore > record) { isrecord = true; record = hiscore; }
        if (GameON == true)
        {
            hiscore += 1;
        }
    }
}
    