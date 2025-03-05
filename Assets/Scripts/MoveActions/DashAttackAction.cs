using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashAttackAction", menuName = "MoveActions/DashAttackAction")]
public class DashAttackAction : MoveAction
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
