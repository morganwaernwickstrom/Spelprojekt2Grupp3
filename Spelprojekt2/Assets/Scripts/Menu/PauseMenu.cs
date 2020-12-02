using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject myPauseMenu = null;
    [SerializeField] GameObject myPauseButton;


    [SerializeField] Slider myMusicSlider;
    [SerializeField] Slider myFxSlider;
    

    bool myGamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        //myMusicSlider.value = SoundManager.myInstance.GetCurrentMusicVolume();
        //myFxSlider.value = SoundManager.myInstance.GetCurrentEffectsVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (myGamePaused) 
        {
            myPauseMenu.SetActive(true);
            myPauseButton.SetActive(false);
            Time.timeScale = 0;
        }
        else 
        {
            myPauseMenu.SetActive(false);
            myPauseButton.SetActive(true);
            Time.timeScale = 1;
        }

        //SoundManager.myInstance.SetEffectsVolume(myFxSlider.value);
        //SoundManager.myInstance.SetMusicVolume(myMusicSlider.value);

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
    }


}
