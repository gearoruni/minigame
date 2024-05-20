using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDestroyTargetEffect : EffectBase
{
    public int condition;

    public override void Init(int define, Entity entity)
    {
        this.condition = define;
    }
    public override void OnCache()
    {

    }
}
