using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdoll : MonoBehaviour
{
    public Transform target;
    public Rigidbody hips;

    public float uprightTorque = 1;
    public float manulaTorque = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BalanceHips();
    }

    void BalanceHips()
    {
        Quaternion rot = Quaternion.FromToRotation(hips.transform.up, Vector3.up);

        hips.AddTorque(new Vector3(rot.x, rot.y, rot.z) * uprightTorque);

        float angle = Vector3.SignedAngle(hips.transform.forward, target.transform.forward, Vector3.up) / 180;

        hips.AddRelativeTorque(0, angle * manulaTorque, 0);

    }
}
