using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarPair : MonoBehaviour
{
    public Image BurstBar;
    public Image HealthBar;
    int id;
    void SetPlayerID(int id)
    {
        this.id = id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
