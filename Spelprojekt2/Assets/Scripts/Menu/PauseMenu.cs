using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject myPauseMenu;

    [SerializeField] Slider myMusicSlider;
    [SerializeField] Slider myFxSlider;

    bool myGamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myGamePaused) 
        {
            myPauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else 
        {
            myPauseMenu.SetActive(false);
            Time.timeScale = 1;
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
        SceneManager.LoadScene("MainMenu");
    }

    public void LevelSelect() 
    {
        SceneManager.LoadScene("LevelSelect");
    }


}
