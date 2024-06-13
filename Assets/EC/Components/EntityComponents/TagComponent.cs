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
    Interactive = 5,
    Trap = 6,
}

public class TagComponent : Component
{
    CharacterComponent characterComponent;
    public Tag tag;

    public override void Init()
    {
        characterComponent = (CharacterComponent)entity.GetComponent("CharacterComponent");
    }
    public override void DataInit()
    {
        int datadefine;
        if (characterComponent != null)
        {
            Debug.Log(characterComponent.configs.Id);
            tag = (Tag)(characterComponent.configs.Id / 1100);
            entity.Tag = tag;
        }
        else if (entity.componentDatas.TryGetValue("TagComponent", out datadefine))
        {
            tag = (Tag)(datadefine);
            entity.Tag = tag;
        }
    }
    public void SetTag(Tag tag)
    {
        this.tag = tag;
    }
    public override void Update()
    {
        entity.Tag = this.tag;
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<TagComponent>(this);
    }
}
