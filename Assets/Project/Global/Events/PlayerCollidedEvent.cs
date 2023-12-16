using UnityEngine;

public class PlayerCollidedEvent
{
    public LayerMask CollisionMask;

    public PlayerCollidedEvent(LayerMask collisionMask)
    {
        CollisionMask = collisionMask;
    }
}
