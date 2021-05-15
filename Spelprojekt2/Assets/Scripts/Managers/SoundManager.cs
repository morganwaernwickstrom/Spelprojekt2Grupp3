using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static SoundManager myInstance { get; private set; }

    #region AudioClips
    [SerializeField] AudioClip[] myMusicClips = null;
    [SerializeField] AudioClip myDefaultMusicClip = null;
    [SerializeField] AudioClip mySnailSound = null;
    [SerializeField] AudioClip myRockSound = null;
    [SerializeField] AudioClip[] mySlidingSounds = null;
    [SerializeField] AudioClip myLaserSound = null;
    [SerializeField] AudioClip myReceiverSound = null;
    [SerializeField] AudioClip[] myPlayerDashSounds = null;
    [SerializeField] AudioClip[] myPlayerPushSounds = null;
    [SerializeField] AudioClip[] myPlayerKickSounds = null;
    [SerializeField] AudioClip[] myPlayerBurnedSounds = null;
    [SerializeField] AudioClip myRockFallingSound = null;
    [SerializeField] AudioClip myDoorOpenSound = null;
    [SerializeField] AudioClip[] myFiddeSounds = null;
    [SerializeField] AudioClip myMenuButtonSound = null;
    [SerializeField] AudioClip myWinSound = null;
    [SerializeField] AudioClip myWinMusic = null;
    [SerializeField] AudioClip myRewindSound = null;
    #endregion

    #region AudioSources
    [SerializeField] AudioSource myEffectsAudioSource = null;
    [SerializeField] AudioSource myMusicAudioSource = null;
    #endregion

    #region Sliders
    [SerializeField] Slider myEffectSlider = null;
    [SerializeField] Slider myMusicSlider = null;
    #endregion

    private bool myHasFinishedLevel = false;

    int myPreSceneIndex = 0;
    int myCurrentSceneIndex = 0;

    private void Start()
    {
        if (myInstance == null)
        {
            myInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        VolumeSliderSetup();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        myCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        VolumeSliderSetup();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        myPreSceneIndex = myCurrentSceneIndex;
        myCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (myEffectsAudioSource != null)
        {
            myEffectsAudioSource.Stop();
        }

        if (myMusicAudioSource != null)
        {
            if (myPreSceneIndex == 0 || myPreSceneIndex >= 2 && myPreSceneIndex <= 7)
            {
                if (myCurrentSceneIndex == 0 || myCurrentSceneIndex >= 2 && myCurrentSceneIndex <= 7)
                {
                    myMusicAudioSource.Play();
                }
            }

            if (myCurrentSceneIndex != 1)
            {
                myMusicAudioSource.clip = myMusicClips[myCurrentSceneIndex];
                myMusicAudioSource.Play();
            }

        }
    }


    public void Update()
    {
        ManageMusic();
        SliderManager();

        if (FindObjectsOfType<SFX>().Length > 0)
        {
            myEffectSlider = FindObjectsOfType<SFX>()[0].gameObject.GetComponent<Slider>();
            myMusicSlider = FindObjectsOfType<MUSIC>()[0].gameObject.GetComponent<Slider>();
        }
    }

    private void VolumeSliderSetup()
    {
        if (GameObject.FindGameObjectWithTag("SoundEffectSlider"))
        {
            myEffectSlider = FindObjectsOfType<SFX>()[0].gameObject.GetComponent<Slider>();
            myMusicSlider = FindObjectsOfType<MUSIC>()[0].gameObject.GetComponent<Slider>();
        }

        if (myEffectSlider != null && myMusicSlider != null)
        {
            if (PlayerPrefs.HasKey("EffectsVolume") && PlayerPrefs.HasKey("MusicVolume"))
            {
                SetEffectsVolume(PlayerPrefs.GetFloat("EffectsVolume"));
                SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));

                myEffectSlider.value = GetCurrentEffectsVolume();
                myMusicSlider.value = GetCurrentMusicVolume();
            }
            else
            {
                SetEffectsVolume(1.0f);
                SetMusicVolume(1.0f);

                myEffectSlider.value = GetCurrentEffectsVolume();
                myMusicSlider.value = GetCurrentMusicVolume();
            }
        }
    }

    private void SliderManager()
    {
        if (myEffectSlider != null && myMusicSlider != null)
        {
            SetEffectsVolume(myEffectSlider.value);
            SetMusicVolume(myMusicSlider.value);
        }
    }

    private void ManageMusic()
    {
        if (!myHasFinishedLevel)
        {
            if (!myMusicClips[myCurrentSceneIndex])
            {
                myMusicAudioSource.clip = myDefaultMusicClip;
            }
            else
            {
                myMusicAudioSource.clip = myMusicClips[myCurrentSceneIndex];
            }

            if (myMusicAudioSource.clip != myMusicClips[myCurrentSceneIndex] || !myMusicAudioSource.isPlaying)
            {
                myMusicAudioSource.Play();
            }
        }
    }

    public void PlaySnailSound()
    {
        myEffectsAudioSource.PlayOneShot(mySnailSound);
    }

    public void PlayRockSound()
    {
        myEffectsAudioSource.PlayOneShot(myRockSound);
    }

    public void PlaySlidingSound()
    {
        myEffectsAudioSource.PlayOneShot(mySlidingSounds[Random.Range(0, mySlidingSounds.Length)]);
    }

    public void PlayRockFallingSound()
    {
        myEffectsAudioSource.PlayOneShot(myRockFallingSound);
    }

    public void PlayDoorOpenSound()
    {
        myEffectsAudioSource.PlayOneShot(myDoorOpenSound);
    }

    public void PlayMenuButtonSound()
    {
        myEffectsAudioSource.PlayOneShot(myMenuButtonSound);
    }

    public void PlayReceiverSound()
    {
        myEffectsAudioSource.PlayOneShot(myReceiverSound);
    }

    public void PlayLaserSound()
    {
        myEffectsAudioSource.PlayOneShot(myLaserSound);
    }

    public void PlayPlayerDashSound()
    {
        myEffectsAudioSource.PlayOneShot(myPlayerDashSounds[Random.Range(0, myPlayerDashSounds.Length)]);
    }

    public void PlayPlayerKickSound()
    {
        myEffectsAudioSource.PlayOneShot(myPlayerKickSounds[Random.Range(0, myPlayerKickSounds.Length)]);
    }

    public void PlayWinSounds()
    {
        myHasFinishedLevel = true;
        myMusicAudioSource.Stop();
        myEffectsAudioSource.PlayOneShot(myWinSound);
        myEffectsAudioSource.PlayOneShot(myWinMusic);
    }

    public void PlayFiddeSounds()
    {
        myEffectsAudioSource.PlayOneShot(myFiddeSounds[Random.Range(0, myFiddeSounds.Length)]);
    }

    public void PlayPlayerPushSound()
    {
        myEffectsAudioSource.PlayOneShot(myPlayerPushSounds[Random.Range(0, myPlayerPushSounds.Length)]);
    }

    public void PlayPlayerBurnedSound()
    {
        myEffectsAudioSource.PlayOneShot(myPlayerBurnedSounds[Random.Range(0, myPlayerBurnedSounds.Length)]);
    }
    public void PlayRewindSound()
    {
        myEffectsAudioSource.PlayOneShot(myRewindSound);
    }

    public void SetMusicVolume(float anAmount)
    {
        PlayerPrefs.SetFloat("MusicVolume", anAmount);
        PlayerPrefs.Save();
        myMusicAudioSource.volume = anAmount;
    }

    public void SetEffectsVolume(float anAmount)
    {
        PlayerPrefs.SetFloat("EffectsVolume", anAmount);
        PlayerPrefs.Save();
        myEffectsAudioSource.volume = anAmount;
    }


    public float GetCurrentEffectsVolume()
    {
        return PlayerPrefs.GetFloat("EffectsVolume");
    }

    public float GetCurrentMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume");
    }
}
