using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    private Move move;
    private Vector2 direction;
    private float modifier = 1;

    public void OnDirectionJoyStickMoved(Vector2 value)
    {
        direction = value.normalized;
        modifier = value.magnitude;
    }

    public void LockInSelection()
    {
        move = MoveManager.GetMove(0); // remove later
        PlayerEventBus<MoveConfirmedEvent>.Publish(new MoveConfirmedEvent(move));
    }
}
