using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController current;
    public int playerIndex;
    public IDamageBehaviour damageBehaviour;

    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public bool isKinematic = false;
    [HideInInspector] public bool useGravity = true;

    Move currentMove;

    public bool IsReady 
    { 
        get
        {
            if (currentMove == null)
                return false;
            if (currentMove.moveBehaviour == null)
                return false;
            return currentMove.moveBehaviour.state != MoveState.NULL; 
        } 
    }

    private void Start()
    {
        PlayerEventBus<MoveConfirmedEvent>.OnEvent += OnMoveConfirmed;
    }

    private void OnDestroy()
    {
        PlayerEventBus<MoveConfirmedEvent>.OnEvent -= OnMoveConfirmed;
    }

    public void PlayerUpdate()
    {
        current = this;
        HandleMove();
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

    private void HandleMove()
    {
        if (!IsReady)
            return;
        currentMove.Update();
    }

    public void Damage(PlayerController other, float damage)
    {
        currentMove.moveBehaviour.OnDamaged(other, damage);
    }

    private void OnMoveConfirmed(MoveConfirmedEvent data)
    {
        if (data.player != playerIndex)
            return;
        currentMove = data.move;
        currentMove.moveBehaviour.Initialize();
        if (PlayerManager.IsEveryoneReady())
            PlayerEventBus<StartActionEvent>.Publish(new StartActionEvent());
    }
}