using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsListController : MonoBehaviour
{
    public GameObject prefab;

    private List<GameObject> objects;
    // Start is called before the first frame update
    void Start()
    {
        PlayerEventBus<StopActionEvent>.OnEvent += OnStopAction;
        PlayerEventBus<MoveSelectionEvent>.OnEvent += OnMoveSelection;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnStopAction(StopActionEvent data)
    {
        RebuildOptions();
    }

    void OnMoveSelection(MoveSelectionEvent data)
    {
        RebuildOptions();
    }

    void RebuildOptions()
    {
        for (int i = 0; i < objects.Count; i++)
            Destroy(objects[i]);

    }
}
