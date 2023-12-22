using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverViewController : ViewController
{
    [SerializeField] private InputAction _tryAgain;

    private bool _restartActionEnabled = false;

    internal void Init()
    {
        EventBus.Subscribe<ReadyToTryAgainEvent>(EnableRestartAction);
    }

    internal void DisableTryAgainAction()
    {
        if (_restartActionEnabled)
        {
            _tryAgain.Disable();
            _tryAgain.performed -= OnTryAgainPerformed;
            _restartActionEnabled = false;
            EventBus.Unsubscribe<ReadyToTryAgainEvent>(EnableRestartAction);
        }
    }

    private void EnableRestartAction(ReadyToTryAgainEvent @event)
    {
        if (!_restartActionEnabled)
        {
            _tryAgain.Enable();
            _tryAgain.performed += OnTryAgainPerformed;
            _restartActionEnabled = true;
        }
    }

    private void OnTryAgainPerformed(InputAction.CallbackContext context)
    {
        EventBus.Publish(new RestartGameEvent());
        DisableTryAgainAction();
    }
}
