using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : Component
{
    public CollisionListener listener;

    //主动回调
    public Action OnBaseCollisionEnter2D;
    public Action OnBaseCollisionExit2D;
    public Action OnBaseTriggerEnter2D;
    public Action OnBaseTriggerExit2D;

    public bool needListen = true;
    public override void Init()
    {
        
    }
    public override void DataInit()
    {
        needListen = entity.componentDatas["CollisionComponent"] == 1 ? true : false;
    }
    public void SetListener(CollisionListener listener)
    {
        this.listener = listener;
    }

    public override void OnCache()
    {
        OnBaseCollisionEnter2D = null;
        OnBaseCollisionExit2D = null;
        OnBaseTriggerEnter2D = null;
        OnBaseTriggerExit2D = null;
        listener.entity = null;
        CachePool.Instance.Cache<CollisionComponent>(this);
    }
}
