using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CollisionDetection : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject collidingObject = col.gameObject;
        EventBus.Publish(new PlayerCollidedEvent(collidingObject));
    }
} 
