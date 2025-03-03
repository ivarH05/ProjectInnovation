using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveDirectionSelector : MonoBehaviour
{
    public Transform joystick;
    public Transform slider;

    private Transform active;

    private void Start()
    {
        active = joystick;
        PlayerEventBus<MoveSelectionEvent>.OnEvent += OnMoveSelected;
    }

    void OnMoveSelected(MoveSelectionEvent data)
    {
        switch (data.move.controlType)
        {
            case ControlType.BIDIRECTIONAL:
                ChangeActive(slider); break;
            case ControlType.MONODIRECTIONAL:
                ChangeActive(null); break;
            case ControlType.JOYSTICK:
                ChangeActive(joystick); break;
        }
    }

    void ChangeActive(Transform newActive)
    {
        if(active != null)
            active.gameObject.SetActive(false);
        if(newActive != null)
            newActive.gameObject.SetActive(true);
        active = newActive;
    }
}
