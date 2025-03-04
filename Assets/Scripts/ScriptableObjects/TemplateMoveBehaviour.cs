using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TemplateMoveBehaviour", menuName = "Move")]
public class TemplateMoveBehaviour : MoveBehaviour
{
    public MoveAction PreperationAction;
    public MoveAction ExecutionAction;
    public MoveAction RecoveryAction;

    public override void Initialize()
    {
        PreperationAction.Initialize();
        ExecutionAction.Initialize();
        RecoveryAction.Initialize();
    }

    public override MoveBehaviour GetClone()
    {
        TemplateMoveBehaviour tmp = Instantiate(this);
        tmp.PreperationAction = PreperationAction.GetClone();
        tmp.ExecutionAction = ExecutionAction.GetClone();
        tmp.RecoveryAction = RecoveryAction.GetClone();

        return tmp;
    }

    public override void OnPreperationStart() { Debug.Log("prepare"); PreperationAction.Start(); }
    public override void OnPreperation() { Debug.Log("preparing"); PreperationAction.Update(); }
    public override void OnPreperationEnd() { Debug.Log("prepared"); PreperationAction.End(); }
    public override void OnExecutionStart() { Debug.Log("execute"); ExecutionAction.Start(); }
    public override void OnExecution() { Debug.Log("executing"); ExecutionAction.Update(); }
    public override void OnExecutionEnd() { Debug.Log("executing"); ExecutionAction.End(); }
    public override void OnRecoveryStart() { Debug.Log("recover"); RecoveryAction.Start(); }
    public override void OnRecovery() { Debug.Log("recovering"); RecoveryAction.Update(); }
    public override void OnRecoveryEnd() { Debug.Log("recovered"); RecoveryAction.End(); }

    public override void OnPlayerTriggerStart(PlayerController other)
    {
        PreperationAction.OnPlayerTriggerStart(other);
        ExecutionAction.OnPlayerTriggerStart(other);
        RecoveryAction.OnPlayerTriggerStart(other);
    }
    public override void OnPlayerTriggerStay(PlayerController other)
    {
        PreperationAction.OnPlayerTriggerStay(other);
        ExecutionAction.OnPlayerTriggerStay(other);
        RecoveryAction.OnPlayerTriggerStay(other);
    }
    public override void OnPlayerTriggerStop(PlayerController other)
    {
        PreperationAction.OnPlayerTriggerStop(other);
        ExecutionAction.OnPlayerTriggerStop(other);
        RecoveryAction.OnPlayerTriggerStop(other);
    }
}
