﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("levelSelect");
    }
    public void Settings()
    {
        SceneManager.LoadScene("settings");
    }
    public void Credits()
    {
        SceneManager.LoadScene("credits");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
