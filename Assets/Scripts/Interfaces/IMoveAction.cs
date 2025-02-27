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
}
