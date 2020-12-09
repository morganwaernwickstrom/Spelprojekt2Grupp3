using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static SoundManager myInstance { get; private set; }

    [SerializeField] AudioClip[] myMusicClips = null;
    [SerializeField] AudioClip myDefaultMusicClip = null;
    [SerializeField] AudioClip myRockSound = null;
    [SerializeField] AudioClip[] mySlidingSounds;
    [SerializeField] AudioClip myLaserSound = null;
    [SerializeField] AudioClip[] myPlayerDashSounds;
    [SerializeField] AudioClip[] myPlayerPushSounds;
    [SerializeField] AudioClip[] myPlayerKickSounds;
    [SerializeField] AudioClip myRockFallingSound;
    [SerializeField] AudioClip myDoorOpenSound;

    [SerializeField] AudioSource myEffectsAudioSource = null;
    [SerializeField] AudioSource myMusicAudioSource = null;

    [SerializeField] Slider myEffectSlider;
    [SerializeField] Slider myMusicSlider;

    private void Start()
    {
        if(myInstance == null) 
        {
            myInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {

        if (!myMusicClips[SceneManager.GetActiveScene().buildIndex]) 
        {
            myMusicAudioSource.clip = myDefaultMusicClip;
        }
        else 
        {
            myMusicAudioSource.clip = myMusicClips[SceneManager.GetActiveScene().buildIndex];
        }

        if(myMusicAudioSource.clip != myMusicClips[SceneManager.GetActiveScene().buildIndex] || !myMusicAudioSource.isPlaying) 
        {
            Debug.Log("Playing");
            myMusicAudioSource.Play();
        }

        if(myEffectSlider != null && myMusicSlider != null) 
        {
            SetEffectsVolume(myEffectSlider.value);
            SetMusicVolume(myMusicSlider.value);
            myEffectSlider.value = GetCurrentEffectsVolume();
            myMusicSlider.value = GetCurrentMusicVolume();
        }
        
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

    public void PlayPlayerPushSound() 
    {
        myEffectsAudioSource.PlayOneShot(myPlayerPushSounds[Random.Range(0, myPlayerPushSounds.Length)]);
    }

    public void SetMusicVolume(float anAmount) 
    {
        Debug.Log("volume for music set to " + anAmount);
        PlayerPrefs.SetFloat("MusicVolume", anAmount);
        PlayerPrefs.Save();
        myMusicAudioSource.volume = anAmount;
    }

    public void SetEffectsVolume(float anAmount) 
    {
        Debug.Log("volume for effects set to " + anAmount);
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
