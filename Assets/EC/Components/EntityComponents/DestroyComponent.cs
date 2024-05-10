using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : Component
{
    public bool needColliderDestroy = false;
    CollisionComponent collision;
    public bool delayDestroy =false;
    public float livingTime;

    public override void Init()
    {
        
    }
    public void SetNeedColliderDestroy()
    {
        needColliderDestroy = true;
        collision = (CollisionComponent)entity.GetComponent("CollisionComponent");
        collision.OnBaseTriggerEnter2D += Destroy;
    }

    public void SetDestroyTimer(float time)
    {
        livingTime = time;
        delayDestroy = true;    
    }
    public void Destroy()
    {
       if (needColliderDestroy)
        {
            collision.OnBaseTriggerEnter2D -= Destroy;
        }

        EntityManager.Instance.RemoveEntity(entity.instanceId);
    }
    
    public override void Update()
    {
        if(delayDestroy)
        {
            livingTime -= Time.deltaTime;
            if(livingTime<0)
            {
                Destroy();
            }
        }
    }

    public override void OnCache()
    {
        needColliderDestroy = false;
        delayDestroy = false;
        livingTime = 0;
        CachePool.Instance.Cache<DestroyComponent>(this);
    }
}
