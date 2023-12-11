using FlappyProject.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleData obstacleData;

    private Action move;
    protected bool isActivated;
    protected Vector2 startPosition;

    public ObstacleData ObstacleData { get => obstacleData; private set { } }

    public virtual void Init(ObstacleData data) {
        obstacleData = data;
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
            startPosition = transform.position;
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
}
