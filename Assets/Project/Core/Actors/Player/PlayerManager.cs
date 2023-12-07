using FlappyProject.Actions;
using FlappyProject.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlappyProject.Managers
{
    public class PlayerManager : MonoBehaviour, IManager
    {
        [SerializeField] private GameObject playerGameObject;

        [SerializeField] private float slowTapSpeedMultiplier;
        [SerializeField] private float jumpForce;
        [SerializeField] private float rotationAngle;
        [SerializeField] private float rotationSpeed;
        
        [SerializeField] private InputAction jump;
        
        private IActor _playerController;
        
        public void Init()
        {
            MovementData movementData = new MovementData(
                slowTapSpeedMultiplier, jumpForce, rotationAngle, rotationSpeed
            );

            _playerController = new PlayerController(jump, playerGameObject, movementData);
            _playerController.Initialize();
        }

        public void Stop()
        {
            _playerController.DisableActions();
        }

        public void UpdateManager(float deltaTime)
        {
            _playerController.Update(deltaTime);
        }
    }
}
