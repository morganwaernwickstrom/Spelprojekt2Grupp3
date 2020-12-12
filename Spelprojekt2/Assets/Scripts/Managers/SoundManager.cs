﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static SoundManager myInstance { get; private set; }

    #region AudioClips
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
    [SerializeField] AudioClip[] myFiddeSounds;
    [SerializeField] AudioClip myMenuButtonSound;
    #endregion

    #region AudioSources
    [SerializeField] AudioSource myEffectsAudioSource = null;
    [SerializeField] AudioSource myMusicAudioSource = null;
    #endregion

    #region Sliders
    [SerializeField] Slider myEffectSlider;
    [SerializeField] Slider myMusicSlider;
    #endregion

    private int myFirstStartUp = -1;

    private void Start()
    {
        if(myInstance == null) 
        {
            myInstance = this;
            myEffectSlider.value = 0.5f;
            myMusicSlider.value = 0.5f;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }

        VolumeSliderSetup();
    }

    public void Update()
    {
        ManageMusic();
        SliderManager();
    }

    private void VolumeSliderSetup() 
    {
        if (myEffectSlider != null && myMusicSlider != null)
        {
            SetEffectsVolume(PlayerPrefs.GetFloat("EffectsVolume"));
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));

            myEffectSlider.value = GetCurrentEffectsVolume();
            myMusicSlider.value = GetCurrentMusicVolume();
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
        if (!myMusicClips[SceneManager.GetActiveScene().buildIndex])
        {
            myMusicAudioSource.clip = myDefaultMusicClip;
        }
        else
        {
            myMusicAudioSource.clip = myMusicClips[SceneManager.GetActiveScene().buildIndex];
        }

        if (myMusicAudioSource.clip != myMusicClips[SceneManager.GetActiveScene().buildIndex] || !myMusicAudioSource.isPlaying)
        {
            myMusicAudioSource.Play();
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

    public void PlayMenuButtonSound() 
    {
        myEffectsAudioSource.PlayOneShot(myMenuButtonSound);
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

    public void PlayFiddeSounds() 
    {
        myEffectsAudioSource.PlayOneShot(myFiddeSounds[Random.Range(0, myFiddeSounds.Length)]);
    }

    public void PlayPlayerPushSound() 
    {
        myEffectsAudioSource.PlayOneShot(myPlayerPushSounds[Random.Range(0, myPlayerPushSounds.Length)]);
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
