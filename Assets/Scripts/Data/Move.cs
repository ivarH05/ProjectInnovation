using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move
{
    public float preperationTimeSeconds;
    public float executionTimeSeconds;
    public float recoveryTimeSeconds;

    private float time;

    public MoveBehaviour moveBehaviour;

    public void Update()
    {
        time += Time.fixedDeltaTime;
        MoveState state = moveBehaviour.state;
        if(state == MoveState.UNINITIALIZED)
        {
            moveBehaviour.state = MoveState.PREPERATION;
            moveBehaviour.OnPreperationStart();
        }
        else if (state == MoveState.PREPERATION)
        {
            if (time > preperationTimeSeconds)
            {
                moveBehaviour.state = MoveState.EXECUTION;
                moveBehaviour.OnPreperationEnd();
                moveBehaviour.OnExecutionStart();
            }
            else
                moveBehaviour.OnPreperation();
        }
        else if (state == MoveState.EXECUTION)
        {
            if (time > executionTimeSeconds + preperationTimeSeconds)
            {
                moveBehaviour.state = MoveState.RECOVERY;
                moveBehaviour.OnExecutionEnd();
                moveBehaviour.OnRecoveryStart();
            }
            else
                moveBehaviour.OnExecution();
        }
        else if (state == MoveState.RECOVERY)
        {
            if (time > recoveryTimeSeconds + executionTimeSeconds + preperationTimeSeconds)
                moveBehaviour.OnRecoveryEnd();
            else
                moveBehaviour.OnRecovery();
        }
    }
}
