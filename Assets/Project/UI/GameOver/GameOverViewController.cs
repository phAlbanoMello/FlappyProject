using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverViewController : ViewController
{
    [SerializeField] private InputAction _tryAgain;
    private GameOverView _gameOverView;


    private void OnEnable()
    {
        _gameOverView = GetComponent<GameOverView>();
        _tryAgain.Enable();

        _tryAgain.performed += context =>
        {
            if (_gameOverView.Enabled)
            {
                EventBus.Publish(new RestartGameEvent());
            }
        };
    }
    private void OnDisable()
    {
        _tryAgain.Disable();
    }
}
