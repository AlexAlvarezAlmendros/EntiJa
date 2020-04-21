using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameController : MonoBehaviour
{
    public Slider slider;
    public int lives;
    public int hiscore;
    public int record;
    public float energy;
    public static GameController instance { get; private set; }
    

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

    public void BeginGame()
    {

    }
    public void giveEnergy(float _energy)
    {
        energy = energy + _energy;
        slider.value = energy;
    }
    public void useEnergy(float _energy)
    {
        energy = energy - _energy;
        slider.value = energy;
    }
    public float getEnergy()
    {
        return energy;
    }

    public void DecrementLives()
    {
        lives--;

        // Has player run out of lives?
        if (lives < 1)
        {
            SceneManager.LoadScene("Game_Over");
            // Restart the game
            BeginGame();
        }
    }

    public void SetSlider()
    {
        Debug.Log("Searching Slider");
        //slider = GameObject.FindGameObjectWithTag("canvas").GetComponent<Slider>();
        if (GameObject.FindGameObjectWithTag("canvas"))
        {
            slider = (Slider)FindObjectOfType(typeof(Slider));
        }

    }
}
    