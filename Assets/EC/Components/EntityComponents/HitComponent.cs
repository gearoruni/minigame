using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitComponent : Component
{
    public StateComponent stateComponent;
    public override void Init()
    {
        stateComponent = (StateComponent)entity.GetComponent("StateComponent");
    }
    public void Invoke(Entity entity)
    {
        EffectComponent effectComponent = (EffectComponent)entity.GetComponent("EffectComponent");
        if (effectComponent == null) return;

        ChangeHealth(effectComponent);
    }
    public void ChangeHealth(EffectComponent effectComponent)
    {
        HealthChangeEffect healthChangeEffect = (HealthChangeEffect)effectComponent.GetEffect("HealthChangeEffect");
        if(healthChangeEffect == null) return;

        stateComponent.ChangeHealth(healthChangeEffect.effectData);
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<HitComponent>(this);
    }
}