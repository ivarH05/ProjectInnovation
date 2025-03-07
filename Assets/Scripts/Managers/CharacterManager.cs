using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterContainer container;
    private static List<Character> Characters = new List<Character>();

    public static int currentIndex;
    public static Character Current 
    { get
        {
            return Characters[currentIndex];
        } 
    }

    private void Start()
    {
        Characters = container.Characters;
    }

    public static Character GetCharacter(int index)
    {
        if (Characters.Count == 0)
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CharacterManager>().Start();
        return Characters[index];
    }
}
