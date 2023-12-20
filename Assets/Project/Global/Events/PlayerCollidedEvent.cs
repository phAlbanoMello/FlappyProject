using UnityEngine;

public class PlayerCollidedEvent
{
    public GameObject _collidingObject { get; private set; }

    public PlayerCollidedEvent(GameObject collidedObject)
    {
        _collidingObject = collidedObject;
    }
}
