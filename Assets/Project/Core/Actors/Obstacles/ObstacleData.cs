using System;

[System.Serializable]
public struct ObstacleData
{
    public float speed;
    public float movementRange;
    public float yPositionOffset;
    public Range delayRange;

    public ObstacleData(float speed, float movementRange, float yPositionOffset, Range delayRange)
    {
        this.speed = speed;
        this.movementRange = movementRange;
        this.yPositionOffset = yPositionOffset;
        this.delayRange = delayRange;
    }
}
