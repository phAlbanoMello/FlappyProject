using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour, ISpawner
{
    //TODO: SpawingData must be configurable on a separate data class
    [SerializeField] private GameObject obstacleToSpawn;
    [SerializeField] private ObstacleData initialObstacleData;

    [SerializeField] private readonly List<Obstacle> spawnedObstacles = new List<Obstacle>();
    
    public void StartSpawning()
    {
        CreateObstacle();
    }

    public void StopSpawning()
    {
        foreach (var obstacle in spawnedObstacles)
        {
            obstacle.Destroy();
        }
        spawnedObstacles.Clear();
    }

    private void CreateObstacle()
    {
        GameObject obstacle = Instantiate(obstacleToSpawn, transform);
        Obstacle obstacleComponent = obstacle.GetComponent<Obstacle>();

        if (obstacleComponent != null)
        {
            obstacleComponent.Init(initialObstacleData);
            obstacleComponent.Move();
            spawnedObstacles.Add(obstacleComponent);
        }
    }
}
