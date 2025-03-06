using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HitboxDebugger : MonoBehaviour
{
    public HitboxSet hitboxSet = new HitboxSet();
    private void OnDrawGizmosSelected()
    {
        if (hitboxSet == null)
            hitboxSet = new HitboxSet();
        //red is hitbox
        //blue is hurtbox
        //yellow is grabbox
        if (hitboxSet.hitboxes?.Length > 0)
        {
            for (int i = 0; i < hitboxSet.hitboxes.Length; i++)
                DrawHitbox(hitboxSet.hitboxes[i], 1f, 0.25f, 0.15f);
        }
        if (hitboxSet.hurtboxes?.Length > 0)
        {
            for (int i = 0; i < hitboxSet.hurtboxes.Length; i++)
                DrawHitbox(hitboxSet.hurtboxes[i], 0.15f, 0.25f, 0.7f);
        }
        if (hitboxSet.grabboxes?.Length > 0)
        {
            for (int i = 0; i < hitboxSet.grabboxes.Length; i++)
                DrawHitbox(hitboxSet.grabboxes[i], 0.7f, 0.7f, 0.35f);
        }
    }
    private void OnDrawGizmos()
    {
        if (hitboxSet == null)
            hitboxSet = new HitboxSet();
        //red is hitbox
        //blue is hurtbox
        //yellow is grabbox
        if (hitboxSet.hitboxes?.Length > 0)
        {
            for (int i = 0; i < hitboxSet.hitboxes.Length; i++)
                DrawHitbox(hitboxSet.hitboxes[i], 1f, 0.25f, 0.15f, false);
        }
        if (hitboxSet.hurtboxes?.Length > 0)
        {
            for (int i = 0; i < hitboxSet.hurtboxes.Length; i++)
                DrawHitbox(hitboxSet.hurtboxes[i], 0.15f, 0.25f, 0.7f, false);
        }
        if (hitboxSet.grabboxes?.Length > 0)
        {
            for (int i = 0; i < hitboxSet.grabboxes.Length; i++)
                DrawHitbox(hitboxSet.grabboxes[i], 0.7f, 0.7f, 0.35f, false);
        }
    }

    private void DrawHitbox(Hitbox hitbox, float r, float g, float b, bool outline = true)
    {
        Vector3 position = transform.TransformPoint(hitbox.relativeOffset);
        Gizmos.color = new Color(r, g, b, 0.5f);
        Gizmos.DrawCube(position, hitbox.absoluteSize);
        if (outline)
        {
            Gizmos.color = new Color(r, g, b);
            Gizmos.DrawWireCube(position, hitbox.absoluteSize);
        }
    }
}