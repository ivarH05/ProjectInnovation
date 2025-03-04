using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticAttackAction", menuName = "MoveActions/StaticAttack")]
public class StaticAttackAction : MoveAction
{
    public override void Start()
    {
        PlayerController.current.SetMomentum(Vector3.zero);
        PlayerController.current.useGravity = false;
    }
    public override void End()
    {
        PlayerController.current.useGravity = true;
    }

    public override void OnPlayerTriggerStart(PlayerCollisionData data)
    {
        Destroy(data.other.gameObject);
    }
}
