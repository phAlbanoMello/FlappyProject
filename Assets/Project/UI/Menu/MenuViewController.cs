using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuViewController : ViewController
{
    [SerializeField] private InputAction _gameStart;
    
    internal void DisableStartAction()
    {
        _gameStart.Disable();
    }

    internal void Init()
    {
        EventBus.Subscribe<ReadyToStartEvent>(EnableStartAction);
    }

    private void EnableStartAction(ReadyToStartEvent @event)
    {
        _gameStart.Enable();

        _gameStart.performed += context =>
        {
            EventBus.Publish(new GameStartedEvent());
        };

        EventBus.Unsubscribe<ReadyToStartEvent>(EnableStartAction);
    }
}
