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
      
        public void Init()
        {
            _playerController = new PlayerController(_playerData);
            _playerController.Initialize();
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
