using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsListController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerEventBus<StopActionEvent>.OnEvent += OnStopAction;
        PlayerEventBus<MoveSelectionEvent>.OnEvent += OnMoveSelection;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnStopAction(StopActionEvent data)
    {
        RebuildOptions();
    }

    void OnMoveSelection(MoveSelectionEvent data)
    {
        RebuildOptions();
    }

    void RebuildOptions()
    {

    }
}
