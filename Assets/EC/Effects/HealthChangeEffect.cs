using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChangeEffect : EffectBase
{
    public int effectData;
    public override void Init(int define)
    {
        this.effectData = define;
    }

    public override void OnCache()
    {

    }
}
