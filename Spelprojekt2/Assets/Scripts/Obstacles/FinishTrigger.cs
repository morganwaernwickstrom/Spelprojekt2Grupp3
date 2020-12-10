using UnityEngine.SceneManagement;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private Coord myCoords;
    private bool myShouldReset = false;

    private float mySoundInterval;

    private bool myMakeSound;

    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        mySoundInterval = 5f;
        myMakeSound = true;

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

        Debug.Log("SoundInterval: " + mySoundInterval);

        if(mySoundInterval <= 0 && myMakeSound) 
        {
            SoundManager.myInstance.PlayFiddeSounds();
            mySoundInterval = Random.Range(5, 10);
            myMakeSound = false;
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
