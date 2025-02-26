using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashMoveAction", menuName = "MoveActions/Dash")]
public class DashMoveAction : MoveAction
{
    public override void Start()
    {
        PlayerController.current.useGravity = false;
    }
    public override void Update()
    {
        PlayerController.current.velocity += new Vector3(Time.fixedDeltaTime, 0, 0);
    }
    public override void End()
    {
        PlayerController.current.useGravity = true;
    }
}
