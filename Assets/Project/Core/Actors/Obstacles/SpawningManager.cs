using FlappyProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlappyProject.Managers
{
    public class SpawningManager : MonoBehaviour, IManager
    {        
        [SerializeField] private List<ObjectSpawner> _spawns = new List<ObjectSpawner>();

        [SerializeField] private bool _autoStart;
        public bool ShouldInitializeAtStart { get { return _autoStart; } }

        public bool HasInitiated { get; private set; }
        
        private SoulRecoverySpawner _soulRecoverySpawner;

        public void Init()
        {
            SubscribeEvents();
            LoadSpawners();

            _soulRecoverySpawner = GetComponentInChildren<SoulRecoverySpawner>();
            _soulRecoverySpawner.Init();
        }

        private void SubscribeEvents()
        {
            EventBus.Subscribe<PlayerStartedMovingEvent>(HandleGameStart);
            EventBus.Subscribe<PlayerDiedEvent>(HandleGameOver);
        }
        private void UnsubscribeEvents()
        {
            EventBus.Unsubscribe<PlayerStartedMovingEvent>(HandleGameStart);
            EventBus.Unsubscribe<PlayerDiedEvent>(HandleGameOver);
        }

        private void HandleGameOver(PlayerDiedEvent @event)
        {
            Stop();
            UnsubscribeEvents();
        }

        private void HandleGameStart(PlayerStartedMovingEvent @event)
        {
            StartSpawningRoutines();
            HasInitiated = true;
        }

        private void LoadSpawners()
        {
            ObjectSpawner[] objectSpawners = GetComponentsInChildren<ObjectSpawner>();
            _spawns = objectSpawners.ToList();
        }

        private void StartSpawningRoutines()
        {
            foreach (var spawner in _spawns)
            {
                spawner.StartSpawning();
            }
        }

        public void Stop()
        {
            foreach (var spawner in _spawns)
            {
                spawner.StopSpawning();
            }
        }

        public void UpdateManager(float deltaTime)
        {
        }
    }
}