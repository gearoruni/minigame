using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    IDLE,
    MOVE,
    FIRE,
    DEATH,
}

public class StateComponent : Component
{

    public bool isDead = false;
    public State state;

    public override void Init()
    {
        state = State.IDLE;
    }
    public override void Update()
    {

    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<StateComponent>(this);
    }
}
