using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveEffect : EffectBase
{

    public float speed;
    public float livingTime;
    public float timesp;
    public override void Init(int define)
    {
        Move move = TableDataManager.Instance.tables.MoveEffectDefine.Get(define);
        speed = move.Speed;
        livingTime = move.Time;
        timesp = 0;
    }
    public override void Invoke(Entity entity)
    {
        MoveComponent moveComponent = (MoveComponent)entity.GetComponent("MoveComponent");
        StateComponent state = (StateComponent)entity.GetComponent("StateComponent");
        if (livingTime< timesp)
        {
            moveComponent.basespeed = 0;
            state.state = State.DESTROY;
            return;
        }
       
        timesp += Time.deltaTime;
        moveComponent.basespeed = speed;
    }
    public override void OnCache()
    {
        timesp = 0;
    }
}
