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

    [SerializeField] GameObject myTutorialCards = null;
    [SerializeField] GameObject myHelpButton = null;
    [SerializeField] GameObject myCard1 = null;
    [SerializeField] GameObject myCard2 = null;
    [SerializeField] GameObject myCard3 = null;

    [SerializeField] Slider myEffectsSlider;
    [SerializeField] Slider myMusicSlider;

    private GameObject myPlayer;

    private float myEffectsDelta;
    private float myMusicDelta;

    bool myGamePaused = false;
    bool myInTutorial = false;

    private void Start()
    {
        myEffectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        myMusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        myEffectsDelta = myEffectsSlider.value;
        myMusicDelta = myMusicSlider.value;
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player")) 
        {
            myPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            Debug.LogError("PLAYER WITH TAG PLAYER NOT FOUND");
        }
        

        if (myGamePaused) 
        {
            if (!myInTutorial)
            {
                myPauseMenu.SetActive(true);
                myPauseButton.SetActive(false);
                myRetryButton.SetActive(false);
            }

            Time.timeScale = 0;
            myPlayer.GetComponent<PlayerMovement>().enabled = false;
        }
        else 
        {
            myPauseMenu.SetActive(false);
            myPauseButton.SetActive(true);
            myRetryButton.SetActive(true);
            Time.timeScale = 1;
            myPlayer.GetComponent<PlayerMovement>().enabled = true;
        }

        /*
        if(myEffectsSlider.value != myEffectsDelta) 
        {
            SoundManager.myInstance.SetEffectsVolume(myEffectsSlider.value);
            myEffectsDelta = myEffectsSlider.value;
        }
        */

        SoundManager.myInstance.SetEffectsVolume(myEffectsSlider.value);
        SoundManager.myInstance.SetMusicVolume(myMusicSlider.value);

        /*
        if (myMusicSlider.value != myMusicDelta) 
        {
            SoundManager.myInstance.SetEffectsVolume(myEffectsSlider.value);
            myMusicDelta = myMusicSlider.value;
        }
        */
        

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

    public void TutorialCards()
    {
        myInTutorial = true;
        myHelpButton.SetActive(false);
        myPauseMenu.SetActive(false);
        myTutorialCards.SetActive(true);
        myCard1.SetActive(true);
    }

    public void BackFromTutorials()
    {
        myInTutorial = false;
        myHelpButton.SetActive(true);
        myPauseMenu.SetActive(true);
        myTutorialCards.SetActive(false);
        myCard1.SetActive(false);
        myCard2.SetActive(false);
        myCard3.SetActive(false);
    }

    public void Card1()
    {
        myCard1.SetActive(true);
        myCard2.SetActive(false);
        myCard3.SetActive(false);
    }
    public void Card2()
    {
        myCard1.SetActive(false);
        myCard2.SetActive(true);
        myCard3.SetActive(false);
    }
    public void Card3()
    {
        myCard1.SetActive(false);
        myCard2.SetActive(false);
        myCard3.SetActive(true);
    }
}
