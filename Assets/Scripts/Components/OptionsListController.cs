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

    private void Start()
    {
        PlayerEventBus<StopActionEvent>.OnEvent += OnStopAction;
        UIEventBus<RedrawMoveEvent>.OnEvent += OnRedrawMove;
    }

    private void OnDestroy()
    {
        PlayerEventBus<StopActionEvent>.OnEvent -= OnStopAction;
        UIEventBus<RedrawMoveEvent>.OnEvent -= OnRedrawMove;
    }

    void OnRedrawMove(RedrawMoveEvent e)
    {
        RebuildOptions(e.character.moves, e.player.IsGrounded());
    }

    private void RebuildOptions(Move[] moves, bool grounded)
    {
        Clear();

        for (int i = 0; i < moves.Length; i++)
        {
            if (grounded && !moves[i].ground)
                continue;
            if (!grounded && !moves[i].aerial)
                continue;
            int moveIndex = i;

            GameObject obj;
            objects.Add(obj = Instantiate(prefab, transform));
            obj.GetComponent<Button>().onClick.AddListener(() => { selector.SelectMove(moveIndex); });

            //temp
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = moves[i].name;
        }
    }
    void OnStopAction(StopActionEvent actionEvent)
    {
        Clear();
    }

    private void Clear()
    {
        for (int i = 0; i < objects.Count; i++)
            Destroy(objects[i]);
        objects.Clear();
    }
}
