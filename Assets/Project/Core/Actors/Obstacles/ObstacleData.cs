using System;

[System.Serializable]
public struct ObstacleData
{
    public float Speed;
    public float MovementRange;
    public float YPositionOffset;
    public Range DelayRange;

    public ObstacleData(float speed, float movementRange, float yPositionOffset, Range delayRange)
    {
        Speed = speed;
        MovementRange = movementRange;
        YPositionOffset = yPositionOffset;
        DelayRange = delayRange;
    }
}
