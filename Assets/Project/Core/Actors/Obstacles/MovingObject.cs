using System;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    protected bool IsActivated;
    protected Vector2 StartPosition;

    private Action _move;

    public SpawnableObjectData ObstacleData { get; private set; }

    public virtual void Init(SpawnableObjectData data)
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

    protected void ResetPosition()
    {
        transform.position = StartPosition;
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    protected void SetMovementCallback(Action moveAction)
    {
        _move = moveAction;
    }

    protected void SetSpawnDelay(int delay)
    {
        Range delayRange = new Range(delay, delay);

        ObstacleData = new SpawnableObjectData(
            ObstacleData.Speed,
            ObstacleData.MovementRange,
            ObstacleData.YPositionOffset,
            delayRange
            );
    }

    protected Vector3 GetNewStartPosition()
    {
        float spawnLimit = transform.localScale.y / 2;
        float yPosOffset = UnityEngine.Random.Range(-spawnLimit, spawnLimit) * ObstacleData.YPositionOffset;
        return new Vector3(StartPosition.x, StartPosition.y + yPosOffset, 0);
    }

    protected virtual float GetNewDelay()
    {
        return UnityEngine.Random.Range(ObstacleData.DelayRange.Min, ObstacleData.DelayRange.Max);
    }
}
