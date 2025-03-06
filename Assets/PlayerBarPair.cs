using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarPair : MonoBehaviour
{
    public Image BurstBar;
    public Image HealthBar;
    public int id;
    void SetPlayerID(int id)
    {
        this.id = id;
    }

    // Update is called once per frame
    void Start()
    {
        PlayerEventBus<PlayerDamagedEvent>.OnEvent += OnPlayerDamaged;
        PlayerEventBus<StopActionEvent>.OnEvent += OnStopAction;
    }

    private void OnDestroy()
    {
        PlayerEventBus<PlayerDamagedEvent>.OnEvent -= OnPlayerDamaged;
        PlayerEventBus<StopActionEvent>.OnEvent -= OnStopAction;
    }

    void OnStopAction(StopActionEvent e)
    {
        if (e.player.playerIndex != id)
            return;
        BurstBar.fillAmount = e.player.Burst / 100;
    }

    void OnPlayerDamaged(PlayerDamagedEvent e)
    {
        if (e.player.playerIndex != id)
            return;
        HealthBar.fillAmount = e.player.Health / e.player.BaseHealth;
    }
}
