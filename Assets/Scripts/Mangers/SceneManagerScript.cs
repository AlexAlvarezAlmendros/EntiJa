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
                FindObjectOfType<AudioManager>().Play("MenuMusic");
                break;
            case "Game":
                SceneManager.LoadScene(scName);
                FindObjectOfType<AudioManager>().Play("GameMusic");
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
