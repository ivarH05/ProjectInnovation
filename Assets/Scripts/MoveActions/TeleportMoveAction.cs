using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TeleportMoveAction", menuName = "MoveActions/TeleportMoveAction")]
public class TeleportMoveAction : MoveAction
{
    public float distance = 8f;
    Vector2 direction = Vector2.zero;
    public override void Initialize()
    {
        direction = MoveSelector.GetDirection();
    }
    public override void Start()
    {
        PlayerController.current.Vanish();
    }

    public override void End()
    {
        PlayerController.current.Move(direction * distance);
        PlayerController.current.Appear();
    }
}
