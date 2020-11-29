using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Coord myCoords;
    private Coord myPreviousCoords;

    private Animator myAnimator;

    [SerializeField]
    GameObject myCharacterModel;

    [SerializeField]
    float myMovementSpeed = 3f;

    private float mySpeed = 0f;

    [SerializeField] private float myDeadzone = 100.0f;
    [SerializeField] private float doubleTapDelta = 0.5f;

    [SerializeField] private int myMaxXCoordinate;
    [SerializeField] private int myMaxZCoordinate;
    [SerializeField] private int myMinXCoordinate;
    [SerializeField] private int myMinZCoordinate;

    private GameObject[] myTiles;

    private bool tap, doubleTap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;
    private float lastTap;
    private float sqrDeadzone;
    private float percentage;

    public bool Tap { get { return tap; } }
    public bool DoubleTap { get { return doubleTap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    private void Awake()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
    }

    private void Start()
    {
        mySpeed = myMovementSpeed;
        sqrDeadzone = myDeadzone * myDeadzone;
        percentage = 0.0f;
        myAnimator = GetComponentInChildren<Animator>();
        myTiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    private void Update()
    {
        tap = doubleTap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        UpdateMobile();
        UpdateStandalone();
        Movement();
        WasdMovement();

        if (percentage > 1.0f)
        {
            transform.position = myDesiredPosition;
        }
        else 
        {
            percentage += Time.deltaTime * mySpeed;

            transform.position = Vector3.Lerp(transform.position, myDesiredPosition, percentage);
        }
    }

    //Movement using mouse
    private void UpdateStandalone()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
            doubleTap = Time.time - lastTap < doubleTapDelta;
            lastTap = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }

        swipeDelta = Vector2.zero;

        if (startTouch != Vector2.zero && Input.GetMouseButton(0))
        {
            swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        if (swipeDelta.sqrMagnitude > sqrDeadzone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }
            startTouch = swipeDelta = Vector2.zero;
        }
    }

    //Movement using mobile controls
    private void UpdateMobile()
    {
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
                doubleTap = Time.time - lastTap < doubleTapDelta;
                Debug.Log(Time.time - lastTap);
                lastTap = Time.time;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }
        }

        swipeDelta = Vector2.zero;

        if (startTouch != Vector2.zero && Input.touches.Length != 0)
        {
            swipeDelta = Input.touches[0].position - startTouch;
        }

        if (swipeDelta.sqrMagnitude > sqrDeadzone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }
            startTouch = swipeDelta = Vector2.zero;
        }
    }

    //Movement using WASD 
    private void WasdMovement()
    {
        Coord originalCoord = myCoords;

        myPreviousCoords = myCoords;

        float distance = Vector3.Distance(transform.position, myDesiredPosition);

        Vector3 myPlayerPosition = transform.position;

        Quaternion myRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);

        if (transform.position == myDesiredPosition)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Coord desiredTile = myCoords + new Coord(0, 1);
                myRotation = Quaternion.Euler(0, 0, 0);
                myCharacterModel.transform.rotation = myRotation;
                if (TileMap.Instance.Get(desiredTile) != eTileType.Null)
                {
                    percentage = 0;
                    myDesiredPosition += new Vector3(0, 0, 1);
                    myCoords.y += 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Coord desiredTile = myCoords + new Coord(0, -1);
                myRotation = Quaternion.Euler(0, 180, 0);
                myCharacterModel.transform.rotation = myRotation;
                if (TileMap.Instance.Get(desiredTile) != eTileType.Null)
                {
                    percentage = 0;
                    myDesiredPosition += new Vector3(0, 0, -1);
                    myCoords.y -= 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Coord desiredTile = myCoords + new Coord(-1, 0);
                myRotation = Quaternion.Euler(0, -90, 0);
                myCharacterModel.transform.rotation = myRotation;
                if (TileMap.Instance.Get(desiredTile) != eTileType.Null)
                {
                    percentage = 0;
                    myDesiredPosition += new Vector3(-1, 0, 0);
                    myCoords.x -= 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Coord desiredTile = myCoords + new Coord(1, 0);
                myRotation = Quaternion.Euler(0, 90, 0);
                myCharacterModel.transform.rotation = myRotation;
                if (TileMap.Instance.Get(desiredTile) != eTileType.Null)
                {
                    percentage = 0;
                    myDesiredPosition += new Vector3(1, 0, 0);
                    myCoords.x += 1;
                }
            }
        }

        if (distance < 0.1) 
        {
            myAnimator.SetBool("Walk", false);
        }
        else 
        {
            myAnimator.SetBool("Walk", true);
        }


        if (EventHandler.current.PlayerMoveEvent(myCoords, myPreviousCoords))
        {
            myDesiredPosition = transform.position;
            myCoords = originalCoord;
        }
        EventHandler.current.PlayerInteractEvent(myCoords, myPreviousCoords);

        TileMap.Instance.Set(myCoords, eTileType.Player);
        myDesiredPosition = new Vector3(Mathf.Round(myDesiredPosition.x), myDesiredPosition.y, Mathf.Round(myDesiredPosition.z));
    }

    private void Movement()
    {
        Coord originalCoord = myCoords;

        myPreviousCoords = myCoords;

        Quaternion myRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);

        Vector3 myPlayerPosition = transform.position;

        if (transform.position == myDesiredPosition)
        {
            if (swipeUp)
            {
                if(TileAhead(myPlayerPosition += new Vector3(0, 0, 1))) 
                {
                    percentage = 0;
                    myDesiredPosition += new Vector3(0, 0, 1);
                    myRotation = Quaternion.Euler(0, 0, 0);
                    myCoords.y += 1;
                    myCharacterModel.transform.rotation = myRotation;
                }
            }
            if (swipeDown)
            {
                if(TileAhead(myPlayerPosition += new Vector3(0, 0, -1))) 
                {
                    percentage = 0;
                    myDesiredPosition += new Vector3(0, 0, -1);
                    myCoords.y -= 1;
                    myRotation = Quaternion.Euler(0, 180, 0);
                    myCharacterModel.transform.rotation = myRotation;
                }
            }
            if (swipeLeft)
            {
                if (TileAhead(myPlayerPosition += new Vector3(-1, 0, 0)))
                {
                    percentage = 0;
                    myDesiredPosition += new Vector3(-1, 0, 0);
                    myRotation = Quaternion.Euler(0, -90, 0);
                    myCoords.x -= 1;
                    myCharacterModel.transform.rotation = myRotation;
                }
            }
            if (swipeRight)
            {
                if(TileAhead(myPlayerPosition += new Vector3(1, 0, 0))) 
                {
                    percentage = 0;
                    myDesiredPosition += new Vector3(1, 0, 0);
                    myRotation = Quaternion.Euler(0, 90, 0);
                    myCoords.x += 1;
                    myCharacterModel.transform.rotation = myRotation;
                }
            }            
        }

        if (EventHandler.current.PlayerMoveEvent(myCoords, myPreviousCoords))
        {
            myDesiredPosition = transform.position;
            myCoords = originalCoord;
        }

        EventHandler.current.PlayerInteractEvent(myCoords, myPreviousCoords);
        myDesiredPosition = new Vector3(Mathf.Round(myDesiredPosition.x), myDesiredPosition.y, Mathf.Round(myDesiredPosition.z));
    }

    bool TileAhead(Vector3 aPosition)
    {
        print(aPosition);

        Vector3 myDesPos = new Vector3(aPosition.x, transform.position.y - 1, aPosition.z);

        foreach(GameObject aTile in myTiles)
        {
            float myDistance = Vector3.Distance(myDesPos, aTile.transform.position);
            if (myDistance <= 0.1f) 
            {
                return true;
            }
        }

        return false;
    }

    public Coord GetCoords()
    {
        return myCoords;
    }
}