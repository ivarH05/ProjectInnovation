using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SampleCharacter", menuName = "Character")]
public class Character : ScriptableObject
{
    public new string name = "SampleName";
    public GameObject model;
    public int baseHealth = 100;

    public Move[] moves;
}
