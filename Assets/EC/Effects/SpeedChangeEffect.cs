using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangeEffect : EffectBase
{
    public SpeedChange speedChange;
    public override void Init(int define)
    {
        speedChange = TableDataManager.Instance.tables.SpeedEffectDefine.Get(define);
    }

    public override void Invoke(Entity entity)
    {

    }
    public override void OnCache()
    {

    }
}
