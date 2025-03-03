using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController current;
    public int playerIndex;
    public IDamageBehaviour damageBehaviour;

    [HideInInspector] public bool isKinematic = false;
    [HideInInspector] public bool useGravity = true;
    public Vector3 Velocity { get { return rb.velocity; } set { rb.velocity = value; } }

    private Rigidbody rb;

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
        rb = GetComponent<Rigidbody>();
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

    private Vector3 storedVelocity;
    public void SetPauseState(bool state)
    {
        if (state)
            storedVelocity = rb.velocity;

        rb.isKinematic = state;

        if(!state)
            rb.velocity = storedVelocity;
    }

    public void SetMomentum(Vector3 momentum)
    {
        rb.velocity = momentum / rb.mass;
    }

    public void AddMomentum(Vector3 momentum)
    {
        rb.velocity += momentum / rb.mass;
    }

    public void AddForce(Vector3 force)
    {
        rb.AddForce(force);
    }

    private void HandlePhysics()
    {
        rb.isKinematic = isKinematic;
        rb.useGravity = useGravity;

        transform.rotation = Quaternion.LookRotation(Vector3.back, -Physics.gravity);
        if (isKinematic)
            return;
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