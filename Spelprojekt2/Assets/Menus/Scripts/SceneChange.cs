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


    // --- UI Knappar --- //
    public void Play()
    {
        // referens till Level Select - canvas
        // GameObject myLevelSelect;
        // myLevelSelect.SetActive(true);
        // myLevelSelect.SetActive(false);

        // Gömma Main Menu Canvas
        // Visa Level Select Canvas

    }
    public void Settings()
    {
        //SceneManager.LoadScene("settings");
    }
    public void Credits()
    {
        //SceneManager.LoadScene("credits");
    }
    public void BackToMenu()
    {
        //SceneManager.LoadScene("mainMenu");
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
}
