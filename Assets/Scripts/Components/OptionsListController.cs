using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsListController : MonoBehaviour
{
    public GameObject prefab;
    public MoveSelector selector;

    private List<GameObject> objects = new List<GameObject>();

    public void RebuildOptions(int playerIndex)
    {
        Character character = PlayerManager.GetPlayerCharacter(playerIndex);
        Move[] moves = character.moves;
        Clear();

        for (int i = 0; i < moves.Length; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            objects.Add(obj);
            int value = i;
            obj.GetComponent<Button>().onClick.AddListener(() => { selector.SelectMove(value); });
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = moves[i].name;
        }
    }

    public void Clear()
    {
        for (int i = 0; i < objects.Count; i++)
            Destroy(objects[i]);
        objects.Clear();
    }
}
