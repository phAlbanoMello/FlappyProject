using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacle
{
    public override void Init(ObstacleData data)
    {
        base.Init(data);
        SetMovementCallback(OnMoveActionCalled);
    }

    public override void Move()
    {
        base.Move();
    }

    private void OnMoveActionCalled()
    {
        BasicMovement(this, Vector3.left, ObstacleData.Speed, ObstacleData.MovementRange);
    }

    private void BasicMovement(Obstacle obstacle, Vector3 moveDirection, float speed, float distance)
    {
        float timeToReachDestination = distance / speed;

        LeanTween.move(gameObject, transform.position + moveDirection * distance, timeToReachDestination)
            .setEase(LeanTweenType.linear)
            .setOnComplete(() => {
                if (isActivated)
                {
                    transform.position = startPosition;
                    OnMoveActionCalled();
                }
                else
                {
                    transform.position = startPosition;
                }
                Debug.Log("Finished Moving");
            });
    }
}
