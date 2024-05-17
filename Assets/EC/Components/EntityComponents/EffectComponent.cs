using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectComponent : Component
{
    public List<EffectBase> baseEffects = new List<EffectBase>();
    public List<EffectBase> collisionEffects = new List<EffectBase>();  
    public Dictionary<string,int> collisionEffectDir = new Dictionary<string,int>();    
    public CollisionComponent collisionComponent;
    EffectData effectData;
    Dictionary<string,int> effectNames;
    public override void Init()
    {
        collisionComponent = (CollisionComponent)entity.GetComponent("CollisionComponent");
        effectNames = new Dictionary<string,int>();
        if(collisionComponent != null )
            collisionComponent.OnBaseTriggerEnter2D += CollisionInvoke;
    }
    public void DataInit(int data)
    {
        effectData = TableDataManager.Instance.tables.EffectDefine.Get(data);
        for (int i = 0; i < effectData.Effectname.Count; i++)
        {
            effectNames.Add(effectData.Effectname[i],i);
        }
        SetEffect(new HealthChangeEffect(), "HealthChangeEffect",true);
        SetEffect(new MoveEffect(), "MoveEffect");
        SetEffect(new TrackEffect(), "TrackEffect");
        SetEffect(new SpeedChangeEffect(), "SpeedChangeEffect", true);
        SetEffect(new AOEEffect(), "AOEEffect");
        SetEffect(new CreateAoeAreaEffect(), "CreateAoeAreaEffect",true);
        SetEffect(new DestroyEffect(), "DestroyEffect",true);
        SetEffect(new DirChangeEffect(), "DirChangeEffect", true);
        SetEffect(new DirChangeEffect(), "DirChangeEffect");

    }
    public void SetEffect(EffectBase effectBase,string effectName = "",bool isCollision = false)
    {
        if (!effectNames.ContainsKey(effectName)) return; 
        if (isCollision )
        {
            collisionEffectDir.Add(effectName, collisionEffects.Count);
            collisionEffects.Add(effectBase);
        }
        else
        {
            baseEffects.Add(effectBase);
        }
        effectBase.Init(effectData.Effectdefine[effectNames[effectName]],entity);
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
            baseEffects[i].Invoke(entity);
        }
    }

    public void CollisionInvoke(Entity entity)
    {
        for(int i = 0; i < collisionEffects.Count; i++)
        {
            collisionEffects[i].Invoke(entity);
        }
    }
    public override void Update()
    {
        BaseInvoke();
    }
    public override void OnCache()
    {
        baseEffects.Clear();
        collisionEffects.Clear();
        collisionEffectDir.Clear();

        CachePool.Instance.Cache<EffectComponent>(this);
    }
}
