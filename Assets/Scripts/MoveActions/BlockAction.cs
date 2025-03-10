using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockAction", menuName = "MoveActions/Block")]
public class BlockAction : MoveAction
{
    public bool useGravity = true;
    public bool isKinematic = false;

    public override void Start()
    {
        PlayerController.current.useGravity = useGravity;
        PlayerController.current.isKinematic = isKinematic;
    }
    public override void End()
    {
        PlayerController.current.useGravity = true;
        PlayerController.current.isKinematic = false;
    }

    public override bool OverrideDamage(PlayerController player, PlayerController other, float damage)
    {
        if ((player.transform.localScale.x > 0) == (other.transform.localScale.x > 0))
            return false;
        if (other.currentMove.moveHeight != MoveHeight.MEDIUM)
            return false;
        if (other.currentMove.moveHeight != player.currentMove.moveHeight)
            return false;

        Debug.Log("Blocked move");
        return true;
    }
}
