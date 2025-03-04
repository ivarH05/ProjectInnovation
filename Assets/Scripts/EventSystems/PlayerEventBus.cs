using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventBus<T> where T : PlayerEvent
{
    public static Action<T> OnEvent;
    public static void Publish(T pEvent)
    {
        OnEvent?.Invoke(pEvent);
    }
}

public abstract class PlayerEvent { }

public class MoveConfirmedEvent : PlayerEvent
{
    public Move move;
    public int player;
}
public class MoveSelectionEvent : PlayerEvent
{
    public Move move;
    public int player;
}
public class StartActionEvent : PlayerEvent { public Move move; }
public class StopActionEvent : PlayerEvent { public Move move; public PlayerController player; }