using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpMoveAction", menuName = "MoveActions/Jump")]
public class JumpMoveAction : MoveAction
{
    Vector2 direction = Vector2.zero;
    public float strength = 800;
    public override void Initialize()
    {
        direction = MoveSelector.GetDirection();
    }
    public override void Start()
    {
        PlayerController.current.SetMomentum(direction * strength);
    }
}
