using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData
{
    private float speed;
    private float movementRange;

    public ObstacleData(float speed, float movementRange)
    {
        this.speed = speed;
        this.movementRange = movementRange;
    }

    public float Speed { get => speed; private set { } }

    public float MovementRange { get => movementRange; private set { } }
}
