using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RagdollBalanceAnimated : RagdollController
{
    public Transform target;

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
        float angle = Vector3.Angle(transform.forward, target.transform.forward);

        rb.AddRelativeTorque((transform.forward - target.transform.forward) * manualTorque * angle);

    }
}
