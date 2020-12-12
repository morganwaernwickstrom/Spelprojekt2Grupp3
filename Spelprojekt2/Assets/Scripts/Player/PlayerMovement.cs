using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    #region serializefields
    //SerializeFields

    //GameObject
    [SerializeField] private GameObject myCharacterModel;

    //Float
    [SerializeField] private float myDeadzone = 100.0f;
    [SerializeField] private float doubleTapDelta = 0.5f;

    //Int
    [SerializeField] private int myMaxXCoordinate;
    [SerializeField] private int myMaxZCoordinate;
    [SerializeField] private int myMinXCoordinate;
    [SerializeField] private int myMinZCoordinate;
    #endregion

    #region private variables
    //Private Variables

    //Coord
    private Coord myCoords;
    private Coord myPreviousCoords;
    private Coord myOriginalCoord;
    private Coord myDesiredTile;

    //GameObject
    private GameObject[] myTiles;

    //Bool
    private bool tap, doubleTap, swipeLeft, swipeRight, swipeUp, swipeDown;

    //Vector3
    private Vector3 myDesiredPosition;

    //Vector2
    private Vector2 swipeDelta, startTouch;

    //Float
    private float lastTap;
    private float sqrDeadzone;
    private float percentage;
    private float mySpeed = 0f;
    private float myMovementSpeed = 15f;

    //Quaternion
    private Quaternion myRotation;

    //Misc
    private Animator myAnimator;
    private Queue myCommandQueue = new Queue();

    #endregion

    #region public variables
    //public variables

    //Bool
    public bool Tap { get { return tap; } }

    public bool DoubleTap { get { return doubleTap; } }

    public bool SwipeRight { get { return swipeRight; } }

    public bool SwipeUp { get { return swipeUp; } }

    public bool SwipeDown { get { return swipeDown; } }

    public bool SwipeLeft { get { return swipeLeft; } }

    //Vector
    public Vector2 SwipeDelta { get { return swipeDelta; } }

    #endregion

    private void Start()
    {
        mySpeed = myMovementSpeed;
        sqrDeadzone = myDeadzone * myDeadzone;
        percentage = 0.0f;
        myAnimator = GetComponentInChildren<Animator>();
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
    }

    private void Update()
    {
        tap = doubleTap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        UpdateMobile();

        UpdateStandalone();

        WasdMovement();

        AnimationHandler();

        HandleCommandQueue();

        HandleLerpLogic();

    }

    private void HandleLerpLogic()
    {
        if (ComparePositions(transform.position, myDesiredPosition, 0.005f))
        {
            transform.position = myDesiredPosition;
        }
        else if (transform.position != myDesiredPosition)
        {
            transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed * Time.deltaTime);
        }
        
        //percentage += Time.fixedDeltaTime * mySpeed;


        //if (percentage > 1.0f)
        //{
        //    //transform.position = myDesiredPosition;
        //}
        //else
        //{
            
        //}
    }

    private bool ComparePositions(Vector3 aPosition, Vector3 aDesiredPosition, float aDif)
    {
        bool xDist = (Mathf.Abs(aPosition.x - aDesiredPosition.x) < aDif);
        bool yDist = (Mathf.Abs(aPosition.y - aDesiredPosition.y) < aDif);
        bool zDist = (Mathf.Abs(aPosition.z - aDesiredPosition.z) < aDif);

        return (xDist && yDist && zDist);
    }

    private bool CanAddCommand()
    {
        if(myCommandQueue.Count == 0)
        {
            return true;
        }

        return false;
    }

    private void HandleCommandQueue()
    {
        if (myCommandQueue.Count != 0)
        {
            ExecuteCommands();
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
                    if (CanAddCommand())
                        AddCommand("left");
                }
                else
                {
                    if (CanAddCommand())
                        AddCommand("right");
                }
            }
            else
            {
                if (y < 0)
                {
                    if (CanAddCommand())
                        AddCommand("down");
                }
                else
                {
                    if (CanAddCommand())
                        AddCommand("up");
                }
            }
            startTouch = swipeDelta = Vector2.zero;
        }
    }

    private void AddCommand<T>(T aCommand) 
    {
        if (transform.position == myDesiredPosition)
        {
            myCommandQueue.Enqueue(aCommand);
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
                    if (CanAddCommand())
                        AddCommand("left");
                }
                else
                {
                    if (CanAddCommand())
                        AddCommand("right");
                }
            }
            else
            {
                if (y < 0)
                {
                    if (CanAddCommand())
                        AddCommand("down");
                }
                else
                {
                    if (CanAddCommand())
                        AddCommand("up");
                }

                startTouch = swipeDelta = Vector2.zero;
            }
        }
    }

    public void Push()
    {

        int myRandom = Random.Range(0, 10);

        Debug.Log("myRandom: " + myRandom);

        if(myRandom < 5) 
        {
            myAnimator.SetTrigger("Push");
            SoundManager.myInstance.PlayPlayerPushSound();
        }
        else 
        {
            myAnimator.SetTrigger("Kick");
            SoundManager.myInstance.PlayPlayerKickSound();
        }
        
    }

    private void ExecuteCommands()
    {
        if (IsPlayerAtDestination()) 
        {
            switch (myCommandQueue.Peek()) 
            {
                case "left":
                    MoveLeft();
                    break;
                case "right":
                    MoveRight();
                    break;
                case "up":
                    MoveUp();
                    break;
                case "down":
                    MoveDown();
                    break;
            }

            myCommandQueue.Dequeue();
        }
    }

    private Quaternion GetPlayerRotation()
    {
        return Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }

    private bool IsPlayerAtDestination() 
    {
        if(transform.position == myDesiredPosition) 
        {
            return true;
        }

        return false;
    }

    private void MoveLeft()
    {
        myOriginalCoord = myCoords;

        myRotation = GetPlayerRotation();

        myPreviousCoords = myCoords;

        myDesiredTile = myCoords + new Coord(-1, 0);

        myRotation = Quaternion.Euler(0, -90, 0);
        myCharacterModel.transform.rotation = myRotation;

        if (CanMove(myDesiredTile))
        {
            percentage = 0;
            myDesiredPosition += new Vector3(-1, 0, 0);
            myCoords.x -= 1;
        }

        EventHandlerManager();
        
    }

    private void MoveRight() 
    {
        myOriginalCoord = myCoords;

        myRotation = GetPlayerRotation();

        myPreviousCoords = myCoords;

        myDesiredTile = myCoords + new Coord(1, 0);

        myRotation = Quaternion.Euler(0, 90, 0);
        myCharacterModel.transform.rotation = myRotation;

        if (CanMove(myDesiredTile))
        {
            percentage = 0;
            myDesiredPosition += new Vector3(1, 0, 0);
            myCoords.x += 1;
        }

        EventHandlerManager();

    }

    private void MoveUp()
    {
        myOriginalCoord = myCoords;

        myRotation = GetPlayerRotation();

        myPreviousCoords = myCoords;

        myDesiredTile = myCoords + new Coord(0, 1);

        myRotation = Quaternion.Euler(0, 0, 0);
        myCharacterModel.transform.rotation = myRotation;

        if (CanMove(myDesiredTile))
        {
            percentage = 0;
            myDesiredPosition += new Vector3(0, 0, 1);
            myCoords.y += 1;
        }

        EventHandlerManager();

    }

    private void MoveDown()
    {
        myOriginalCoord = myCoords;

        myRotation = GetPlayerRotation();

        myPreviousCoords = myCoords;

        myDesiredTile = myCoords + new Coord(0, -1);

        myRotation = Quaternion.Euler(0, 180, 0);
        myCharacterModel.transform.rotation = myRotation;

        if (CanMove(myDesiredTile))
        {
            percentage = 0;
            myDesiredPosition += new Vector3(0, 0, -1);
            myCoords.y -= 1;
        }

        EventHandlerManager();

    }

    private void EventHandlerManager()
    {
        if (EventHandler.current.PlayerMoveEvent(myCoords, myPreviousCoords))
        {
            if(TileMap.Instance.Get(myCoords) == eTileType.Laser) 
            {
                PlayLaserAnimation();
            }
            else 
            {

                Push();
            }

            myDesiredPosition = transform.position;
            myCoords = myOriginalCoord;

        }
        else 
        {
            SoundManager.myInstance.PlayPlayerDashSound();
            PlayJumpAnimation();
        }
        EventHandler.current.PlayerInteractEvent(myCoords, myPreviousCoords);

        TileMap.Instance.Set(myCoords, eTileType.Player);
        myDesiredPosition = new Vector3(Mathf.Round(myDesiredPosition.x), myDesiredPosition.y, Mathf.Round(myDesiredPosition.z));
    }

    private void WasdMovement()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            if (CanAddCommand())
                AddCommand("up");
        }

        if (Input.GetKeyDown(KeyCode.S)) 
        {
            if (CanAddCommand())
                AddCommand("down");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CanAddCommand())
                AddCommand("left");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (CanAddCommand())
                AddCommand("right");
        }
    }

    private void PlayJumpAnimation() 
    {
        myAnimator.SetTrigger("Jump");
    }

    private void PlayLaserAnimation() 
    {
        myAnimator.SetTrigger("Whiplash");
    }

    private void AnimationHandler() 
    {

        float distance = Vector3.Distance(transform.position, myDesiredPosition);

        if (distance < 0.1)
        {
            //myAnimator.SetBool("Walk", false);
            
        }
        else
        {
            //myAnimator.SetBool("Walk", true);
        }

    }

    bool CanMove(Coord aCoord)
    {
        return (TileMap.Instance.Get(aCoord) != eTileType.Null);
    }

    public Coord GetCoords()
    {
        return myCoords;
    }
}