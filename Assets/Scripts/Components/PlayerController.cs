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

    private float xScale;

    private Rigidbody rb;
    private BoxCollider moveHitBox;

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

    private void OnEnable()
    {
        PlayerManager.AddPlayer(this);
    }

    private void OnDisable()
    {
        PlayerManager.RemovePlayer(this);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveHitBox = transform.AddComponent<BoxCollider>();
        moveHitBox.isTrigger = true;
        PlayerEventBus<MoveConfirmedEvent>.OnEvent += OnMoveConfirmed;
        xScale = transform.localScale.x;
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

        if (rb.velocity.magnitude > 1)
            SetDirection(rb.velocity.x);
    }

    public void SetDirection(float direction)
    {
        if (direction < 0)
            transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
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
        moveHitBox.size = currentMove.hitbox.absoluteSize;
        Vector3 offset = currentMove.hitbox.relativeOffset;
        moveHitBox.center = new Vector3(-offset.x, offset.y, offset.z);

        if (PlayerManager.IsEveryoneReady())
            PlayerEventBus<StartActionEvent>.Publish(new StartActionEvent());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;
        PlayerController pc = other.GetComponent<PlayerController>();
        currentMove.moveBehaviour.OnPlayerTriggerStart(pc);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;
        PlayerController pc = other.GetComponent<PlayerController>();
        currentMove.moveBehaviour.OnPlayerTriggerStay(pc);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;
        PlayerController pc = other.GetComponent<PlayerController>();
        currentMove.moveBehaviour.OnPlayerTriggerStop(pc);
    }
}