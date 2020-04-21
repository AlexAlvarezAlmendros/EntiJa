using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void ChangeScene (string scName) {
        switch (scName)
        {
            case "MainMenu":
                SceneManager.LoadScene(scName);
                break;
            case "Game":
                FindObjectOfType<AudioManager>().Stop("MenuMusic");
                FindObjectOfType<AudioManager>().Play("GameMusic");
                SceneManager.LoadScene(scName);
                break;
            default:
                SceneManager.LoadScene(scName);
                break;
        }
        
    }

    public void ExitGame() {
        Application.Quit();
    }
}
