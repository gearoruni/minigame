using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChangeEffect : EffectBase
{
    public int effectData;

    public int dmgCnt = 1;

    public Dictionary<int, int> target = new Dictionary<int, int>();

    public override void Init(int define, Entity entity)
    {
        this.effectData = define;
    }

    public bool CanSetAtk(int instanceId)
    {
        int singledmgCnt; 
        if (target.TryGetValue(instanceId,out singledmgCnt))
        {
            if(singledmgCnt<dmgCnt)
            {
                target[instanceId] = singledmgCnt + 1; 
                return true;
            }
            return false;
        }
        target[instanceId] = 1;
        return true;
    }

    public override void OnCache()
    {
        target.Clear();
    }
}
