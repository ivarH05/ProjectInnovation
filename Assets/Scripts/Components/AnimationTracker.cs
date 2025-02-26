using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTracker : MonoBehaviour
{
    public Transform target;

    void FixedUpdate()
    {
        transform.localPosition = target.localPosition;
        transform.localEulerAngles = target.localEulerAngles;
        target.localScale = target.localScale;
    }
}
