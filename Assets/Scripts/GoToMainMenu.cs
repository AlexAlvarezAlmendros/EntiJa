﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    
    void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
