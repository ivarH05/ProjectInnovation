using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static bool isPaused { get; private set; } = true;

    private bool PauseNextFrame = true;

    private void Start()
    {
        PlayerEventBus<StopActionEvent>.OnEvent += OnStopAction;
        PlayerEventBus<StartActionEvent>.OnEvent += OnStartAction;
    }

    private void FixedUpdate()
    {
        if(PauseNextFrame)
            isPaused = true;
        else
            isPaused = false;
    }

    void OnStopAction(StopActionEvent actionEvent)
    {
        PauseNextFrame = true;
    }

    void OnStartAction(StartActionEvent actionEvent)
    {
        PauseNextFrame = false;
    }
}
