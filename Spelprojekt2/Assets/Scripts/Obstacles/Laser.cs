using UnityEngine;

public class Laser : MonoBehaviour
{
    private Coord myCoords;

    //private void Awake()
    //{
    //    myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    //    EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
    //}

    private void OnEnable()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
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
