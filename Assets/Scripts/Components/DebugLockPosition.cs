using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLockPosition : MonoBehaviour
{
    Vector3 pos;
    Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pos;
        transform.rotation = rot;
    }
}
