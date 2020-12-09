using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    public static SoundManager myInstance { get; private set; }

    [SerializeField] AudioClip[] myMusicClips = null;
    [SerializeField] AudioClip myDefaultMusicClip = null;
    [SerializeField] AudioClip myRockSound = null;
    [SerializeField] AudioClip[] mySlidingSounds;
    [SerializeField] AudioClip myLaserSound = null;

    [SerializeField] AudioSource myEffectsAudioSource = null;
    [SerializeField] AudioSource myMusicAudioSource = null;

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
        
    }

    public void PlayRockSound() 
    {
        myEffectsAudioSource.PlayOneShot(myRockSound);
    }

    public void PlaySlidingSound()
    {
        myEffectsAudioSource.PlayOneShot(mySlidingSounds[Random.Range(0, mySlidingSounds.Length)]);
    }

    public void PlayLaserSound() 
    {
        myEffectsAudioSource.PlayOneShot(myLaserSound);
    }

    public void SetMusicVolume(float anAmount) 
    {
        Debug.Log("volume for music set to " + anAmount);
        myMusicAudioSource.volume = anAmount;
    }

    public void SetEffectsVolume(float anAmount) 
    {
        Debug.Log("volume for effects set to " + anAmount);
        myEffectsAudioSource.volume = anAmount;
    }

    public float GetCurrentEffectsVolume() 
    {
        return myEffectsAudioSource.volume;
    }

    public float GetCurrentMusicVolume() 
    {
        return myMusicAudioSource.volume;
    }
}
