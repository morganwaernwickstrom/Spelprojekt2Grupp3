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
        SoundManager.myInstance.PlayMenuButtonSound();
        SceneManager.LoadScene("LevelSelect");
    }
    public void Options()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        myMainMenu.SetActive(false);
        myOptionMenu.SetActive(true);
    }
    public void Credits()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        myMainMenu.SetActive(false);
        myCredits.SetActive(true);
    }
    public void BackToMenu()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        myCredits.SetActive(false);
        myOptionMenu.SetActive(false);
        myMainMenu.SetActive(true);

    }

    public void LoadMenu()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        SceneManager.LoadScene("Main Menu");
    }

    // --- Level Select Buttons --- //

    public void TutorialOne()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        SceneManager.LoadScene("TutorialLevel1");
    }

    public void TutorialTwo()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        SceneManager.LoadScene("TutorialLevel2");
    }
    public void LevelOne()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        SceneManager.LoadScene("Level 1");
    }
    public void LevelTwo()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        SceneManager.LoadScene("Level 2");
    }
    public void LevelThree()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        SceneManager.LoadScene("Level 3");
    }
    public void LevelFour()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        SceneManager.LoadScene("Level 4");
    }
    public void LevelFive()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        SceneManager.LoadScene("Level 5");
    }
    public void LevelSix()
    {
        SoundManager.myInstance.PlayMenuButtonSound();
        SceneManager.LoadScene("Level 6");
    }
}
