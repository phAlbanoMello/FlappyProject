using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour, ISpawner
{
    //TODO: SpawingData must be configurable on a separate data class
    [SerializeField] private GameObject _obstacleToSpawn;
    [SerializeField] private ObstacleData _initialObstacleData;

    [SerializeField] private readonly List<Obstacle> _spawnedObstacles = new List<Obstacle>();
    
    public void StartSpawning()
    {
        CreateObstacle();
    }

    public void StopSpawning()
    {
        foreach (var obstacle in _spawnedObstacles)
        {
            obstacle.Destroy();
        }
        _spawnedObstacles.Clear();
    }

    private void CreateObstacle()
    {
        GameObject obstacle = Instantiate(_obstacleToSpawn, transform);
        Obstacle obstacleComponent = obstacle.GetComponent<Obstacle>();

        if (obstacleComponent != null)
        {
            obstacleComponent.Init(_initialObstacleData);
            obstacleComponent.Move();
            _spawnedObstacles.Add(obstacleComponent);
        }
    }
}
