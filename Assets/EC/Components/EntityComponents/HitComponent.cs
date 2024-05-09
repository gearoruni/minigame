using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitComponent : Component
{
    public override void Init()
    {

    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<HitComponent>(this);
    }
}