using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : Component
{
    public Action<Entity> OnCollisionEnter2D;
    public Action<Entity> OnCollisionExit2D;
    public Action<Entity> OnTriggerEnter2D;
    public Action<Entity> OnTriggerExit2D;

    public float radis;
    public override void Init()
    {
        radis = dataDefind / 2f;
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<CollisionComponent>(this);
    }
}
