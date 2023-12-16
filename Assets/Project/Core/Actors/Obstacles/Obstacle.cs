using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected bool IsActivated;
    protected Vector2 StartPosition;

    private Action _move;

    public ObstacleData ObstacleData { get; private set; }

    public virtual void Init(ObstacleData data)
    {
        ObstacleData = data;
        StartPosition = transform.position;
    }

    public void Activate()
    {
        IsActivated = true;
    }

    public void Deactivate()
    {
        IsActivated = false;
    }

    public virtual void Move()
    {
        if (_move != null)
        {
            Activate();
            _move.Invoke();
        }
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    protected void SetMovementCallback(Action moveAction)
    {
        _move = moveAction;
    }

    protected Vector3 GetNewStartPosition()
    {
        float spawnLimit = transform.localScale.y / 2;
        float yPosOffset = UnityEngine.Random.Range(-spawnLimit, spawnLimit) * ObstacleData.YPositionOffset;
        return new Vector3(StartPosition.x, StartPosition.y + yPosOffset, 0);
    }

    protected float GetNewDelay()
    {
        return UnityEngine.Random.Range(ObstacleData.DelayRange.Min, ObstacleData.DelayRange.Max);
    }
}
