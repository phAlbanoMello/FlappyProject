using FlappyProject.Actions;
using FlappyProject.Interfaces;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlappyProject.Managers
{
    [System.Serializable]
    public class PlayerManager : MonoBehaviour, IManager
    {
        [SerializeField] private PlayerSettings _playerData;

        private IActor _playerController;
        private Action<LayerMask> _playerDied;
        public Action<LayerMask> PlayerDiedEvent { get => _playerDied; private set { } }

        public void Init()
        {
            _playerController = new PlayerController(_playerData);
            _playerController.Initialize();
        }

        public void SubscribeToPlayerCollisionEvent(Action<LayerMask> callback)
        {
            CollisionDetection collisionDetection = _playerData.PlayerGameObject.GetComponent<CollisionDetection>();
            collisionDetection.OnCollisionDetected += callback;
        }

        public void Stop()
        {
            _playerController.DisableActions();
        }

        public void UpdateManager(float deltaTime)
        {
            _playerController.UpdateActor(deltaTime);
        }
    }
}
