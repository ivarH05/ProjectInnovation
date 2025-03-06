using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static readonly List<PlayerController> players = new List<PlayerController>();
    private static readonly List<int> characterIndexes = new List<int>();
    private PlayerManager _singleton;
    bool pausedLastFrame = false;

    private void Start()
    {
        _singleton = this;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].playerIndex = i;
            characterIndexes.Add(i);
        }
    }

    private void FixedUpdate()
    {
        bool isPaused = TimeManager.isPaused;
        if(isPaused !=  pausedLastFrame )
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].SetPauseState(isPaused);
            }
            pausedLastFrame = isPaused;
        }

        if (isPaused)
            return;

        for (int i = 0; i < players.Count; i++)
        {
            players[i].PlayerUpdate();
        }
    }

    public static void AddPlayer(PlayerController pc)
    {
        pc.playerIndex = players.Count;
        players.Add(pc);
        characterIndexes.Add(players.Count);
    }
    public static void RemovePlayer(PlayerController pc)
    {
        players.RemoveAt(pc.playerIndex);
        characterIndexes.RemoveAt(pc.playerIndex);

        for (int i = pc.playerIndex; i < players.Count; i++)
        {
            players[i].playerIndex--;
        }
    }

    public static int PlayerCount { get { return players.Count; } }

    public static PlayerController GetPlayer(int index) { return players[index]; }
    public static Character GetPlayerCharacter(int index) { return CharacterManager.GetCharacter(characterIndexes[index]); }
    public static Vector3 GetPosition(int playerIndex) { return players[playerIndex].transform.position; }
    public static Vector3 GetVelocity(int playerIndex) { return players[playerIndex].Velocity; }

    public static bool IsEveryoneReady()
    {
        for (int i = 0; i < players.Count; i++)
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
