using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterContainer container;
    public static List<Character> Characters = new List<Character>();

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
        Debug.Log(Characters.Count);
    }
}
