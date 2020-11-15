using System;
using UnityEngine;

public enum eEventType
{
    PlayerMove,
    RockMove,
    ButtonPressed
}

public class EventHandler : MonoBehaviour
{
    public static EventHandler current;
    public event Func<Coord, Coord, bool> onPlayerMoveEvent;
    public event Func<Coord, bool> onRockMoveEvent;
    public event Func<bool> onButtonPressed;

    private void Awake()
    {
        current = this;
    }

    public void Subscribe(eEventType aType, Func<Coord, Coord, bool> aFunc)
    {
        switch (aType)
        {
            case eEventType.PlayerMove:
                onPlayerMoveEvent += aFunc;
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
                for (int i = 0; i < FindObjectsOfType<Button>().Length; i++)
                {
                    onButtonPressed += aFunc;
                }
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
}
