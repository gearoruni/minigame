using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tag
{
    Player = 0,
    Enemy = 1,
    Skill =2,
    Terrain = 3,
    Weapon = 4,
}

public class TagComponent : Component
{
    public Tag tag;
    public Tag parent;

    public override void Init()
    {
        tag = (Tag)dataDefind;
        parent = (Tag)dataDefind;
    }
    public override void Update()
    {

    }
    public void SetParent(Tag tag)
    {
        parent = tag;
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<TagComponent>(this);
    }
}
