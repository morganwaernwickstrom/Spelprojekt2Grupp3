using UnityEngine;

public class HoleBlocking : MonoBehaviour
{
    private Coord myCoords;
    private bool myShouldDestroy = false;

    private void Awake()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        FindObjectOfType<PlayerMovement>().MoveEvent += OnPlayerMove;
        foreach (var rock in FindObjectsOfType<RockMovement>())
        {
            rock.MoveEvent += OnRockMove;
        }
    }

    private void Update()
    {
        if (myShouldDestroy)
        {
            Destroy(gameObject);
        }
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        return (aPlayerCurrentPos == myCoords);
    }

    private bool OnRockMove(Coord aRockCurrentPos)
    {
        if (aRockCurrentPos == myCoords)
        {
            myShouldDestroy = true;
            return true;
        }
        return false;
    }

    private void OnDestroy()
    {
        if (FindObjectOfType<PlayerMovement>())
        {
            FindObjectOfType<PlayerMovement>().MoveEvent -= OnPlayerMove;
        }
        foreach (var rock in FindObjectsOfType<RockMovement>())
        {
            rock.MoveEvent -= OnRockMove;
        }
    }
}
