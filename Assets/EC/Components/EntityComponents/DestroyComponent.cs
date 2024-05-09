using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : Component
{
    GoComponent go;
    public bool needColliderDestroy = false;
    public bool delayDestroy =false;
    public float livingTime;

    public override void Init()
    {
        go = (GoComponent)entity.GetComponent("GoComponent");
    }
    public void SetNeedColliderDestroy()
    {
        needColliderDestroy = true;
        CollisionComponent collision = (CollisionComponent)entity.GetComponent("CollisionComponent");
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
            CollisionComponent collision = (CollisionComponent)entity.GetComponent("CollisionComponent");
            collision.OnBaseTriggerEnter2D -= Destroy;
        }

        go.DestroyGameObject();

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
        go = null;
        needColliderDestroy = false;
        delayDestroy = false;
        livingTime = 0;
        CachePool.Instance.Cache<DestroyComponent>(this);
    }
}
