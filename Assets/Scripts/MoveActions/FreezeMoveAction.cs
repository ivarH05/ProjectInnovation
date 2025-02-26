using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeMoveAction", menuName = "MoveActions/Freeze")]
public class FreezeMoveAction : MoveAction
{
    public override void Start()
    {
        PlayerController.current.isKinematic = true;
    }
    public override void End()
    {
        PlayerController.current.isKinematic = false;
    }
}
