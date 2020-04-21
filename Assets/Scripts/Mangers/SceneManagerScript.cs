using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void ChangeScene (string scName) {
        switch (scName)
        {
            case "AlexScene":
                FindObjectOfType<AudioManager>().Stop("MenuMusic");
                break;
            case "MainMenu":
                if (FindObjectOfType<AudioManager>().Equals("MenuMusic")){
                    
                    FindObjectOfType<AudioManager>().Play("MenuMusic");
                }
                
                break;
            default:
                break;
        }
        SceneManager.LoadScene(scName);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
