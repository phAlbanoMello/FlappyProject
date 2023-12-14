using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private ObstacleData obstacleData;
    private Action move;
    protected bool isActivated;
    protected Vector2 startPosition;

    public ObstacleData ObstacleData { get => obstacleData; private set { } }

    public virtual void Init(ObstacleData data) {
        obstacleData = data;
        startPosition = transform.position;
    }

    public void Activate()
    {
        isActivated = true;
    }
    public void Deactivate()
    {
        isActivated = false;
    }

    public virtual void Move()
    {
        if (move != null)
        {
            Activate();
            move.Invoke();
        }
    }
    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    protected void SetMovementCallback(Action moveAction)
    {
        move = moveAction;
    }
    protected Vector3 GetNewStartPosition()
    {
        float spawnLimit = transform.localScale.y / 2;
        float yPosOffset = UnityEngine.Random.Range(-spawnLimit, spawnLimit) * ObstacleData.yPositionOffset;
        return new Vector3(startPosition.x, startPosition.y + yPosOffset, 0);
    }
    protected float GetNewDelay()
    {
        return UnityEngine.Random.Range(ObstacleData.delayRange.min, ObstacleData.delayRange.max);
    }
}
