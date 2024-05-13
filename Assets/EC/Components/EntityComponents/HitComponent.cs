using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitComponent : Component
{
    CharacterDataComponent characterData;
    public HealthChangeEffect healthChangeEffect;

    MoveComponent moveComponent;
    public SpeedChangeEffect speedChangeEffect;
    public override void Init()
    {
        characterData = (CharacterDataComponent)entity.GetComponent("CharacterDataComponent");
    }
    public void Invoke(Entity entity)
    {
        if(entity == null) {
            Debug.Log("¶ªÊ§entity");
        }
        EffectComponent effectComponent = (EffectComponent)entity.GetComponent("EffectComponent");
        if (effectComponent == null) return;

        healthChangeEffect = (HealthChangeEffect)effectComponent.GetEffect("HealthChangeEffect");
        speedChangeEffect = (SpeedChangeEffect)effectComponent.GetEffect("SpeedChangeEffect");
        
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<HitComponent>(this);
    }
}