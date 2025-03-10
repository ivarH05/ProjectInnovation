using JetBrains.Annotations;
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

    public virtual void OnPlayerTriggerStart(PlayerCollisionData data) { }
    public virtual void OnPlayerTriggerStay(PlayerCollisionData data) { }
    public virtual void OnPlayerTriggerStop(PlayerCollisionData data) { }

    public void OnHitPlayer(PlayerController other) { }

    public virtual void OnDamaged(PlayerController other, float damage)
    {
        float direction = (player.transform.position.x - other.transform.position.x) > 0 ? 1 : -1;
        Vector3 totalDir = new Vector3(direction, 1, 0).normalized;
        float x = Mathf.Clamp(damage, 0, 4 * Mathf.PI);
        float multipiler = Mathf.Cos((x / 4.0f) - Mathf.PI) + 1;

        player.SetMomentum(totalDir * multipiler * 120);
        player.DealDamage(other, damage);
    }
}

public enum MoveState { NULL, UNINITIALIZED, PREPERATION, EXECUTION, RECOVERY, BLOCKED, CANCELED };