using UnityEngine;
using System.Collections.Generic;

public class FinishTrigger : MonoBehaviour
{
    private Coord myCoords;
    private bool myShouldReset = false;
    private bool myHasPlayed = false;

    private float mySoundInterval;

    private Animator myAnimator;

    private bool myMakeSound;
    private bool myPlayVictorySound;
    private float myWinSoundTimer = 0f;
    private float myWinSoundTimeMax = 8f;
    private bool myHasPlayedOnce = false;

    [SerializeField] Camera myCamera = null;

    [SerializeField] List<ParticleSystem> myConfettiEffects = null;

    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        mySoundInterval = 10f;
        myMakeSound = true;
        myAnimator = GetComponentInChildren<Animator>();
        myCamera.enabled = false;
        myPlayVictorySound = true;
        HideConfetti();
    }

    private void Update()
    {
        if (myShouldReset)
        {
            EventHandler.current.GoalReachedEvent(myCoords);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetTrigger("Dance");
            GetComponentInChildren<Animator>().SetBool("Dance", true);
            RotateCamera();

            if (myPlayVictorySound) 
            {
                if (!myHasPlayedOnce)
                {
                    SoundManager.myInstance.PlayWinSounds();
                    myHasPlayedOnce = true;
                }

                myWinSoundTimer += Time.deltaTime;

                if (myWinSoundTimer >= myWinSoundTimeMax)
                {
                    SoundManager.myInstance.PlayWinSounds();
                    myWinSoundTimer = 0;
                    //myPlayVictorySound = false;
                }

            }

            if (!myHasPlayed)
            {
                HideUI();

                CreateConfetti();
                myHasPlayed = true;
            }
        }

        MakeSound();
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        myShouldReset = (myCoords == aPlayerCurrentPos);
        return (myCoords == aPlayerCurrentPos);
    }

    private void RotateCamera()
    {
        //Camera.main.gameObject.SetActive(false);
        myCamera.enabled = true;

        myCamera.GetComponent<Camera>().gameObject.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        myCamera.transform.RotateAround(GameObject.FindGameObjectWithTag("Player").transform.position, transform.up, Time.fixedDeltaTime * 2f);
    }

    private void MakeSound()
    {
        mySoundInterval -= Time.deltaTime;

        if (mySoundInterval <= 0 && myMakeSound)
        {
            SoundManager.myInstance.PlayFiddeSounds();
            mySoundInterval = Random.Range(10, 25);
            myMakeSound = false;
            myAnimator.SetTrigger("Idle2");
        }
        else
        {
            myMakeSound = true;
        }
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    public Coord GetCoords()
    {
        return myCoords;
    }

    private void HideConfetti()
    {
        foreach (ParticleSystem confetti in myConfettiEffects)
        {
            confetti.gameObject.SetActive(false);
        }
    }

    private void CreateConfetti()
    {
        foreach (ParticleSystem confetti in myConfettiEffects)
        {
            confetti.gameObject.SetActive(true);
            confetti.Play();
        }
    }

    private void HideUI()
    {
        FindObjectsOfType<PauseMenu>()[0].gameObject.SetActive(false);
    }
}
