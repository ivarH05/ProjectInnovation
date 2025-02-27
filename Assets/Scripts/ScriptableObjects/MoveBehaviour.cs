using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveBehaviour : ScriptableObject
{
    [HideInInspector] public PlayerController player;
    [HideInInspector] public MoveState state = MoveState.UNINITIALIZED;

    public virtual void Initialize() { }
    public virtual void Update() { }
    public virtual MoveBehaviour GetClone() { return Instantiate(this); }

    public virtual void OnPreperationStart() { }
    public virtual void OnPreperation() { }
    public virtual void OnPreperationEnd() { }
    public virtual void OnExecutionStart() { }
    public virtual void OnExecution() { }
    public virtual void OnExecutionEnd() { }
    public virtual void OnRecoveryStart() { }
    public virtual void OnRecovery() { }
    public virtual void OnRecoveryEnd() { }


    public void OnHitPlayer(PlayerController other) { }

    public void OnDamaged(PlayerController other, float damage)
    {
        player.damageBehaviour.OnDamage(damage);
    }
}

public enum MoveState { NULL, UNINITIALIZED, PREPERATION, EXECUTION, RECOVERY, BLOCKED, CANCELED };