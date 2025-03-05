using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ControlType { BIDIRECTIONAL, MONODIRECTIONAL, JOYSTICK, BOTTOMHALFJOYSTICK, TOPHALFJOYSTICK }
public enum MoveType { MOVEMENT, ATTACK, BLOCK, HYBRID }
public enum MoveHeight { NONE, LOW, MEDIUM, HIGH }

[System.Serializable]
public class Move
{
    //general
    [Tooltip("The display name of the move")] public string name;
    [Tooltip("The Icon you see when selecting the move")] public Sprite sprite;
    [Tooltip("The type of attack")] public MoveType moveType;
    [Tooltip("The default value for a move, (1, 0, 0) is forwards")] public Vector3 baseDirection = new Vector3(1, 0, 0);

    [Tooltip("The type of controll")] public ControlType controlType;
    [Tooltip("True if the action can be used mid-air")] public bool aerial = false;
    [Tooltip("True if the action can be used when grounded")] public bool ground = true;
    [Tooltip("True if the action can be used when KnockedDown")] public bool knockedMove = false;

    [Tooltip("The amount of frames before the main execution")] public int preperationFrames = 5;
    [Tooltip("The amount of frames the execution takes")] public int executionFrames = 5;
    [Tooltip("The amount of frames after the main execution")] public int recoveryFrames = 5;
    //movement options

    //attack options
    [Tooltip("The damage of the attack under normal circumstances")] public int baseDamage;
    [Tooltip("The amount of additional frames on top of recovery frames, after getting blocked")] public int onGetBlockedModifier = 5;
    [Tooltip("The amount of additional frames on top of recovery frames, after performing an attack")] public int onDealDamageStun = 5;
    [Tooltip("The hitbox of the attack, this distance will call the collision methods")] public HitboxSet hitboxes;

    //block options
    [Tooltip("True if this move ignores the MoveHeight and also blocks from behind")] public bool fullBody;


    //attack and block
    [Tooltip("The height of the attack, NONE if not applicable")] public MoveHeight moveHeight;

    //attack and movement options

    //also general, place at the bottom though
    [Tooltip("The main behaviour of the move")] public MoveBehaviour moveBehaviour;

    private int frameCount;

    public Move GetClone()
    {
        Move result = this.CloneViaSerialization<Move>();
        result.moveBehaviour = moveBehaviour.GetClone();
        frameCount = 0;
        return result;
    }

    public void Update()
    {
        frameCount++;
        MoveState state = moveBehaviour.state;
        if(state == MoveState.UNINITIALIZED)
        {
            moveBehaviour.state = MoveState.PREPERATION;
            moveBehaviour.OnPreperationStart();
        }
        else if (state == MoveState.PREPERATION)
        {
            if (frameCount > preperationFrames)
            {
                moveBehaviour.state = MoveState.EXECUTION;
                moveBehaviour.OnPreperationEnd();
                moveBehaviour.OnExecutionStart();
                moveBehaviour.OnExecution();
            }
            else
                moveBehaviour.OnPreperation();
        }
        else if (state == MoveState.EXECUTION)
        {
            if (frameCount > executionFrames + preperationFrames)
            {
                moveBehaviour.state = MoveState.RECOVERY;
                moveBehaviour.OnExecutionEnd();
                moveBehaviour.OnRecoveryStart();
                moveBehaviour.OnRecovery();
            }
            else
                moveBehaviour.OnExecution();
        }
        else if (state == MoveState.RECOVERY)
        {
            if (frameCount > recoveryFrames + executionFrames + preperationFrames)
            {
                moveBehaviour.OnRecoveryEnd();
                moveBehaviour.state = MoveState.NULL;
                PlayerEventBus<StopActionEvent>.Publish(new StopActionEvent() { player = PlayerController.current, move = this });
            }
            else
                moveBehaviour.OnRecovery();
        }
    }
}
