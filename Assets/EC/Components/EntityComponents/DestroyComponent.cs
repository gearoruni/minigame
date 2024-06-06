using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : Component
{
    public bool needColliderDestroy = false;
    CollisionComponent collision;
    public bool delayDestroy =false;
    public float livingTime;

    public int condition = 0;
    HitComponent hit;
    public override void Init()
    {
        hit = (HitComponent)entity.GetComponent("HitComponent");
    }

    public override void DataInit()
    {
        int dataDefine;
        if (entity.componentDatas.TryGetValue("DestroyComponent", out dataDefine))
        {
            condition = dataDefine;
        }
    }

    //public void SetNeedColliderDestroy()
    //{
    //    needColliderDestroy = true;
    //    collision = (CollisionComponent)entity.GetComponent("CollisionComponent");
    //    collision.OnBaseTriggerEnter2D += ChangeState;
    //}

    public void SetDestroyTimer(float time)
    {
        livingTime = time;
        delayDestroy = true;    
    }
    public void Destroy()
    {
        EntityManager.Instance.RemoveEntity(entity.instanceId);
        if (BattleUI.Instance != null) BattleUI.Instance.ChangeMonsterHp(entity.instanceId, false);
    }
    public void ChangeState()
    {
        StateComponent state = (StateComponent)entity.GetComponent("StateComponent");
        state.state = State.DESTROY;
    }
    
    public override void Update()
    {
        if(delayDestroy)
        {
            livingTime -= Time.deltaTime;
            if(livingTime<0)
            {
                Destroy();
            }
        }
        if(hit !=null && hit.canDestroyTargetEffect != null )
        {
            if(hit.canDestroyTargetEffect.condition == this.condition)
            {
                hit.canDestroyTargetEffect = null;
                Debug.Log("±»´Ý»Ù");

                Destroy();
            }
        }
    }

    public override void OnCache()
    {
        needColliderDestroy = false;
        delayDestroy = false;
        livingTime = 0;
        CachePool.Instance.Cache<DestroyComponent>(this);
    }
}
