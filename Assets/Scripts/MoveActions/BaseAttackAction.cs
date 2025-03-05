using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseAttackAction", menuName = "MoveActions/BaseAttackAction")]
public class BaseAttackAction : MoveAction
{

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
