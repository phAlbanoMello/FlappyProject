using FlappyProject.Interfaces;
using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace FlappyProject.Actions
{
    public class PlayerController : IActor
    {
        private PlayerSettings _settings;
        private Rigidbody2D _targetRigidbody;

        private bool _gameStarted = false;
        private Vector2 _preStartPosition;

        public PlayerController(PlayerSettings data)
        {
            _settings = data;
        }

        public void Initialize()
        {
            if (_settings.PlayerGameObject == null) { Debug.LogError("Target Actor was not set at the PlayerManager."); return; }
            GetRigidbody();
            SetPreStartPosition();

            SubscribeEvents();

            MovePlayerToGameStartPosition();
        }

        private void SetPreStartPosition()
        {
            Collider2D playerCollider = _settings.PlayerGameObject.GetComponent<Collider2D>();
            float offScreenX = -Camera.main.orthographicSize * Camera.main.aspect - playerCollider.bounds.extents.x;
            Vector2 initialPosition = new Vector2(offScreenX, 0);

            _targetRigidbody.simulated = false;
            _settings.PlayerGameObject.transform.position = initialPosition;
        }

        private void MovePlayerToGameStartPosition()
        {
            MoveObjectToPosition(_settings.PlayerGameObject,Vector2.zero, 2f, EnableMovement);
        }

        private void SubscribeEvents()
        {
            EventBus.Subscribe<PlayerDiedEvent>(HandleDeath);
        }

        private void GetRigidbody()
        {
            _targetRigidbody = _settings.PlayerGameObject.GetComponent<Rigidbody2D>();
        }

        private void EnableMovement()
        {
            EnableActions();
            SetupJumpInputAction();
        }

        private void EnablePhysics()
        {
            _targetRigidbody.simulated = true;
            _targetRigidbody.velocity = Vector2.zero;

            EventBus.Publish(new PlayerStartedMovingEvent());
        }

        private void SetupJumpInputAction()
        {
            MovementData moveData = _settings.MovementData;
       
            _settings.Jump.performed += context =>
            {
                if (!_gameStarted)
                {
                    _gameStarted = true;
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

        private void MoveObjectToPosition(GameObject objectToMove, Vector3 targetPosition, float speed, Action onCompleteCallback = null)
        {
            float distance = Vector3.Distance(objectToMove.transform.position, targetPosition);
            float duration = distance / speed;

            LeanTween.move(objectToMove, targetPosition, duration)
                .setEase(LeanTweenType.easeInOutBack)
                .setOnComplete(()=> { onCompleteCallback?.Invoke(); });
        }

    }
}
