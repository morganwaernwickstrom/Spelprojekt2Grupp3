using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // menyer vi ska ha:
    //  - Main
    //  - Settings
    //  - Level Select
    //  - Credits
    public GameObject myMainMenu;
    public GameObject myOptionMenu;
    public GameObject myCredits;
    

    // --- UI Knappar --- //
    public void Play()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void Options()
    {
        myMainMenu.SetActive(false);
        myOptionMenu.SetActive(true);
    }
    public void Credits()
    {
        myMainMenu.SetActive(false);
        myCredits.SetActive(true);
    }
    public void BackToMenu()
    {
        myCredits.SetActive(false);
        myOptionMenu.SetActive(false);
        myMainMenu.SetActive(true);

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // --- Level Select Buttons --- //
    public void LevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void LevelThree()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void LevelFour()
    {
        SceneManager.LoadScene("Level 4");
    }
    public void LevelFive()
    {
        SceneManager.LoadScene("Level 5");
    }
    public void LevelSix()
    {
        SceneManager.LoadScene("Level 6");
    }
}
