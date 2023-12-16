using FlappyProject.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyProject.Managers
{
    public class ObstaclesManager : MonoBehaviour, IManager
    {        
        [SerializeField] private List<ObstacleSpawner> _spawns = new List<ObstacleSpawner>();

        public void Init()
        {
            StartSpawningRoutines();
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