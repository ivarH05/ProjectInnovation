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

    public void Change()
    {
        Vector3 pos = stick.transform.localPosition;
        if (pos.magnitude > size)
        {
            stick.transform.localPosition = pos.normalized * size;
        }
        OnDropdownValueChanged?.Invoke(pos / size);
    }
}
