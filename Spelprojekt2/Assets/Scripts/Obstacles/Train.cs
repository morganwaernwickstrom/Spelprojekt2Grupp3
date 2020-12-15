﻿using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Vector3 myDestinationRot;
    private float mySpeed = 5f;
    private float myRotationLerpSpeed = 0.05f;
    private Coord myCoords;
    private Coord myPreviousCoords;

    private Stack myPreviousMoves;

    // Start is called before the first frame update
    void Start()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myPreviousCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
        EventHandler.current.Subscribe(eEventType.PlayerMove, OnPlayerMove);
        EventHandler.current.Subscribe(eEventType.Rewind, OnRewind);
        if (gameObject != null)
        {
            myDestinationRot = transform.eulerAngles;
        }
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed * Time.deltaTime);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, myDestinationRot, myRotationLerpSpeed);
    }

    private void OnRewind()
    {
        if (myPreviousMoves.Count > 0)
        {
            var moveInfo = (MoveInfo)myPreviousMoves.Peek();
            myPreviousCoords = myCoords;
            myCoords = moveInfo.coord;
            myDesiredPosition = moveInfo.position;
            myPreviousMoves.Pop();

            TileMap.Instance.Set(myPreviousCoords, eTileType.Rail);
            TileMap.Instance.Set(myCoords, eTileType.Train);
        }
    }

    private bool OnPlayerMove(Coord aPlayerCurrentPos, Coord aPlayerPreviousPos)
    {
        CreateMove();
        if (myCoords == aPlayerCurrentPos)
        {
            if (aPlayerPreviousPos.x == myCoords.x - 1)
            {
                Move(new Coord(1, 0));
            }
            if (aPlayerPreviousPos.x == myCoords.x + 1)
            {
                Move(new Coord(-1, 0));
            }
            if (aPlayerPreviousPos.y == myCoords.y - 1)
            {
                Move(new Coord(0, 1));
            }
            if (aPlayerPreviousPos.y == myCoords.y + 1)
            {
                Move(new Coord(0, -1));
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    private void Move(Coord aDirection)
    {
        Quaternion myRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        Coord previousCoords = myCoords;
        Coord desiredTile = myCoords + aDirection;
        if (TileMap.Instance.Get(desiredTile) == eTileType.Rock ||
            TileMap.Instance.Get(desiredTile) == eTileType.Door ||
            TileMap.Instance.Get(desiredTile) == eTileType.Emitter ||
            TileMap.Instance.Get(desiredTile) == eTileType.Reflector ||
            TileMap.Instance.Get(desiredTile) == eTileType.Receiver ||
            TileMap.Instance.Get(desiredTile) == eTileType.Impassable ||
            TileMap.Instance.Get(desiredTile) == eTileType.Sliding ||
            TileMap.Instance.Get(desiredTile) == eTileType.Train)
            return;

        // TODO: Add Lookup map of to check if tile is empty!
        if (TileMap.Instance.Get(desiredTile) == eTileType.Rail)
        {
            myDesiredPosition += new Vector3(aDirection.x, 0, aDirection.y);
            myCoords += aDirection;
            TileMap.Instance.Set(previousCoords, eTileType.Rail);

            if (aDirection.x > 0 && TileMap.Instance.Get(myCoords + aDirection) != eTileType.Rail)
            {
                myDestinationRot = new Vector3(0, 0, 0);
            }
            else if (aDirection.x < 0 && TileMap.Instance.Get(myCoords + aDirection) != eTileType.Rail)
            {
                myDestinationRot = new Vector3(0, 180, 0);
            }
            else if (aDirection.y > 0 && TileMap.Instance.Get(myCoords + aDirection) != eTileType.Rail)
            {
                myDestinationRot = new Vector3(0, 270, 0); ;
            }
            else if (aDirection.y < 0 && TileMap.Instance.Get(myCoords + aDirection) != eTileType.Rail)
            {
                myDestinationRot = new Vector3(0, 90, 0); ;
            }
        }
        EventHandler.current.RockMoveEvent(myCoords);

    }

    public Coord GetCoords()
    {
        return myCoords;
    }

    private void CreateMove()
    {
        var temp = new MoveInfo();
        temp.coord = myCoords;
        temp.position = transform.position;
        myPreviousMoves.Push(temp);
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerMove, OnPlayerMove);
    }
}
