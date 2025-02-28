
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterContainer", menuName = "Solo Resources/CharacterContainer")]
public class CharacterContainer : ScriptableObject
{
    public List<Character> Characters;
}