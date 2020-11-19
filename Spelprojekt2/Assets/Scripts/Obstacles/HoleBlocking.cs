using UnityEngine;

public class HoleBlocking : MonoBehaviour
{
    private Coord myCoords;

    private void Start()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.RockMove, OnRockMove);
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        return (aPlayerCurrentPos == myCoords);
    }

    private bool OnRockMove(Coord aRockCurrentPos)
    {
        if (aRockCurrentPos == myCoords)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public Coord GetCoords()
    {
        return myCoords;
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.UnSubscribe(eEventType.RockMove, OnRockMove);
    }
}
