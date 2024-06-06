using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyEffect : EffectBase
{
    StateComponent state;
    public override void Init(int define, Entity entity)
    {
        this.entity = entity;
        state = (StateComponent)entity.GetComponent("StateComponent");
    }
    public override void Invoke(Entity entity)
    {
        state.state = State.DESTROY;
    }
    public override void OnCache()
    {

    }
}

