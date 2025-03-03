using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashMoveAction", menuName = "MoveActions/Dash")]
public class DashMoveAction : MoveAction
{
    Vector2 direction = Vector2.zero;
    public float strength = 800;
    public override void Initialize()
    {
        direction = MoveSelector.GetDirection();
    }
    public override void Start()
    {
        PlayerController.current.useGravity = false;
    }
    public override void Update()
    {
        PlayerController.current.SetMomentum(direction * strength);
    }
    public override void End()
    {
        PlayerController.current.useGravity = true;
    }
}
