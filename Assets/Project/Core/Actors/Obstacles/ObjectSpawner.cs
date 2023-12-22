using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] protected GameObject ObjectToSpawn;
    [SerializeField] protected SpawnableObjectData InitialObjectData;

    [SerializeField] protected readonly List<MovingObject> SpawnedObjects = new List<MovingObject>();
    
    public virtual void StartSpawning()
    {
        CreateObject();
    }

    public void StopSpawning()
    {
        foreach (MovingObject spawningObject in SpawnedObjects)
        {
            spawningObject.Destroy();
        }
        SpawnedObjects.Clear();
    }

    protected virtual void CreateObject()
    {
        GameObject spawningObject = Instantiate(ObjectToSpawn, transform);
        MovingObject movingObjectComponent = spawningObject.GetComponent<MovingObject>();

        if (movingObjectComponent != null)
        {
            movingObjectComponent.Init(InitialObjectData);
            movingObjectComponent.Move();
            SpawnedObjects.Add(movingObjectComponent);
        }
    }
}
