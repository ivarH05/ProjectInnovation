using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEventBus<T> where T : TimeEvent
{
    public static Action<T> OnEvent;
    public static void Publish(T pEvent)
    {
        OnEvent?.Invoke(pEvent);
    }
}

public abstract class TimeEvent { }

public class PauseGameEvent : TimeEvent { }
public class ResumeGameEvent : TimeEvent { }
public class PauseViewportEvent : TimeEvent { }
public class ResumeViewportEvent : TimeEvent { }
public class PauseReplayEvent : TimeEvent { }
public class ResumeReplayEvent : TimeEvent { }