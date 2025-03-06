using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterNameDisplay : MonoBehaviour
{
    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        UIEventBus<RedrawMoveEvent>.OnEvent += OnRedrawMove;
    }
    private void OnDestroy()
    {
        UIEventBus<RedrawMoveEvent>.OnEvent -= OnRedrawMove;
    }

    void OnRedrawMove(RedrawMoveEvent e)
    {
        text.text = e.character.name;
    }
}
