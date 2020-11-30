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
    public GameObject myLevelSelect;
    public GameObject myMainMenu;
    public GameObject myOptionMenu;
    public GameObject myCredits;

    // --- UI Knappar --- //
    public void Play()
    {
        myMainMenu.SetActive(false);
        myLevelSelect.SetActive(true);

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
        myLevelSelect.SetActive(false);
        myCredits.SetActive(false);
        myOptionMenu.SetActive(false);
        myMainMenu.SetActive(true);

    }

    // --- Level Select Buttons --- //
    public void LevelOne()
    {
        SceneManager.LoadScene("Paul_Level1");
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene("Paul_Level2");
    }
    public void LevelThree()
    {
        SceneManager.LoadScene("Bana_3");
    }
    public void LevelFour()
    {
        SceneManager.LoadScene("Bana_4");
    }
    public void LevelFive()
    {
        SceneManager.LoadScene("Bana_5");
    }
    public void LevelSix()
    {
        SceneManager.LoadScene("Bana_6");
    }
}
