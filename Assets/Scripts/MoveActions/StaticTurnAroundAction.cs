using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticTurnAroundAction", menuName = "MoveActions/StaticTurnAround")]
public class StaticTurnAroundAction : MoveAction
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
        PlayerController.current.SetDirection(direction.x);
    }
    public override void Update()
    {

    }
    public override void End()
    {
        PlayerController.current.useGravity = true;
    }
}
