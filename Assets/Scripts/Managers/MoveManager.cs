using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public List<Move> moves;

    private static MoveManager _singleton;
    private void Start()
    {
        _singleton = this;
    }

    public static Move GetMove(int index)
    {
        Move m = _singleton.moves[index].GetClone();
        m.moveBehaviour.state = MoveState.UNINITIALIZED;
        return m;
    }
}
