using System.Collections;
using UnityEngine;

public class BasicObject : MovingObject
{
    public override void Init(SpawnableObjectData data)
    {
        base.Init(data);
        SetMovementCallback(OnMoveActionCalled);
    }

    public override void Move()
    {
        base.Move();
    }

    public void CancelMovement()
    {
        StopCoroutine(MoveDelayed());
        IsActivated = false;
        ResetPosition();
    }
    
    private void OnMoveActionCalled()
    {
        StartCoroutine(MoveDelayed());
    }

    private IEnumerator MoveDelayed()
    {
        yield return new WaitForSeconds(GetNewDelay());
        ResetFX();
        BasicMovement(this, Vector3.left, ObstacleData.Speed, ObstacleData.MovementRange);
    }

    private void BasicMovement(MovingObject obstacle, Vector3 moveDirection, float speed, float distance)
    {
        float timeToReachDestination = distance / speed;

        LeanTween.move(gameObject, transform.position + moveDirection * distance, timeToReachDestination)
            .setEase(LeanTweenType.linear)
            .setOnComplete(() => {
                transform.position = GetNewStartPosition();
                if (IsActivated)
                {
                    OnMoveActionCalled();
                }
            });
    }

    protected virtual void ResetFX()
    {
        
    }
}
