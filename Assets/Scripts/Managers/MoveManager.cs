using System.Collections;
using System.Collections.Generic;
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
        return _singleton.moves[index];
    }
}
