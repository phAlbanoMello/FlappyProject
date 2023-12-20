
using System;

public class GameOverView : View
{
    public bool Enabled { get { return IsEnabled; } private set { } }
    public override void Initialize()
    { 
        base.Initialize();
        EventBus.Subscribe<PlayerDiedEvent>(HandlePlayerDied);
        IsEnabled = gameObject.activeSelf;
    }

    private void HandlePlayerDied(PlayerDiedEvent @event)
    {
        EnableView();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerDiedEvent>(HandlePlayerDied);
    }
}
