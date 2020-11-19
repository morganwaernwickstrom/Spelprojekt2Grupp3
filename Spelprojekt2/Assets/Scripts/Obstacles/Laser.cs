using UnityEngine.SceneManagement;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Coord myCoords;
    private bool myShouldReset = false;

    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    private void Update()
    {
        if (myShouldReset)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
}
