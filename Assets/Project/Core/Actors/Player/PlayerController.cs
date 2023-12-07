using FlappyProject.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace FlappyProject.Actions
{
    public class PlayerController : IActor
    {
        private MovementData _movementData;

        private GameObject _gameObject;
        private Rigidbody2D _targetRigidbody;

        private InputAction jump;

        public PlayerController(InputAction jumpAction, GameObject gameObject, MovementData movementData)
        {
            jump = jumpAction;
            _gameObject = gameObject;
            _movementData = movementData;
        }

        public void Initialize()
        {
            EnableActions();

            if (_targetRigidbody == null)
            {
                _targetRigidbody = _gameObject.GetComponent<Rigidbody2D>();
            }

            jump.performed += context =>
            {
                if (context.interaction is SlowTapInteraction)
                {
                    Jump(_movementData.JumpForce * _movementData.Speed);
                    return;
                }
                Jump(_movementData.JumpForce);
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

        public void Update(float deltaTime)
        {
            HandleRotation(deltaTime);
        }

        private void HandleRotation(float deltaTime)
        {
            if (_targetRigidbody != null)
            {
                float tiltAngle = Mathf.Clamp(_targetRigidbody.velocity.y * _movementData.RotationSpeed, -_movementData.RotationAngle, _movementData.RotationAngle);
                _gameObject.transform.eulerAngles = new Vector3(0, 0, tiltAngle);
            }
        }

        public void EnableActions()
        {
            jump.Enable();
        }
        public void DisableActions()
        {
            jump.Disable();
        }
        public void Destroy()
        {
            throw new System.NotImplementedException();
        }
    }
}
