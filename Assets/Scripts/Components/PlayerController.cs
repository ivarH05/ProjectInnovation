using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController current;
    public int playerIndex;
    [HideInInspector] public GameObject mesh;
    private Animator anim;

    [HideInInspector] public bool isKinematic = false;
    [HideInInspector] public bool useGravity = true;
    [HideInInspector] public float BaseHealth { get; private set; } = 100;
    [HideInInspector] public float Health { get; private set; } = 100;
    [HideInInspector] public float Burst { get; private set; } = 100;
    public float BaseDamage { get { return currentMove.baseDamage; } }
    public Vector3 Velocity { get { return rb.velocity; } set { rb.velocity = value; } }

    private Rigidbody rb;
    private readonly List<BoxCollider> hitboxes = new List<BoxCollider>();

    private Dictionary<BoxCollider, CollisionType> hitboxTypes = new Dictionary<BoxCollider, CollisionType>();

    HitboxDebugger debugger;

    public Move currentMove { get; private set; }

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
        anim = mesh.GetComponent<Animator>();
    }

    public void Vanish()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        mesh.SetActive(false);
    }

    public void Appear()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
        mesh.SetActive(true);
    }

    private void Start()
    {
        debugger = GetComponent<HitboxDebugger>();
        if (debugger == null)
            debugger = transform.AddComponent<HitboxDebugger>();
        rb = GetComponent<Rigidbody>();
        PlayerEventBus<MoveConfirmedEvent>.OnEvent += OnMoveConfirmed;
    }

    private void OnDestroy()
    {
        PlayerEventBus<MoveConfirmedEvent>.OnEvent -= OnMoveConfirmed;
        PlayerManager.RemovePlayer(this);
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
            OnPause();

        rb.isKinematic = state;

        if (!state)
            OnPlay();
    }

    public void SetMomentum(Vector3 momentum)
    {
        rb.velocity = momentum / rb.mass;
    }

    public void AddMomentum(Vector3 momentum)
    {
        rb.velocity += momentum / rb.mass;
    }

    public void Move(Vector3 offset)
    {
        rb.MovePosition(transform.position + offset);
    }

    public void SetPosition(Vector3 newPos)
    {
        rb.MovePosition(newPos);
    }

    public void AddForce(Vector3 force)
    {
        rb.AddForce(force);
    }

    private void HandlePhysics()
    {
        rb.isKinematic = isKinematic;
        rb.useGravity = useGravity;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, -Physics.gravity);
        if (isKinematic)
            return;
    }

    private void HandleMove()
    {
        if (!IsReady)
            return;
        currentMove.Update();

        if (Mathf.Abs(rb.velocity.x) > 1.5f)
            SetDirection(rb.velocity.x);
    }

    public void SetDirection(float direction)
    {
        if (direction > 0)
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
    }

    public void OnHit(PlayerController other, float damage)
    {
        currentMove.moveBehaviour.OnDamaged(other, damage);
    }
    public void OnPause()
    {
        anim.speed = 0;
        storedVelocity = rb.velocity;
    }
    public void OnPlay()
    {
        anim.speed = 1;
        rb.velocity = storedVelocity;
    }

    public void DealDamage(PlayerController other, float damage)
    {
        Health -= damage;
        print("Player " + playerIndex + " took damage: " + damage);
        print("New health: " + Health);
        PlayerEventBus<PlayerDamagedEvent>.Publish(new PlayerDamagedEvent() { player = this });

        currentMove = new Move()
        {
            hitboxes = new HitboxSet() { hurtboxes = new Hitbox[] { new Hitbox() { absoluteSize = new Vector3(1, 2, 1), relativeOffset = new Vector3(0, 1, 0) } } },
            moveBehaviour = new TemplateMoveBehaviour() { PreperationAction = new DoNothingMoveAction(), ExecutionAction = new DoNothingMoveAction(), RecoveryAction = new DoNothingMoveAction() },
            preperationFrames = 1,
            executionFrames = other.currentMove.onDealDamageStun - 2,
            recoveryFrames = 1,
        };
    }

    private void OnMoveConfirmed(MoveConfirmedEvent data)
    {
        if (data.player != playerIndex)
            return;
        currentMove = data.move;
        currentMove.moveBehaviour.Initialize();
        currentMove.moveBehaviour.player = this;

        float direction = MoveSelector.GetDirection().x;
        if (direction != 0)
            SetDirection(direction);

        anim.SetInteger("ID", currentMove.animationID);
        anim.SetTrigger("Change");

        hitboxTypes = new Dictionary<BoxCollider, CollisionType>();
        HitboxSet set = data.move.hitboxes;
        int index = 0;
        debugger.hitboxSet = set;
        for (int i = 0; i < set.hitboxes.Length; i++)
            CreateHitboxCollider(set.hitboxes[i], GetHitbox(ref index, CollisionType.HITBOX), 8);
        for (int i = 0; i < set.hurtboxes.Length; i++)
            CreateHitboxCollider(set.hurtboxes[i], GetHitbox(ref index, CollisionType.HURTBOX), 9);
        for (int i = 0; i < set.grabboxes.Length; i++)
            CreateHitboxCollider(set.grabboxes[i], GetHitbox(ref index, CollisionType.GRABBOX), 10);

        while(index < hitboxes.Count)
        {
            Destroy(hitboxes[index]);
            hitboxes.RemoveAt(index);
        }

        Burst += 5;
        if(Burst > 100)
            Burst = 100;

        if (PlayerManager.IsEveryoneReady())
            PlayerEventBus<StartActionEvent>.Publish(new StartActionEvent());
    }

    BoxCollider GetHitbox(ref int index, CollisionType type)
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

        collider.isTrigger = layer != 9;

        if (layer == 8)
            hitboxTypes.Add(collider, CollisionType.HITBOX);
        else if (layer == 9)
            hitboxTypes.Add(collider, CollisionType.HURTBOX);
        else if (layer == 10)
            hitboxTypes.Add(collider, CollisionType.GRABBOX);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(TimeManager.isPaused) return;

        Debug.Log("trigger enter: " + other.transform.name);
        if (!other.transform.CompareTag("Player"))
            return;
        PlayerController pc = other.GetComponent<PlayerController>();

        BoxCollider[] colliders = GetComponents<BoxCollider>();
        foreach (var col in colliders)
        {
            if (!col.bounds.Intersects(other.bounds))
                continue;

            CollisionType t1 = hitboxTypes[col];
            CollisionType t2 = pc.hitboxTypes[(BoxCollider)other];
            print("Collision between " + t1 + " and " + t2);
            PlayerCollisionData data = new PlayerCollisionData
            {
                current = this,
                other = pc,
                currentType = t1,
                otherType = t2,
            };
            currentMove.moveBehaviour.OnPlayerTriggerStart(data);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (TimeManager.isPaused) return;

        Debug.Log("trigger stay: " + other.transform.name);
        if (!other.transform.CompareTag("Player"))
            return;
        PlayerController pc = other.GetComponent<PlayerController>();

        BoxCollider[] colliders = GetComponents<BoxCollider>();
        foreach (var col in colliders)
        {
            if (!col.bounds.Intersects(other.bounds))
                continue;
            CollisionType t1 = hitboxTypes[col];
            CollisionType t2 = pc.hitboxTypes[(BoxCollider)other];
            print("Collision between " + t1 + " and " + t2);
            PlayerCollisionData data = new PlayerCollisionData
            {
                current = this,
                other = pc,
                currentType = t1,
                otherType = t2,
            };
            currentMove.moveBehaviour.OnPlayerTriggerStay(data);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (TimeManager.isPaused) return;

        Debug.Log("trigger exit: " + other.transform.name);
        if (!other.transform.CompareTag("Player"))
            return;
        PlayerController pc = other.GetComponent<PlayerController>();

        BoxCollider[] colliders = GetComponents<BoxCollider>();
        foreach (var col in colliders)
        {
            if (!col.bounds.Intersects(other.bounds))
                continue;
            CollisionType t1 = hitboxTypes[col];
            CollisionType t2 = pc.hitboxTypes[(BoxCollider)other];
            print("Collision between " + t1 + " and " + t2);
            PlayerCollisionData data = new PlayerCollisionData
            {
                current = this,
                other = pc,
                currentType = t1,
                otherType = t2,
            };
            currentMove.moveBehaviour.OnPlayerTriggerStop(data);
        }
    }
    
    public bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position - new Vector3(0, 0.025f, 0), new Vector3(0.5f, 0.02f, 0.5f));
        foreach (var col in colliders)
        {
            if (!col.transform.CompareTag("Player"))
                return true;
        }
        return false;
    }
}