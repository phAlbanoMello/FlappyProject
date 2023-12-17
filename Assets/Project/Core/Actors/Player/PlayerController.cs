using FlappyProject.Interfaces;
using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

namespace FlappyProject.Actions
{
    public class PlayerController : IActor
    {
        private PlayerSettings _settings;

        private CollisionDetection _playerCollision;
        private Rigidbody2D _targetRigidbody;
        private Action _playerDiedEvent;

        private bool gameStarted = false;

        public PlayerController(PlayerSettings data)
        {
            _settings = data;
        }

        public void Initialize()
        {
            if (_settings.PlayerGameObject == null) { Debug.LogError("Target Actor was not set at the PlayerManager."); return; }

            EventBus.Subscribe<PlayerDiedEvent>(HandleDeath);
            _targetRigidbody = _settings.PlayerGameObject.GetComponent<Rigidbody2D>();

            MoveObjectToPosition(
                _settings.PlayerGameObject,
                Vector2.zero, 2f,
                EnableMovement
            );
        }

        private void EnableMovement()
        {
            //TODO:Countdown before starting
            //Synchronize with obstacles
            EnableActions();
            SetupJumpInputAction();
        }

        private void EnablePhysics()
        {
            _targetRigidbody.simulated = true;
            _targetRigidbody.velocity = Vector2.zero;
        }

        private void SetupJumpInputAction()
        {
            MovementData moveData = _settings.MovementData;
       
            _settings.Jump.performed += context =>
            {
                if (!gameStarted)
                {
                    gameStarted = true;
                    EnablePhysics();
                    return;
                }
                float jumpForce = moveData.JumpForce;
                if (context.interaction is SlowTapInteraction)
                {
                    jumpForce = moveData.JumpForce * moveData.Speed;
                    Jump(jumpForce);
                    return;
                }
                Jump(jumpForce);
            };
        }

        private void Jump(float jumpForce)
        {
            if (_targetRigidbody != null)
            {
                _targetRigidbody.velocity = Vector2.zero;
                _targetRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        public void UpdateActor(float deltaTime)
        {
            HandleRotation(deltaTime);
        }

        private void HandleRotation(float deltaTime)
        {
            if (_targetRigidbody != null)
            {
                float tiltAngle = Mathf.Clamp(
                    _targetRigidbody.velocity.y * _settings.MovementData.RotationSpeed, 
                    -_settings.MovementData.RotationAngle,
                    _settings.MovementData.RotationAngle
                );

                _settings.PlayerGameObject.transform.eulerAngles = new Vector3(0, 0, tiltAngle);
            }
        }
        
        private void HandleDeath(PlayerDiedEvent playerDiedEvent)
        {
            DisableActions();
            Debug.Log("Player died");
            EventBus.Unsubscribe<PlayerDiedEvent>(HandleDeath);
        }

        public void EnableActions()
        {
            _settings.Jump.Enable();
        }
        public void DisableActions()
        {
            _settings.Jump.Disable();
        }
        public void Destroy()
        {
           
        }

        private void MoveObjectToPosition(GameObject objectToMove, Vector3 targetPosition, float speed, Action onCompleteCallback)
        {
            float distance = Vector3.Distance(objectToMove.transform.position, targetPosition);
            float duration = distance / speed;

            LeanTween.move(objectToMove, targetPosition, duration)
                .setEase(LeanTweenType.easeInOutBack)
                .setOnComplete(onCompleteCallback);
        }

    }
}
