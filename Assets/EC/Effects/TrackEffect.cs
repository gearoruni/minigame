using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TrackEffect : EffectBase
{
    public Entity entity;
    public float radius;
    public int time = 0;

    public GameObject selfGo;
    public TransformComponent selTransform;

    public TransformComponent target;
    public TrackListener listener;
    public override void Init(int define, Entity entity)
    {
        radius = define/10f;
        time = 0;
    }
    public override void Invoke(Entity entity)
    {
        this.entity = entity;
        selfGo = entity.go;
        selTransform = (TransformComponent)entity.GetComponent("TransformComponent");

        if (time == 0 && listener == null)
        {
            SetListener(entity);
            //SearchTarget(entity);
            return;
        }
        if (target == null) { return; }
        float dis = selTransform.GetDistance(target.position);
        if (dis > radius) return;
        else
        {
            Vector2 dir = target.position - selTransform.position;
            MoveComponent moveComponent = (MoveComponent)entity.GetComponent("MoveComponent");
            moveComponent.input = dir;
        }
    }
    public void SetListener(Entity entity)
    {
        listener = entity.go.GetComponentInChildren<TrackListener>();
        listener.Init(entity, this);
    }
    public void SearchTarget(Entity entity)
    {
        for (int i = 0; i < EntityManager.Instance.CharacterList.Count; i++)
        {
            Entity temp = EntityManager.Instance.CharacterList[i];
            TransformComponent transform = (TransformComponent)temp.GetComponent("TransformComponent");
            float dis = selTransform.GetDistance(transform.position);
            if (temp.Tag != entity.Tag && dis < radius)
            {
                target = transform;
                time = 1;
                return;
            }
        }
    }

    public override void OnCache()
    {
        
    }
}
