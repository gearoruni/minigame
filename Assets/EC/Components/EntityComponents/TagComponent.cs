using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tag
{
    Player = 0,
    Enemy = 1,
    Skill =2,
    Terrain = 3,
}

public class TagComponent : Component
{
    public Tag tag;

    public override void Init()
    {
        tag = (Tag)dataDefind;
    }
    public override void Update()
    {

    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<MoveComponent>(this);
    }
}
