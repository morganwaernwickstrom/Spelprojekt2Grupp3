using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private float mySpeed = 0.1f;
    private Coord myCoords;

    // Start is called before the first frame update
    void Start()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
        //EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);???????????
    }

    // Update is called once per frame
    void Update()
    {
        // if desired position is rail, move
        // else stay
    }
}
