using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    private Coord myCoords;
    void Start()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);

    }

    public Coord GetCoords()
    {
        return myCoords;
    }

}
