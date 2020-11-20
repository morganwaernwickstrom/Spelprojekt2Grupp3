using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    private Coord myCoords;
    private bool myShouldReset = false;
    private bool myCoroutineRunning = false;
    private GameObject myPlayer;

    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    private void Update()
    {

        myPlayer = GameObject.FindGameObjectWithTag("Player");

        if (myShouldReset)
        {
            if(!myCoroutineRunning)
            {
                StartCoroutine(RestartAfterDeath());
                myCoroutineRunning = true;
            }
        }
    }

    private void OnEnable()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        if (EventHandler.current != null)
        {
            EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        }
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        myShouldReset = (myCoords == aPlayerCurrentPos);
        return (aPlayerCurrentPos == myCoords);
    }

    private void OnDisable()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    private IEnumerator RestartAfterDeath() 
    {
        myPlayer.GetComponentInChildren<Animator>().SetBool("Die", true);
        myPlayer.GetComponent<PlayerMovement>().enabled = false;
        Debug.Log("Working");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
