using System;
using UnityEngine;

public enum eEventType
{
    PlayerMove,
    PlayerInteract,
    RockMove,
    RockInteract,
    ButtonPressed,
    ButtonUp
}

public class EventHandler : MonoBehaviour
{
    public static EventHandler current = null;
    public event Func<Coord, Coord, bool> onPlayerMoveEvent;
    public event Func<Coord, Coord, bool> onPlayerInteractEvent;
    public event Func<Coord, bool> onRockMoveEvent;
    public event Func<Coord, Coord, bool> onRockInteractEvent;
    public event Func<bool> onButtonPressed;
    public event Func<bool> onButtonUp;

    private void Start()
    {
        if (current == null) current = this;
    }

    private void OnEnable()
    {
        if (current == null) current = this;
    }

    public void Subscribe(eEventType aType, Func<Coord, Coord, bool> aFunc)
    {
        switch (aType)
        {
            case eEventType.PlayerMove:
                onPlayerMoveEvent += aFunc;
                break;
            case eEventType.PlayerInteract:
                onPlayerInteractEvent += aFunc;
                break;
            case eEventType.RockInteract:
                onRockInteractEvent += aFunc;
                break;
            default:
                break;
        }
    }

    public void Subscribe(eEventType aType, Func<Coord, bool> aFunc)
    {
        switch (aType)
        {
            case eEventType.RockMove:
                for (int i = 0; i < FindObjectsOfType<RockMovement>().Length; i++)
                {
                    onRockMoveEvent += aFunc;
                }
                break;
            default:
                break;
        }
    }

    public void Subscribe(eEventType aType, Func<bool> aFunc)
    {
        switch (aType)
        {
            case eEventType.ButtonPressed:
                onButtonPressed += aFunc;    
                break;
            case eEventType.ButtonUp:
                onButtonUp += aFunc;
                break;
            default:
                break;
        }
    }

    public void UnSubscribe(eEventType aType, Func<bool> aFunc)
    {
        switch (aType)
        {
            case eEventType.ButtonPressed:
                onButtonPressed -= aFunc;
                break;
            case eEventType.ButtonUp:
                onButtonUp -= aFunc;
                break;
            default:
                break;
        }
    }

    public void UnSubscribe(eEventType aType, Func<Coord, Coord, bool> aFunc)
    {
        switch (aType)
        {
            case eEventType.PlayerMove:
                onPlayerMoveEvent -= aFunc;
                break;
            case eEventType.PlayerInteract:
                onPlayerInteractEvent -= aFunc;
                break;
            case eEventType.RockInteract:
                onRockInteractEvent -= aFunc;
                break;
            default:
                break;
        }
    }

    public void UnSubscribe(eEventType aType, Func<Coord, bool> aFunc)
    {
        switch (aType)
        {
            case eEventType.RockMove:
                foreach (RockMovement _ in FindObjectsOfType<RockMovement>())
                {
                    onRockMoveEvent -= aFunc;
                }
                break;
            default:
                break;
        }
    }

    public bool PlayerMoveEvent(Coord aPlayerCoord, Coord aPlayerPreviousCoord)
    {
        if (onPlayerMoveEvent != null)
        {
            foreach (Func<Coord, Coord, bool> f in onPlayerMoveEvent.GetInvocationList())
            {
                if (f(aPlayerCoord, aPlayerPreviousCoord)) return true;
            }
        }
        return false;
    }

    public bool PlayerInteractEvent(Coord aPlayerCoord, Coord aPlayerPreviousCoord)
    {
        if (onPlayerInteractEvent != null)
        {
            foreach (Func<Coord, Coord, bool> f in onPlayerInteractEvent.GetInvocationList())
            {
                if (f(aPlayerCoord, aPlayerPreviousCoord)) return true;
            }
        }
        return false;
    }

    public bool RockMoveEvent(Coord aRockCoord)
    {
        if (onRockMoveEvent != null)
        {
            foreach (Func<Coord, bool> f in onRockMoveEvent.GetInvocationList())
            {
                if (f(aRockCoord)) return true;
            }
        }
        return false;
    }

    public bool RockInteractEvent(Coord aRockCoord, Coord aRockPreviousCoord)
    {
        if (onRockInteractEvent != null)
        {
            foreach (Func<Coord, Coord, bool> f in onRockInteractEvent.GetInvocationList())
            {
                if (f(aRockCoord, aRockPreviousCoord)) return true;
            }
        }
        return false;
    }

    public bool ButtonPressedEvent()
    {
        if (onButtonPressed != null)
        {
            foreach (Func<bool> f in onButtonPressed.GetInvocationList())
            {
                if (f()) return true;
            }
        }
        return false;
    }

    public bool ButtonUpEvent()
    {
        if (onButtonUp != null)
        {
            foreach (Func<bool> f in onButtonUp.GetInvocationList())
            {
                if (f()) return true;
            }
        }
        return false;
    }
}
