using FlappyProject.Actions;
using FlappyProject.Interfaces;
using UnityEngine;

namespace FlappyProject.Managers
{
    [System.Serializable]
    public class PlayerManager : MonoBehaviour, IManager
    {
        [SerializeField] private PlayerSettings _playerData;

        private IActor _playerController;

        [SerializeField] private bool _autoStart;
        public bool ShouldInitializeAtStart { get { return _autoStart; } }
        public bool HasInitiated { get; private set; }

        public void Init()
        {
            _playerController = new PlayerController(_playerData);
            _playerController.Initialize();
            HasInitiated = true;
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
