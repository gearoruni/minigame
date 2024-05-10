using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectComponent : Component
{
    public List<EffectBase> baseEffects = new List<EffectBase>();
    public List<EffectBase> collisionEffects = new List<EffectBase>();  
    public Dictionary<string,int> collisionEffectDir = new Dictionary<string,int>();    
    public CollisionComponent collisionComponent;
    public override void Init()
    {
        collisionComponent = (CollisionComponent)entity.GetComponent("CollisionComponent");
    }
    public void SetEffect(EffectBase effectBase,string effectName = "",bool isCollision = false)
    {
        
        if (isCollision )
        {
            collisionEffectDir.Add(effectName, collisionEffects.Count);
            collisionEffects.Add(effectBase);
        }
        else
        {
            baseEffects.Add(effectBase);
        }
    }
    public EffectBase GetEffect(string effectName)
    {
        if(effectName!=null && collisionEffectDir.ContainsKey(effectName))
        {
            return collisionEffects[collisionEffectDir[effectName]];
        }
        return null;
    }
    public void BaseInvoke()
    {
        for (int i = 0; i < baseEffects.Count; i++)
        {
            baseEffects[i].Invoke();
        }
    }

    public void CollisionInvoke(Entity entity)
    {
        for(int i = 0; i < collisionEffects.Count; i++)
        {
            collisionEffects[i].Invoke();
        }
    }

    public override void OnCache()
    {
        baseEffects.Clear();
        collisionEffects.Clear();
        collisionEffectDir.Clear();

        CachePool.Instance.Cache<EffectComponent>(this);
    }
}
