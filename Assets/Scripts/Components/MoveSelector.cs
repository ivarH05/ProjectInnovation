using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    private static MoveSelector _singleton;
    public OptionsListController olc;
    public UIMoveDirectionSelector moveDirectionSelector;

    private Vector2 direction;
    private int move;

    private int player = 0;

    private void Start()
    {
        _singleton = this;
        PlayerEventBus<StopActionEvent>.OnEvent += OnActionEnd;
        UIEventBus<RedrawMoveEvent>.Publish(new RedrawMoveEvent { player = PlayerManager.GetPlayer(player), character = PlayerManager.GetPlayerCharacter(player) });
    }

    private void OnDestroy()
    {
        PlayerEventBus<StopActionEvent>.OnEvent -= OnActionEnd;
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

        UIEventBus<RedrawMoveEvent>.Publish(new RedrawMoveEvent { player = PlayerManager.GetPlayer(player), character = PlayerManager.GetPlayerCharacter(player) });
    }

    public static Vector3 GetDirection()
    {
        return new Vector3(-_singleton.direction.x, _singleton.direction.y, 0);
    }

    public void SelectMove(int index)
    {
        PlayerManager.SetActiveCharacter(player);
        Move selectedMove = MoveManager.GetMove(index);

        PlayerEventBus<MoveSelectionEvent>.Publish(new MoveSelectionEvent { move = selectedMove, player = player });
        move = index;
        float playerDir = -PlayerManager.GetPlayer(player).transform.localScale.x;
        Vector3 direction = new Vector3(selectedMove.baseDirection.x * playerDir, selectedMove.baseDirection.y, 0);
        _singleton.direction = direction;
        moveDirectionSelector.SetDirection(direction);
    }

    public void OnActionEnd(StopActionEvent e)
    {
        player = e.player.playerIndex;
        SelectMove(0);
        UIEventBus<RedrawMoveEvent>.Publish(new RedrawMoveEvent { player = e.player, character = PlayerManager.GetPlayerCharacter(e.player.playerIndex) });
    }
}
