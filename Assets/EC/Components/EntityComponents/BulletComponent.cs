using cfg;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BulletComponent : Component
{

    public AnimationData animationData;
    public AnimatorListener listener;
    public StateComponent state;

    public override void Init()
    {
        state = (StateComponent)entity.GetComponent("StateComponent");
    }

    public override void DataInit()
    {
        int dataDefine;
        if (entity.componentDatas.TryGetValue("BulletComponent", out dataDefine))
        {
            DataInit(dataDefine);
        }
    }
    public void DataInit(int dataDefine)
    {
        animationData = TableDataManager.Instance.tables.AnimationDefine.Get(dataDefine);
        if (entity.go == null) return;
        //Ìí¼Ó¶¯»­
        if (animationData != null && entity.go.GetComponent<AnimatorListener>() == null)
        {
            entity.go.AddComponent<AnimatorListener>();
        }
        AnimatorListener listener = entity.go.GetComponent<AnimatorListener>();
        BindAnimator(listener);
        listener.Init(animationData.AnimationName, entity);
    }

    public void BindAnimator(AnimatorListener listener)
    {
        this.listener = listener;
    }

    public override void Update()
    {

        if(state.state == State.DESTROY)
        {
            listener.SetStateAnime("Destroy");
            state.state = State.WAITDESTROY;    
        }
        if(listener.CheckDestroyAnime())
        {
            state.isDead = true;
        }
    }
    public override void OnCache()
    {
        listener = null;
        CachePool.Instance.Cache<BulletComponent>(this);
    }
}
