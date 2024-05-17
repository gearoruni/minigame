using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentComponent : Component
{
    public Entity parent;

    public void SetParent(Entity parent)
    {
        this.parent = parent;
        
        parent.childIds.Add(entity.instanceId);
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<HitComponent>(this);
    }
}