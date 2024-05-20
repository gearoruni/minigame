using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitComponent : Component
{
    CharacterDataComponent characterData;
    public HealthChangeEffect healthChangeEffect;

    MoveComponent moveComponent;
    public SpeedChangeEffect speedChangeEffect;

    CollisionComponent collision;
    public AOEEffect AOEEffect;

    public DirChangeEffect dirChangeEffect;
    public CanDestroyTargetEffect canDestroyTargetEffect;
    public override void Init()
    {
        characterData = (CharacterDataComponent)entity.GetComponent("CharacterDataComponent");
        collision = (CollisionComponent)entity.GetComponent("CollisionComponent");
    }

    public override void DataInit()
    {
        collision.OnBaseTriggerEnter2D += Invoke;
    }

    public void Invoke(Entity entity)
    {
        if(entity == null) {
            return;
        }
        EffectComponent effectComponent = (EffectComponent)entity.GetComponent("EffectComponent");
        if (effectComponent == null) return;

        healthChangeEffect = (HealthChangeEffect)effectComponent.GetEffect("HealthChangeEffect");
        speedChangeEffect = (SpeedChangeEffect)effectComponent.GetEffect("SpeedChangeEffect");
        dirChangeEffect = (DirChangeEffect)effectComponent.GetEffect("DirChangeEffect");
        canDestroyTargetEffect = (CanDestroyTargetEffect)effectComponent.GetEffect("CanDestroyTargetEffect");

    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<HitComponent>(this);
    }
}