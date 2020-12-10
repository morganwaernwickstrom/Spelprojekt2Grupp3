using System;
using UnityEngine;

public enum eEventType
{
    PlayerMove,
    PlayerInteract,
    PlayerDeath,
    RockMove,
    RockInteract,
    ButtonPressed,
    ButtonUp,
    GoalReached
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
    public event Func<Coord, bool> onGoalReachedEvent;
    public event Action onPlayerDeath;

    private void Start()
    {
        current = this;
    }

    private void OnEnable()
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
                for (int i = 0; i < FindObjectsOfType<SlidingRockMovement>().Length; i++)
                {
                    onRockMoveEvent += aFunc;
                }
                break;
            case eEventType.GoalReached:
                onGoalReachedEvent += aFunc;
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

    public void Subscribe(eEventType aType, Action aFunc)
    {
        switch (aType)
        {
            case eEventType.PlayerDeath:
                onPlayerDeath += aFunc;
                break;
        }
    }

    public void UnSubscribe(eEventType aType, Action aFunc)
    {
        switch (aType)
        {
            case eEventType.PlayerDeath:
                onPlayerDeath -= aFunc;
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
                foreach (SlidingRockMovement _ in FindObjectsOfType<SlidingRockMovement>())
                {
                    onRockMoveEvent -= aFunc;
                }
                break;
            case eEventType.GoalReached:
                onGoalReachedEvent -= aFunc;
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

    public bool GoalReachedEvent(Coord aGoalCoord)
    {
        if (onGoalReachedEvent != null)
        {
            foreach (Func<Coord, bool> f in onGoalReachedEvent.GetInvocationList())
            {
                if(f(aGoalCoord)) return true;
            }
        }
        Debug.Log("No Canvas?");
        return false;
    }

    public void PlayerDeathEvent()
    {
        if (onPlayerDeath != null)
        {
            foreach (Action f in onPlayerDeath.GetInvocationList())
            {
                f();
            }
        }
    }
}
