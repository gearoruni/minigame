using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapComponent : Component
{
    public CollisionListener collisionListener;
    public CollisionComponent collisionComponent;
    public List<Entity> entities = new List<Entity>();
    public bool continueOpen;
    public int atk;
    public int delay;
    public override void Init()
    {
        // collisionListener = entity.go.GetComponentInChildren<CollisionListener>();
        // collisionListener.Init(entity);
        collisionComponent = (CollisionComponent)entity.GetComponent("CollisionComponent");
        collisionComponent.OnBaseTriggerEnter2D += EnterTrap;
        collisionComponent.OnBaseTriggerExit2D += ExitTrap;
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

    private void EnterTrap(Entity entity)
    {
        if(entity == null)return;
        entities.Add(entity);
        Debug.Log("进入陷阱");
        if (entity.Tag != Tag.Player) return;
        continueOpen = true;
        OnClickTrap();
    }
    private void ExitTrap(Entity entity)
    {
        if(entities.Contains(entity))
            entities.Remove(entity);
        if(entity.Tag == Tag.Player)
        {
            continueOpen = false;
        }
    }
    private void OnClickTrap()
    {
        TimerManager.Instance.RegisterTimer(delay, 1, () =>
        {

            foreach (var entity in entities)
            {
                var cmp = (CharacterDataComponent)entity.GetComponent("CharacterDataComponent");
                if(cmp == null)continue;
                cmp.nowHp -= atk;
            }
            Debug.Log("陷阱启动");
            if (continueOpen) OnClickTrap();
        });
    }
}
