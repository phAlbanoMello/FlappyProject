using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : View
{
    private MenuViewController _viewController;

    public override void Initialize()
    {
        base.Initialize();
        
        InitViewController();

        EventBus.Subscribe<GameStartedEvent>(HandleGameStarted);
    }

    private void InitViewController()
    {
        _viewController = gameObject.GetComponent<MenuViewController>();

        _viewController.Init();
    }

    private void HandleGameStarted(GameStartedEvent gameStartedEvent)
    {
        DisableView();
        _viewController.DisableStartAction();
        EventBus.Unsubscribe<GameStartedEvent>(HandleGameStarted);
    }
}
