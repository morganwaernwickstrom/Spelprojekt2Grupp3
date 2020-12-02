using UnityEngine;

public class Laser : MonoBehaviour
{
    private Coord myCoords;

    private void Start()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }

    private void Update()
    {
        myCoords = new Coord(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
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
        // --- Måste detta finnas? skriver över allt som Tile Map sätter med lasrarna --- //

        //if (TileMap.Instance.Get(myCoords) != eTileType.Hole)
        //{
        //    TileMap.Instance.Set(myCoords, eTileType.Laser);
        //}
        if (TileMap.Instance.Get(aPlayerCurrentPos) == eTileType.Laser && aPlayerCurrentPos != aPlayerPreviousPos)
        {
            EventHandler.current.PlayerDeathEvent();
        }
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

    public Coord GetCoords()
    {
        return myCoords;
    }
}
