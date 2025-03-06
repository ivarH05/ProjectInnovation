using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventBus<T> where T : UIEvent
{
    public static Action<T> OnEvent;
    public static void Publish(T pEvent)
    {
        OnEvent?.Invoke(pEvent);
    }
}

public abstract class UIEvent { }

public class RedrawMoveEvent : UIEvent
{
    public Character character;
    public PlayerController player;
}