using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Coord myCoords;
    private Coord myPreviousCoords;

    [SerializeField]
    float mySpeed = 0.1f;

    [SerializeField] private float myDeadzone = 100.0f;
    [SerializeField] private float doubleTapDelta = 0.5f;

    private bool tap, doubleTap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;
    private float lastTap;
    private float sqrDeadzone;

    public bool Tap { get { return tap; } }
    public bool DoubleTap { get { return doubleTap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    private void WasdMovement()
    {
        Coord originalCoord = myCoords;

        myPreviousCoords = myCoords;

        if (transform.position == myDesiredPosition)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                myDesiredPosition += new Vector3(0, 0, 1);
                myCoords.y += 1;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                myDesiredPosition += new Vector3(0, 0, -1);
                myCoords.y -= 1;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                myDesiredPosition += new Vector3(-1, 0, 0);
                myCoords.x -= 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                myDesiredPosition += new Vector3(1, 0, 0);
                myCoords.x += 1;
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

    private void Awake()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
        myDesiredPosition = transform.position;
    }

    private void Start()
    {
        sqrDeadzone = myDeadzone * myDeadzone;
    }

    private void Update()
    {
        tap = doubleTap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        UpdateMobile();
        UpdateStandalone();
        Movement();
        WasdMovement();

        transform.position = Vector3.Lerp(transform.position, myDesiredPosition, mySpeed);

    }

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

    private void Movement()
    {
        Coord originalCoord = myCoords;

        myPreviousCoords = myCoords;

        if (transform.position == myDesiredPosition)
        {
            if (swipeUp)
            {
                myDesiredPosition += new Vector3(0, 0, 1);
                myCoords.y += 1;
            }
            if (swipeDown)
            {
                myDesiredPosition += new Vector3(0, 0, -1);
                myCoords.y -= 1;
            }
            if (swipeLeft)
            {
                myDesiredPosition += new Vector3(-1, 0, 0);
                myCoords.x -= 1;
            }
            if (swipeRight)
            {
                myDesiredPosition += new Vector3(1, 0, 0);
                myCoords.x += 1;
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
}
