using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIJoystick_Stick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool dragging = false;
    public UIJoystick joyStick;
    Vector3 offset = Vector3.zero;
    void Update()
    {
        if (!dragging)
            return;
        transform.position = Input.mousePosition + offset;

        joyStick.Change();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = transform.position - Input.mousePosition;
        dragging = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }
}
