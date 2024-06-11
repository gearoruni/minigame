using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEEffect : EffectBase
{
    public AreaConfigs areaConfigs;
    public int invokeTime = 0;
    public override void Init(int define, Entity entity)
    {
        areaConfigs = TableDataManager.Instance.tables.AreaDefine.Get(define);
        this.entity = entity;
    }

    public override void Invoke(Entity coll)
    {
        if(areaConfigs.AlwaysActive == false && invokeTime > 0) {
            StateComponent destroy = (StateComponent)entity.GetComponent("StateComponent");
            destroy.state = State.DESTROY;
            return;
        }
        CircleSkillFieldListener circleSkillFieldListener = new CircleSkillFieldListener();
        circleSkillFieldListener.Init(areaConfigs, entity, Vector2.zero, new List<Entity>());
        circleSkillFieldListener.CheckColliders();

        foreach (Entity target in circleSkillFieldListener.targets)
        {
            HitComponent hit = (HitComponent)target.GetComponent("HitComponent");
            hit?.Invoke(entity);
        }
        circleSkillFieldListener.targets = null;
        invokeTime++;
    }

    public override void OnCache()
    {
        invokeTime = 0;
    }
}
