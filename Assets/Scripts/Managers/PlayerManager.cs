using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerController[] playerCharacters;
    private static PlayerController[] players;
    private static PlayerManager _singleton;


    private void Start()
    {
        _singleton = this;
        players = playerCharacters;
    }
    public static int PlayerCount { get { return players.Length; } }

    public static Vector3 GetPosition(int playerIndex) { return players[playerIndex].transform.position; }
    public static Vector3 GetVelocity(int playerIndex) { return players[playerIndex].velocity; }

    public static bool IsEveryoneReady()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].IsReady)
                return false;
        }

        return true;
    }
}
