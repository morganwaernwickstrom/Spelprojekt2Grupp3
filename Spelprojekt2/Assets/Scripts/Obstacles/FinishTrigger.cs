using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private Coord myCoords;
    private bool myShouldReset = false;

    private float mySoundInterval;

    private Animator myAnimator;

    private bool myMakeSound;

    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        mySoundInterval = 10f;
        myMakeSound = true;
        myAnimator = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        if (myShouldReset)
        {
            EventHandler.current.GoalReachedEvent(myCoords);
        }

        MakeSound();
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        myShouldReset = (myCoords == aPlayerCurrentPos);
        return (myCoords == aPlayerCurrentPos);
    }

    private void MakeSound() 
    {
        mySoundInterval -= Time.deltaTime;

        if(mySoundInterval <= 0 && myMakeSound) 
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
}
