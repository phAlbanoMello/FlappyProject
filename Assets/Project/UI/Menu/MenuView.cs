using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : View
{
    public override void Initialize()
    {
        base.Initialize();
        EventBus.Subscribe<GameStartedEvent>(HandleGameStarted);
    }

    private void HandleGameStarted(GameStartedEvent gameStartedEvent)
    {
        DisableView();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameStartedEvent>(HandleGameStarted);
    }
}
