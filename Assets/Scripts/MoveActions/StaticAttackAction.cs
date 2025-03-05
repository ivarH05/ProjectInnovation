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
        PlayerController.current.isKinematic = false;
    }
    public override void End()
    {
        PlayerController.current.isKinematic = true;
    }

    bool hit = false;
    public override void OnPlayerTriggerStay(PlayerCollisionData data)
    {
        if (hit)
            return;
        if (data.currentType != CollisionType.HITBOX || data.otherType != CollisionType.HURTBOX)
            return;
        data.other.OnHit(data.current, data.current.BaseDamage);
        hit = true;
    }
}
