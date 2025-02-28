using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerController[] playerCharacters;
    private static PlayerController[] players;
    private static List<int> characterIndexes = new List<int>();
    private static PlayerManager _singleton;


    private void Start()
    {
        _singleton = this;
        players = playerCharacters;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].playerIndex = i;
            characterIndexes.Add(i);
        }
    }

    private void FixedUpdate()
    {
        if (TimeManager.isPaused)
            return;

        for (int i = 0; i < players.Length; i++)
        {
            players[i].PlayerUpdate();
        }
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

    public static void SetActiveCharacter(int index)
    {
        CharacterManager.currentIndex = characterIndexes[index];
    }
}
