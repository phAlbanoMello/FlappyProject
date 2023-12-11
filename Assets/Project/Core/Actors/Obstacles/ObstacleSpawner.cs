using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class ObstacleSpawner : MonoBehaviour, ISpawner
{
    //TODO: Implement spawnRate (currently the obstacles reposition themselves using
    // Lean Tween OnComplete event
    //TODO: Test creating a range on Y for spawning
    //TODO: SpawingData must be configurable on a separate data class
    [SerializeField] private GameObject obstacleToSpawn;
    [SerializeField] private float spawnRate;
    [SerializeField] private int spawnLimit;
    [SerializeField] private int objectSpeed;
    [SerializeField] private float movementRange;

    [SerializeField] private readonly List<Obstacle> spawnedObstacles = new List<Obstacle>();
    public bool activated;

    public void SetSpawnRate(float spawnRate)
    {
        this.spawnRate = spawnRate;
    }

    public void StartSpawning()
    {
        activated = true;
        CreateObstacle();
    }

    public void StopSpawning()
    {
        activated = false;
        foreach (var obstacle in spawnedObstacles)
        {
            obstacle.Destroy();
        }
        spawnedObstacles.Clear();
    }

    private void CreateObstacle()
    {
        ObstacleData data = new ObstacleData(objectSpeed, movementRange);

        GameObject obstacle = Instantiate(
            obstacleToSpawn,
            transform.position, 
            obstacleToSpawn.transform.localRotation, transform
        );
        Obstacle obstacleComponent = obstacle.GetComponent<Obstacle>();

        if (obstacleComponent != null)
        {
            obstacleComponent.Init(data);
            obstacleComponent.Move();
            spawnedObstacles.Add(obstacleComponent);
        }
    }   
}
