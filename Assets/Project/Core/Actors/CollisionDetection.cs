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
        LayerMask otherLayerMask = 1 << col.gameObject.layer;
        EventBus.Publish(new PlayerCollidedEvent(otherLayerMask));
    }
} 
