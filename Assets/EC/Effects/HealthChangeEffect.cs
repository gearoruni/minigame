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
        this.entity = entity;
    }

    public bool CanSetAtk(Entity entity)
    {
        if(entity.Tag == this.entity.Tag ) { return false; }
        int instanceId = entity.instanceId;
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
