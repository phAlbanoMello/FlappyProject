using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuViewController : ViewController
{
    [SerializeField] private InputAction _gameStart;
    
    private void OnEnable()
    {
        _gameStart.Enable();

        _gameStart.performed += context =>
        {
            EventBus.Publish(new GameStartedEvent());
        };
    }
    private void OnDisable()
    {
        _gameStart.Disable();
    }
}
