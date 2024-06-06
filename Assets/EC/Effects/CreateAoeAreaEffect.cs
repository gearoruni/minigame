using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CreateAoeAreaEffect : EffectBase
{
    Entity areaEntity;
    int define;
    public override void Init(int define, Entity entity)
    {
        this.define = define;
        this.entity = entity;
    }

    public override void Invoke(Entity collision)
    {
        if(areaEntity != null) { return; }
        areaEntity = EntityManager.Instance.CreateEntity(6, 3);

        EffectComponent effectComponent = (EffectComponent)areaEntity.GetComponent("EffectComponent");
        effectComponent.DataInit(define);

        TransformComponent transform = (TransformComponent)areaEntity.GetComponent("TransformComponent");
        transform.position = entity.go.transform.position;

        GoComponent go = (GoComponent)areaEntity.GetComponent("GoComponent");
        go.CreateGameObject("Temp");
    }

    public override void OnCache()
    {

    }
}

