using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    private Coord myCoords;
    private Vector3 RockDesiredPosition;

    // Start is called before the first frame update
    void Start()
    {
        myCoords = new Coord((int)transform.poistion.x, (int)transform.position.z);
        EventHandler.current.Subscribe(eEventType.PlayerMove,OnPlayerMove)
    }

    // Update is called once per frame
    void Update()
    {

        if (RockDesiredPosition == myCoords.z + 1 || RockDesiredPosition == myCoords.z - 1)
        {
            RockDesiredPosition = myCoords;
        }
    }
}
