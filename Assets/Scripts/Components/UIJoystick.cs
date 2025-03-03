using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class UIJoystick : MonoBehaviour
{
    public UnityEvent<Vector2> OnDropdownValueChanged;
    public UIJoystick_Stick stick;
    public float size = 100;

    public bool clampX;
    public bool clampY;

    public void Change()
    {
        Vector3 pos = stick.transform.localPosition;
        Vector3 newPos = pos;
        if (pos.magnitude > size)
            newPos = newPos.normalized * size;

        if(clampX)
            newPos.x = 0;
        if(clampY)
            newPos.y = 0;

        stick.transform.localPosition = newPos;
        OnDropdownValueChanged?.Invoke(newPos / size);
    }
}
