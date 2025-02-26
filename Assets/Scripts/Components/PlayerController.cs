using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController current;
    public IDamageBehaviour damageBehaviour;

    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public bool isKinematic = false;
    [HideInInspector] public bool useGravity = true;

    Move currentMove;

    public bool IsReady { get { return currentMove != null; } }

    private void Start()
    {
        PlayerEventBus<MoveConfirmedEvent>.OnEvent += OnMoveConfirmed;
    }

    private void OnDestroy()
    {
        PlayerEventBus<MoveConfirmedEvent>.OnEvent -= OnMoveConfirmed;
    }

    private void FixedUpdate()
    {
        if (TimeManager.isPaused)
            return;

        HandleAttack();
        HandlePhysics();
    }

    private void HandlePhysics()
    {
        if (isKinematic)
            return;
        if(useGravity)
            velocity += Physics.gravity * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }

    private void HandleAttack()
    {
        if (currentMove == null)
        current = this;
        currentMove.Update();
    }

    public void Damage(PlayerController other, float damage)
    {
        currentMove?.moveBehaviour.OnDamaged(other, damage);
    }

    private void OnMoveConfirmed(MoveConfirmedEvent data)
    {
        currentMove = data.move;
        if (PlayerManager.IsEveryoneReady())
            PlayerEventBus<StartActionEvent>.Publish(new StartActionEvent());
    }
}