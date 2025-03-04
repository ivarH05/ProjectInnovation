using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hitbox
{
    public Vector3 relativeOffset = new Vector3(1, 0, 0);
    public Vector3 absoluteSize = Vector3.one;
}
