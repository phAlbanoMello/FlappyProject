using System.Collections;
using UnityEngine;

public interface ISpawner
{
    public void SetSpawnRate(float spawnRate);
    public void StartSpawning();
    public void StopSpawning();
}
