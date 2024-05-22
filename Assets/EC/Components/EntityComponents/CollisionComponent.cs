using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : Component
{
    public CollisionListener listener;

    /// <summary>
    /// 主动回调 注意 传入参数可以为Null
    /// </summary>
    public Action<Entity> OnBaseTriggerEnter2D;
    public Action<Entity> OnBaseTriggerExit2D;


    public void SetListener(CollisionListener listener)
    {
        this.listener = listener;
    }

    public override void OnCache()
    {
        OnBaseTriggerEnter2D = null;
        listener = null;
        CachePool.Instance.Cache<CollisionComponent>(this);
    }
}
