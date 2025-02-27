using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    private static MoveSelector _singleton;

    private Move move;
    private Vector2 direction;

    private int player = 0;

    private void Start()
    {
        _singleton = this;
    }

    public void OnDirectionJoyStickMoved(Vector2 value)
    {
        direction = value;
    }

    public void LockInSelection()
    {
        move = MoveManager.GetMove(0); // change later

        PlayerEventBus<MoveConfirmedEvent>.Publish(new MoveConfirmedEvent { move = move, player = player });

        // remove later
        player++;
        if (player >= PlayerManager.PlayerCount)
            player = 0;
    }

    public static Vector3 GetDirection()
    {
        return new Vector3(-_singleton.direction.x, _singleton.direction.y, 0);
    }
}
