
using System;

public class GameOverView : View
{
    private GameOverViewController _viewController;

    public override void Initialize()
    {
        base.Initialize();

        InitViewController();
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventBus.Subscribe<PlayerDiedEvent>(HandlePlayerDied);
        EventBus.Subscribe<RestartGameEvent>(HandleGameRestarted);
    }

    private void InitViewController()
    {
        _viewController = GetComponent<GameOverViewController>();
        _viewController.Init();
    }

    private void HandleGameRestarted(RestartGameEvent @event)
    {
        DisableView();
        EventBus.Unsubscribe<RestartGameEvent>(HandleGameRestarted);
    }

    private void HandlePlayerDied(PlayerDiedEvent @event)
    {
        EnableView();
        EventBus.Publish(new ReadyToTryAgainEvent());
        EventBus.Unsubscribe<PlayerDiedEvent>(HandlePlayerDied);
    }
}
