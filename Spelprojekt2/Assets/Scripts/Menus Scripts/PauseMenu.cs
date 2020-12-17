using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject myPauseMenu = null;
    [SerializeField] GameObject myPauseButton = null;
    [SerializeField] GameObject myRetryButton = null;
    [SerializeField] GameObject myOptionsMenu = null;
    [SerializeField] GameObject myIngameMenu = null;

    [SerializeField] GameObject myTutorialCards = null;
    [SerializeField] GameObject myHelpButton = null;
    [SerializeField] GameObject myFieldGuide = null;
    [SerializeField] GameObject myCard1 = null;
    [SerializeField] GameObject myCard2 = null;
    [SerializeField] GameObject myCard3 = null;
    [SerializeField] GameObject myCard4 = null;
    [SerializeField] GameObject myFade = null;
    private Animator myFadeAnimator;

    [SerializeField] Slider myEffectsSlider = null;
    [SerializeField] Slider myMusicSlider = null;

    private GameObject myPlayer = null;

    // Rewind
    private bool myCanRewind = true;
    private bool myIsRewinding = false;
    private float myRewindCounter = 0.0f;
    private float myRewindCounterMax = 0.5f;

    private PlayerMovement myPlayerMovement = null;
    private Animator myPlayerAnimator = null;

    private float myEffectsDelta;
    private float myMusicDelta;

    bool myGamePaused = false;
    bool myInTutorial = false;

    private void Start()
    {
        myFadeAnimator = myFade.GetComponent<Animator>();
        myEffectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        myMusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        myEffectsDelta = myEffectsSlider.value;
        myMusicDelta = myMusicSlider.value;

        myPlayerMovement = FindObjectOfType<PlayerMovement>();
        myPlayerAnimator = myPlayerMovement.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Debug.LogError("Mult. " + EventHandler.speedMultiplier);
        myFadeAnimator.SetBool("Fade", myIsRewinding);
        
        //if (EventHandler.isRewinding)
        //{
        //    //Debug.LogError("Rewinding");
        //    EventHandler.speedMultiplier = 5.0f;
        //}
        //else
        //{
        //    //Debug.LogError("Normal");
        //    EventHandler.speedMultiplier = 1.0f;
        //}

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
                myHelpButton.SetActive(false);
            }

            Time.timeScale = 0;
            myPlayer.GetComponent<PlayerMovement>().enabled = false;
        }
        else 
        {
            myPauseMenu.SetActive(false);
            myPauseButton.SetActive(true);
            myRetryButton.SetActive(true);
            myHelpButton.SetActive(true);
            Time.timeScale = 1;
            myPlayer.GetComponent<PlayerMovement>().enabled = true;
        }

        SoundManager.myInstance.SetEffectsVolume(myEffectsSlider.value);
        SoundManager.myInstance.SetMusicVolume(myMusicSlider.value);        

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
        myIngameMenu.SetActive(false);
        myInTutorial = true;
        myPauseMenu.SetActive(false);
        myTutorialCards.SetActive(true);
    }

    public void BackFromTutorials()
    {
        myInTutorial = false;
        myIngameMenu.SetActive(true);
        myPauseMenu.SetActive(true);
        myTutorialCards.SetActive(false);
        myCard1.SetActive(false);
        myCard2.SetActive(false);
        myCard3.SetActive(false);
        myCard4.SetActive(false);
        myFieldGuide.SetActive(true);
    }

    public void Card1()
    {
        myCard1.SetActive(true);
        myCard2.SetActive(false);
        myCard3.SetActive(false);
        myCard4.SetActive(false);
        myFieldGuide.SetActive(false);
    }
    public void Card2()
    {
        myCard1.SetActive(false);
        myCard2.SetActive(true);
        myCard3.SetActive(false);
        myCard4.SetActive(false);
        myFieldGuide.SetActive(false);
    }
    public void Card3()
    {
        myCard1.SetActive(false);
        myCard2.SetActive(false);
        myCard3.SetActive(true);
        myCard4.SetActive(false);
        myFieldGuide.SetActive(false);
    }
    public void Card4()
    {
        myCard1.SetActive(false);
        myCard2.SetActive(false);
        myCard3.SetActive(false);
        myCard4.SetActive(true);
        myFieldGuide.SetActive(false);
    }

    public void Rewind()
    {
        if (EventHandler.canRewind && !EventHandler.isRewinding)
        {
            SoundManager.myInstance.PlayRewindSound();
            EventHandler.current.RewindEvent();
            EventHandler.canRewind = false;
            EventHandler.isRewinding = true;
            myRewindCounter = 0.0f;
        }
    }
}
