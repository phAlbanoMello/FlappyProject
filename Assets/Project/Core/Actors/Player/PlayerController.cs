using FlappyProject.Interfaces;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace FlappyProject.Actions
{
    public class PlayerController : IActor
    {

        //TODO: Handle losing lives 
        private PlayerSettings _settings;

        private CollisionDetection _playerCollision;
        private Rigidbody2D _targetRigidbody;
        private Action _playerDiedEvent;

        public PlayerController(PlayerSettings data)
        {
            _settings = data;
        }

        public void Initialize()
        {
            if (_settings.PlayerGameObject == null) { Debug.LogError("Target Actor was not set at the PlayerManager.");  return;}

            _targetRigidbody = _settings.PlayerGameObject.GetComponent<Rigidbody2D>();
            
            SetupCollision();
            EnableActions();
            SetupJumpInputAction();
        }

        private void SetupJumpInputAction()
        {
            MovementData moveData = _settings.MovementData;
       
            _settings.Jump.performed += context =>
            {
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

        private void SetupCollision()
        {
            _playerCollision = _settings.PlayerGameObject.GetComponent<CollisionDetection>();
            _playerCollision.OnCollisionDetected += HandleCollision;
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
        
        private void HandleCollision(LayerMask layerMask)
        {
            if (layerMask == _settings.HazardLayer)
            {
                DisableActions();
                Debug.Log("Player died");
            }
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
            throw new System.NotImplementedException();
        }
    }
}
