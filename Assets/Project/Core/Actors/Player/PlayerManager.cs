using FlappyProject.Actions;
using FlappyProject.Interfaces;
using System;
using UnityEngine;

namespace FlappyProject.Managers
{
    [System.Serializable]
    public class PlayerManager : MonoBehaviour, IManager
    {
        [SerializeField] private PlayerSettings _playerData;
        [SerializeField] private Transform _vFXSpawnPoint;
        [SerializeField] private GameObject _vFXParent;

        private IActor _playerController;
        private PlayerVisual _playerVisual;

        [SerializeField] private bool _autoStart;
        public bool ShouldInitializeAtStart { get { return _autoStart; } }
        public bool HasInitiated { get; private set; }

        public void Init()
        {
            CreatePlayerVisualHandler();
            EventBus.Subscribe<ReadyToStartEvent>(HandleReadyToStart);
        }

        private void CreatePlayerVisualHandler()
        {
            _playerVisual = new PlayerVisual(_playerData.PlayerGameObject, _vFXSpawnPoint, _vFXParent);
        }

        private void InitPlayerController()
        {
            _playerController = new PlayerController(_playerData);
            _playerController.Initialize();
            HasInitiated = true;
        }

        private void HandleReadyToStart(ReadyToStartEvent @event)
        {
            InitPlayerController();
            _playerVisual.Show();
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
