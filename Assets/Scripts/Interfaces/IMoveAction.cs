using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveAction : ScriptableObject
{
    public virtual void Initialize() { }
    public virtual MoveAction GetClone() { return Instantiate(this); }

    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void End() { }

    public virtual void OnPlayerTriggerStart(PlayerCollisionData data) { }
    public virtual void OnPlayerTriggerStay(PlayerCollisionData data) { }
    public virtual void OnPlayerTriggerStop(PlayerCollisionData data) { }


    public virtual bool OverrideDamage(PlayerController player, PlayerController other, float damage) { return false; }
}
