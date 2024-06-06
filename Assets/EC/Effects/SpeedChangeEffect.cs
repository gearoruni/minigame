using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangeEffect : EffectBase
{
    public SpeedChange speedChange;
    public override void Init(int define,Entity entity)
    {
        speedChange = TableDataManager.Instance.tables.SpeedEffectDefine.Get(define);
    }
    public override void OnCache()
    {

    }
}
