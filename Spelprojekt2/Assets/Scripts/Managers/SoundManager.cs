using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    public static SoundManager myInstance { get; private set; }

    [SerializeField] AudioClip[] myMusicClips;
    [SerializeField] AudioClip myDefaultMusicClip;
    [SerializeField] AudioClip myRockSound;
    [SerializeField] AudioClip mySlidingSound;
    [SerializeField] AudioClip myLaserSound;

    [SerializeField] AudioSource myEffectsAudioSource;
    [SerializeField] AudioSource myMusicAudioSource;

    private void Awake()
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

    private void Start()
    {

    }

    public void Update()
    {
        Debug.Log("Music Volume " + myMusicAudioSource.volume);
        Debug.Log("Effects Volume " + myEffectsAudioSource.volume);
    }

    public void PlayRockSound() 
    {
        myEffectsAudioSource.PlayOneShot(myRockSound);
    }

    public void PlaySlidingSound() 
    {
        myEffectsAudioSource.PlayOneShot(mySlidingSound);
    }

    public void PlayLaserSound() 
    {
        myEffectsAudioSource.PlayOneShot(myLaserSound);
    }

    public void SetMusicVolume(float anAmount) 
    {
        myMusicAudioSource.volume = anAmount;
    }

    public void SetEffectsVolume(float anAmount) 
    {
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
