using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler 
{
    public Entity CreateEntity(int instanceId,int entityId,int cmp = 0)
    {
        Entity entity = CachePool.Instance.Get<Entity>();
        if (entity == null) 
        { 
            Debug.LogError("cache失败"); 
            return null;
        }
        entity.Init(instanceId, entityId);

        CreateComponents(entity);

        if (cmp != 0)
        {
            entity.DataSet(cmp);
        }

        entity.InitComponents();



        return entity;
    }

    public void LateCreate(Entity entity)
    {
        LateCreateComponents(entity);
        entity.LateInitComponents();
    }
    private void CreateComponents(Entity entity)
    {
        //注意组件添加顺序
        //组件添加顺序影响组件更新顺序
        AddComponen<CharacterComponent>(entity, "CharacterComponent");
        AddComponen<CharacterDataComponent>(entity, "CharacterDataComponent");

        AddComponen<StateComponent>(entity, "StateComponent");
        AddComponen<TagComponent>(entity, "TagComponent");
        AddComponen<ParentComponent>(entity, "ParentComponent");
        AddComponen<CollisionComponent>(entity, "CollisionComponent");
        AddComponen<SpawnComponent>(entity, "SpawnComponent");
        AddComponen<InputComponent>(entity, "InputComponent");
        AddComponen<ControllerComponent>(entity, "ControllerComponent");
        AddComponen<MoveComponent>(entity, "MoveComponent");
        AddComponen<TransformComponent>(entity, "TransformComponent");
        AddComponen<GoComponent>(entity, "GoComponent");
        AddComponen<BulletComponent>(entity, "BulletComponent");

        AddComponen<EffectComponent>(entity, "EffectComponent");
        AddComponen<DestroyComponent>(entity, "DestroyComponent");
        AddComponen<HitComponent>(entity, "HitComponent");
        AddComponen<AnimatorComponent>(entity, "AnimatorComponent");
    }

    private void LateCreateComponents(Entity entity)
    {
        LateAddComponen<WeaponComponent>(entity, "WeaponComponent");
        LateAddComponen<SkillComponent>(entity, "SkillComponent");
    }
    private void LateAddComponen<T>(Entity entity, string componentName) where T : Component, new()
    {
        int dataDefine;
        if (entity.GetComponentConfig(componentName, out dataDefine))
        {
            entity.lastComponents.Add(AddComponen<T>(entity, componentName));
        }
        
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

            return component;
        }
        return null;
    }
}
