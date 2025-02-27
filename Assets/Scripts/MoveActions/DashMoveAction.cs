using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashMoveAction", menuName = "MoveActions/Dash")]
public class DashMoveAction : MoveAction
{
    Vector2 direction = Vector2.zero;
    public float strength = 25;
    public override void Initialize()
    {
        Debug.Log("Initialized");
        direction = MoveSelector.GetDirection();
    }
    public override void Start()
    {
        Debug.Log("Started");
        PlayerController.current.useGravity = false;
    }
    public override void Update()
    {
        Debug.Log("Updated");
        PlayerController.current.velocity = direction * strength;
    }
    public override void End()
    {
        Debug.Log("Ended");
        PlayerController.current.useGravity = true;
    }
}
