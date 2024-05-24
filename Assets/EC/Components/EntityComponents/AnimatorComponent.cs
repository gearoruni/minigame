using cfg;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum AnimType
{
    Idle,
    Move,
    Hurt,
    Death,
}

public class AnimatorComponent : Component
{
    public AnimationData animationData;

    public AnimatorListener listener;

    public TransformComponent transform;
    public StateComponent state;

    public string lastAnime;
    public const string firebefore = "_atk_f";
    public const string fire = "_atk";
    public const string fireafter = "_atk_a";

    public bool isAtk = false;
    public int skillidx = -1;
    Action useSkill;
    public override void Init()
    {
        transform = (TransformComponent)entity.GetComponent("TransformComponent");
        state = (StateComponent)entity.GetComponent("StateComponent");
    }

    public override void DataInit()
    {
        int dataDefine;
        if(entity.componentDatas.TryGetValue("AnimatorComponent",out dataDefine))
        {
            animationData = TableDataManager.Instance.tables.AnimationDefine.Get(dataDefine);
        }

        if (entity.go == null) return;

        //Ìí¼Ó¶¯»­
        if (entity.componentNameToIdx.ContainsKey("AnimatorComponent") && entity.go.GetComponent<AnimatorListener>() == null)
        {
            entity.go.AddComponent<AnimatorListener>();
        }
        AnimatorListener listener = entity.go.GetComponent<AnimatorListener>();

        BindAnimator(listener);
        listener.Init(animationData.AnimationName, entity);

        lastAnime = "Idle";
    }

    public override void Update()
    {
        if (animationData == null) return;
        if(isAtk) 
        {
            if (lastAnime == "Idle"|| lastAnime == "Move")
            {
                lastAnime = skillidx.ToString() + firebefore;
                PlayerAnime(lastAnime);
            }
            else if(lastAnime == skillidx.ToString() + firebefore)
            {
                if(listener.CheckAnime(lastAnime))
                {
                    useSkill?.Invoke();
                    lastAnime = skillidx.ToString() + fire;
                    PlayerAnime(lastAnime);
                }
            }
            else if (lastAnime == skillidx.ToString() + fire)
            {
                if (listener.CheckAnime(lastAnime))
                {
                    lastAnime = skillidx.ToString() + fireafter;
                    PlayerAnime(lastAnime);
                }
            }
            else
            {
                if (listener.CheckAnime(lastAnime))
                {
                    lastAnime = "Idle";
                    PlayerAnime(lastAnime);
                    isAtk = false;
                }
            }
            return; 
        }
        if(state != null)
        {
            switch(state.state)
            {
                case State.IDLE:
                    PlayerAnime("Idle");
                    lastAnime = "Idle";
                    break;
                case State.MOVE:
                    PlayerAnime("Move");
                    lastAnime = "Move";
                    break;
                case State.DEATH:
                    break;
                default:
                    PlayerAnime(lastAnime);
                    break;
            }
        }

        if (transform != null)
        {
            listener.SetParam(transform.faceDir.x, transform.faceDir.y);
        }
    }
    public void SetSkill(Action action)
    {
        useSkill = action;
    }
    public void BindAnimator(AnimatorListener listener)
    {
        this.listener = listener;
    }

    public void PlayerAnime(string animeName)
    {
        listener.SetStateAnime(animeName);
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<AnimatorComponent>(this);
    }
}
