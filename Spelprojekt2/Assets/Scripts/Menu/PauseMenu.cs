using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject myPauseMenu = null;
    [SerializeField] GameObject myPauseButton = null;
    [SerializeField] GameObject myRetryButton = null;
    [SerializeField] GameObject myOptionsMenu = null;

    private GameObject myPlayer;

    bool myGamePaused = false;

    void Update()
    {

        myPlayer = GameObject.FindGameObjectWithTag("Player");

        if (myGamePaused) 
        {
            myPauseMenu.SetActive(true);
            myPauseButton.SetActive(false);
            Time.timeScale = 0;
            myPlayer.GetComponent<PlayerMovement>().enabled = false;
        }
        else 
        {
            myPauseMenu.SetActive(false);
            myPauseButton.SetActive(true);
            Time.timeScale = 1;
            myPlayer.GetComponent<PlayerMovement>().enabled = true;
        }

        SetPauseState();
    }

    void SetPauseState() 
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            myGamePaused = !myGamePaused;
        }
    }

    public void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LevelSelect() 
    {
        SceneManager.LoadScene("LevelSelect");
    }
    
    public void ChangePauseState() 
    {
        myGamePaused = !myGamePaused;
        myOptionsMenu.SetActive(false);
    }

    public void Options()
    {
        myOptionsMenu.SetActive(true);
    }

    public void BackFromOptions()
    {
        myOptionsMenu.SetActive(false);
    }

}
