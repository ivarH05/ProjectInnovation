using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveDirectionSelector : MonoBehaviour
{
    public Transform joystick;
    public Transform topHalfJoystick;
    public Transform bottomHalfJoystick;
    public Transform slider;

    private Transform active;

    private void Start()
    {
        active = joystick;
        PlayerEventBus<MoveSelectionEvent>.OnEvent += OnMoveSelected;
    }

    private void OnDestroy()
    {
        PlayerEventBus<MoveSelectionEvent>.OnEvent -= OnMoveSelected;
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
            case ControlType.TOPHALFJOYSTICK:
                ChangeActive(topHalfJoystick); break;
            case ControlType.BOTTOMHALFJOYSTICK:
                ChangeActive(bottomHalfJoystick); break;

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

    public void SetDirection(Vector2 direction)
    {
        if (active == null)
            return;
        active.GetComponent<UIJoystick>().SetDirection(direction);
    }
}
