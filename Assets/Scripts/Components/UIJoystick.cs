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

    public bool minX;
    public bool maxX;
    public bool minY;
    public bool maxY;

    public void Change()
    {
        Vector3 pos = stick.transform.localPosition;
        Vector3 newPos = pos;
        if (pos.magnitude > size)
            newPos = newPos.normalized * size;

        if (minX && newPos.x < 0)
            newPos.x = 0;
        if (maxX && newPos.x > 0)
            newPos.x = 0;
        if (minY && newPos.y < 0)
            newPos.y = 0;
        if (maxY && newPos.y > 0)
            newPos.y = 0;

        stick.transform.localPosition = newPos;
        OnDropdownValueChanged?.Invoke(newPos / size);
    }
}
