using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameController : MonoBehaviour
{
    public int lives;
    public int hiscore;
    public int record;
    public bool isrecord;
    public float energy;
    public static GameController instance { get; private set; }

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
    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.Instance.Play(GameMusic);
        StreamReader reader = new StreamReader("Hiscore.txt");
        string hiscorestring = reader.ReadLine();
        hiscore = int.Parse(hiscorestring);
        reader.Close();
        BeginGame();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void FixedUpdate()
    {
        if (GameON == true)
        {
            hiscore += 1;
        }
    }

    public void BeginGame()
    {

    }


}
    