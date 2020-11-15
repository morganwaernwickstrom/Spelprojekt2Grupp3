using UnityEngine.SceneManagement;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private Coord myCoords;
    private bool myShouldReset = false;

    private void Awake()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    private void Update()
    {
        if (myShouldReset)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        myShouldReset = (myCoords == aPlayerCurrentPos);
        return (myCoords == aPlayerCurrentPos);
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }

}
