using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler 
{
    public Entity CreateEntity(int instanceId,int entityId)
    {
        Entity entity = CachePool.Instance.Get<Entity>();
        if (entity == null) 
        { 
            Debug.LogError("cache失败"); 
            return null;
        }
        entity.Init(instanceId, entityId);

        CreateComponents(entity);

        entity.InitComponents();

        return entity;
    }

    private void CreateComponents(Entity entity)
    {
        //注意组件添加顺序
        //组件添加顺序影响组件更新顺序
        AddComponen<StateComponent>(entity, "StateComponent");
        AddComponen<TagComponent>(entity, "TagComponent");

        AddComponen<CollisionComponent>(entity, "CollisionComponent");
        AddComponen<SpawnComponent>(entity, "SpawnComponent");


        AddComponen<InputComponent>(entity, "InputComponent");
        AddComponen<MoveComponent>(entity, "MoveComponent");
        AddComponen<TransformComponent>(entity, "TransformComponent");
        AddComponen<GoComponent>(entity, "GoComponent");

        AddComponen<CharacterComponent>(entity, "CharacterComponent");
        AddComponen<WeaponComponent>(entity, "WeaponComponent");

        AddComponen<SkillComponent>(entity, "SkillComponent");
        AddComponen<EffectComponent>(entity, "EffectComponent");
        AddComponen<DestroyComponent>(entity, "DestroyComponent");
    }

    private Component AddComponen<T>(Entity entity,string componentName) where T : Component, new()
    {
        int dataDefine;
        if (entity.GetComponentConfig(componentName,out dataDefine))
        {
            Component component = CachePool.Instance.Get<T>();

            if (!entity.componentNameToIdx.ContainsKey(componentName))
            {
                entity.componentNameToIdx.Add(componentName,entity.components.Count);
                entity.components.Add(component);
            } 

            component.name = componentName;
            component.entity = entity;
            component.dataDefind = dataDefine;

            return component;
        }
        return null;
    }
}
