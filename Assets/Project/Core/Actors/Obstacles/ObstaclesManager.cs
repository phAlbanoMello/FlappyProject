using FlappyProject.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlappyProject.Managers
{
    public class ObstaclesManager : MonoBehaviour, IManager
    {        
        [SerializeField] private List<ObstacleSpawner> _spawns = new List<ObstacleSpawner>();

        [SerializeField] private bool _autoStart;
        public bool ShouldInitializeAtStart { get { return _autoStart; } }

        public bool HasInitiated { get; private set; }

        public void Init()
        {
            LoadSpawners();
            StartSpawningRoutines();
            HasInitiated = true;
        }

        private void LoadSpawners()
        {
            ObstacleSpawner[] obstacleSpawners = GetComponentsInChildren<ObstacleSpawner>();
            _spawns = obstacleSpawners.ToList<ObstacleSpawner>();
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
        }

        public void UpdateManager(float deltaTime)
        {
        }

 
    }
}