using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : Component
{
    //被动触发回调
    public Action<Entity> OnCollisionEnter2D;
    public Action<Entity> OnCollisionExit2D;
    public Action<Entity> OnTriggerEnter2D;
    public Action<Entity> OnTriggerExit2D;

    //主动回调
    public Action OnBaseCollisionEnter2D;
    public Action OnBaseCollisionExit2D;
    public Action OnBaseTriggerEnter2D;
    public Action OnBaseTriggerExit2D;

    public float radis;
    public override void Init()
    {
        radis = dataDefind / 2f;
    }
    public void SetRadis(float radis)
    {
        this.radis = radis;
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<CollisionComponent>(this);
    }
}
