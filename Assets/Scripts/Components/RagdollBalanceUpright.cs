using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RagdollBalanceUpright : RagdollController
{
    public Transform target;
    public Vector3 upAxis = new Vector3(0, 0, 1);

    public float uprightTorque = 1;
    public float manualTorque = 1;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Balance();
    }

    void Balance()
    {
        Quaternion rot = Quaternion.FromToRotation(transform.up, upAxis);

        rb.AddTorque(new Vector3(rot.x, rot.y, rot.z) * uprightTorque);

        float angle = Vector3.SignedAngle(transform.forward, target.transform.forward, Vector3.up) / 180;

        rb.AddRelativeTorque(0, angle * manualTorque, 0);

    }
}
