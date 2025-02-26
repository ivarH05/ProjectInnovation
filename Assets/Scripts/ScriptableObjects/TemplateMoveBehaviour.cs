using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TemplateMoveBehaviour", menuName = "MoveBehaviour/Template")]
public class TemplateMoveBehaviour : MoveBehaviour
{
    public MoveAction PreperationAction;
    public MoveAction ExecutionAction;
    public MoveAction RecoveryAction;
    public override void OnPreperationStart() { PreperationAction.Start(); }
    public override void OnPreperation() { PreperationAction.Update(); }
    public override void OnPreperationEnd() { PreperationAction.End(); }
    public override void OnExecutionStart() { ExecutionAction.Start(); }
    public override void OnExecution() { ExecutionAction.Update(); }
    public override void OnExecutionEnd() { ExecutionAction.End(); }
    public override void OnRecoveryStart() { RecoveryAction.Start(); }
    public override void OnRecovery() { RecoveryAction.Update(); }
    public override void OnRecoveryEnd() { RecoveryAction.End(); }
}
