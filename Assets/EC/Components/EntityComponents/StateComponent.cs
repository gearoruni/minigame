using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum State
{
    BIRTH,
    IDLE,
    MOVE,

    BEFOREFIRE,
    FIRE,
    AFTERFIRE,

    DESTROY,
    WAITDESTROY,
    DEATH,
}

public class StateComponent : Component
{

    public bool isVaild = false;

    //public int health = 0;
    public bool isDead = false;
    public State state;

    public override void Init()
    {
        state = State.IDLE;
        isVaild = false;
    }

    public override void Update()
    {
        if (isDead)
        {
            state = State.DEATH;
            //test
            DestroyComponent destroy = (DestroyComponent)entity.GetComponent("DestroyComponent");
            destroy.Destroy();
        }
    }
    public override void OnCache()
    {
        isVaild = true;
        isDead = false;
        CachePool.Instance.Cache<StateComponent>(this);
    }

}
