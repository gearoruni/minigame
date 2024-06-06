using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase :PoolBaseClass
{
    public Entity entity;
    public virtual void Init(int define, Entity entity)
    {

    }
    public virtual void Invoke(Entity entity)
    {

    }

    public virtual void OnCache()
    {

    }
}
