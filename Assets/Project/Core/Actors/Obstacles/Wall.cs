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
        StartCoroutine(MoveDelayed());
    }

    private void BasicMovement(Obstacle obstacle, Vector3 moveDirection, float speed, float distance)
    {
        float timeToReachDestination = distance / speed;

        LeanTween.move(gameObject, transform.position + moveDirection * distance, timeToReachDestination)
            .setEase(LeanTweenType.linear)
            .setOnComplete(() => {
                transform.position = GetNewStartPosition();
                if (isActivated)
                {
                    OnMoveActionCalled();
                }
            });
    }

    private IEnumerator MoveDelayed()
    {
        yield return new WaitForSeconds(GetNewDelay());
        BasicMovement(this, Vector3.left, ObstacleData.speed, ObstacleData.movementRange);
    }
}
