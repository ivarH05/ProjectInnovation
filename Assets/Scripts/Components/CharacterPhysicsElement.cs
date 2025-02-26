using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Joint))]
public class CharacterPhysicsElement : MonoBehaviour
{
    public Transform linkedObject;

    const float speed = 100;
    const float angularSpeed = 100f;
    const float maxAcceleration = 1000;

    private Rigidbody rb;
    private Joint joint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<Joint>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //UpdateMovement();
        UpdateRotation();
    }


    void UpdateMovement()
    {
        rb.AddForce((transform.localPosition - linkedObject.localPosition).normalized * speed);
    }

    void UpdateRotation()
    {

    }
}
