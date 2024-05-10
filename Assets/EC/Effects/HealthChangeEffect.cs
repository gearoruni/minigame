using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChangeEffect : EffectBase
{
    public int effectData;
    public override void Init()
    {

    }
    public void SetEffectData(int effectData)
    {
        this.effectData = effectData;
    }

    public override void Invoke()
    {

    }
    public override void OnCache()
    {

    }
}
