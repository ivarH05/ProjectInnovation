using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CollisionType { HITBOX, HURTBOX, GRABBOX }
public class PlayerCollisionData
{
    public PlayerController current;
    public PlayerController other;
    public CollisionType currentType;
    public CollisionType otherType;
}
