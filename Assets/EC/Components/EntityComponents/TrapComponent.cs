using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapComponent : Component
{
    public CollisionListener collisionListener;
    public CollisionComponent collisionComponent;
    public List<Entity> entities = new List<Entity>();
    public int atk;
    public int delay;
    public override void Init()
    {
        // collisionListener = entity.go.GetComponentInChildren<CollisionListener>();
        // collisionListener.Init(entity);
        collisionComponent = (CollisionComponent)entity.GetComponent("CollisionComponent");
        collisionComponent.OnBaseTriggerEnter2D += OnClickTrap;
        Debug.Log("enter");
    }
    public override void DataInit()
    {
        entity.componentDatas.TryGetValue("TrapDelay",out delay);
        entity.componentDatas.TryGetValue("TrapAtk", out atk);
        Debug.Log(delay);
    }
    public override void Update()
    {
        base.Update();
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<TransformComponent>(this);
    }

    private void OnClickTrap(Entity entity)
    {
        entities.Add(entity);
        Debug.Log("进入陷阱");
        if(entity.Tag != Tag.Player)return;
        TimerManager.Instance.RegisterTimer(delay,1,()=>{
            foreach(var entity in entities)
            {
                var cmp = (CharacterDataComponent)entity.GetComponent("CharacterDataComponent");
                cmp.nowHp-=atk;
            }
            Debug.Log("陷阱启动");
        });
    }
}
