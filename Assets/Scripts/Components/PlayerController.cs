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
    private List<BoxCollider> hitboxes = new List<BoxCollider>();

    private Dictionary<BoxCollider, CollisionType> hitboxTypes = new Dictionary<BoxCollider, CollisionType>();

    HitboxDebugger debugger;

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
        debugger = GetComponent<HitboxDebugger>();
        if (debugger == null)
            debugger = transform.AddComponent<HitboxDebugger>();
        rb = GetComponent<Rigidbody>();
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

        hitboxTypes = new Dictionary<BoxCollider, CollisionType>();
        HitboxSet set = data.move.hitboxes;
        int index = 0;
        debugger.hitboxSet = set;
        for (int i = 0; i < set.hitboxes.Length; i++)
            CreateHitboxCollider(set.hitboxes[i], GetHitbox(ref index), 8);
        for (int i = 0; i < set.hurtboxes.Length; i++)
            CreateHitboxCollider(set.hurtboxes[i], GetHitbox(ref index), 9);
        for (int i = 0; i < set.grabboxes.Length; i++)
            CreateHitboxCollider(set.grabboxes[i], GetHitbox(ref index), 10);

        while(index < hitboxes.Count)
            hitboxes.RemoveAt(index);

        if (PlayerManager.IsEveryoneReady())
            PlayerEventBus<StartActionEvent>.Publish(new StartActionEvent());
    }

    BoxCollider GetHitbox(ref int index)
    {
        int i = index;
        index++;
        if (i < hitboxes.Count)
            return hitboxes[i];

        BoxCollider box = transform.AddComponent<BoxCollider>();
        hitboxes.Add(box);
        return box;
    }

    void CreateHitboxCollider(Hitbox hitbox, BoxCollider collider, int layer)
    {
        collider.center = hitbox.relativeOffset;
        collider.size = hitbox.absoluteSize;
        collider.includeLayers = 1 << layer;
        collider.excludeLayers = ~(1 << 9);
        collider.isTrigger = true;

        if (layer == 8)
            hitboxTypes.Add(collider, CollisionType.HURTBOX);
        else if (layer == 9)
            hitboxTypes.Add(collider, CollisionType.HITBOX);
        else if (layer == 10)
            hitboxTypes.Add(collider, CollisionType.GRABBOX);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;
        PlayerController pc = other.GetComponent<PlayerController>();

        BoxCollider[] colliders = GetComponents<BoxCollider>();
        foreach (var col in colliders)
        {
            if (!col.bounds.Intersects(other.bounds))
                continue;
            PlayerCollisionData data = new PlayerCollisionData
            {
                current = this,
                other = pc,
                currentType = hitboxTypes[col],
                otherType = pc.hitboxTypes[col],
            };
            currentMove.moveBehaviour.OnPlayerTriggerStart(data);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;
        PlayerController pc = other.GetComponent<PlayerController>();

        BoxCollider[] colliders = GetComponents<BoxCollider>();
        foreach (var col in colliders)
        {
            if (!col.bounds.Intersects(other.bounds))
                continue;
            PlayerCollisionData data = new PlayerCollisionData
            {
                current = this,
                other = pc,
                currentType = hitboxTypes[col],
                otherType = pc.hitboxTypes[col],
            };
            currentMove.moveBehaviour.OnPlayerTriggerStay(data);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;
        PlayerController pc = other.GetComponent<PlayerController>();

        BoxCollider[] colliders = GetComponents<BoxCollider>();
        foreach (var col in colliders)
        {
            if (!col.bounds.Intersects(other.bounds))
                continue;
            PlayerCollisionData data = new PlayerCollisionData
            {
                current = this,
                other = pc,
                currentType = hitboxTypes[col],
                otherType = pc.hitboxTypes[col],
            };
            currentMove.moveBehaviour.OnPlayerTriggerStop(data);
        }
    }
}