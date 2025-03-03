using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    private static MoveSelector _singleton;

    public Move Debug_Move;

    private Vector2 direction;
    private int move;

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
        PlayerManager.SetActiveCharacter(player);
        Move selectedMove = MoveManager.GetMove(move);

        PlayerEventBus<MoveConfirmedEvent>.Publish(new MoveConfirmedEvent { move = selectedMove, player = player });
        move = 0;

        // remove later
        player++;
        if (player >= PlayerManager.PlayerCount)
            player = 0;
    }

    public static Vector3 GetDirection()
    {
        return new Vector3(-_singleton.direction.x, _singleton.direction.y, 0);
    }

    public void SelectMove(int index)
    {
        PlayerManager.SetActiveCharacter(player);
        Move selectedMove = MoveManager.GetMove(index);
        Debug_Move = selectedMove;

        PlayerEventBus<MoveSelectionEvent>.Publish(new MoveSelectionEvent { move = selectedMove, player = player });
        move = index;
    }
}
