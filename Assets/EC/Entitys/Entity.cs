using cfg;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Entity : PoolBaseClass
{
    public int entityId;
    public int instanceId;
    public int parentId;

    public List<int> childIds;

    public List<Component> components;
    public List<Component> lastComponents;
    public Dictionary<string,int> componentNameToIdx;

    public EntityConfigs entityConfig;
    public Dictionary<string, int> componentDatas;

    public GameObject go;
    internal Tag Tag;

    public Entity() {}

    public void Init(int instanceId,int entityId)
    {

        this.entityId = entityId;
        this.instanceId = instanceId;

        this.childIds = new List<int>();

        this.components = new List<Component>();
        this.lastComponents = new List<Component>();

        this.componentNameToIdx = new Dictionary<string, int>();
        componentDatas = new Dictionary<string, int>();

        entityConfig = TableDataManager.Instance.tables.BaseDefine.Get(entityId);
        parentId = entityConfig.ParentEntityId;
    }

    public void BindingGo(GameObject go)
    {
        this.go = go;
    }

    public void InitComponents()
    {
        for(int i = 0; i < components.Count; i++)
        {
            components[i].Init();
        }
        for (int i = 0; i < components.Count; i++)
        {
            components[i].DataInit();
        }
    }
    public void LateInitComponents()
    {
        for (int i = 0; i < lastComponents.Count; i++)
        {
            lastComponents[i].Init();
        }
        for (int i = 0; i < lastComponents.Count; i++)
        {
            lastComponents[i].DataInit();
        }
        lastComponents.Clear();
    }
    public void UpdateComponents()
    {
        for (int i = 0; i < components.Count; i++)
        {
            components[i].Update();
        }
    }

    public void AddComponent()
    {

    }
    public void RemoveComponent()
    {

    }

    public bool GetComponentConfig(string componentName,out int dataDefine)
    {
        dataDefine = 0;
        if (entityConfig == null) { return false; }  
        for(int i = 0;i<entityConfig.ComponentName.Count;i++)
        {
            if (entityConfig.ComponentName[i] == componentName){
                dataDefine = entityConfig.ComponentDefine[i];
                return true; 
            }
        }
        return false;
    }

    public void OnCache()
    {
        for(int i = 0; i < childIds.Count; i++)
        {
            Entity entity = EntityManager.Instance.GetEntityFromInstanceId(childIds[i]);
            EntityManager.Instance.RemoveEntity(entity.instanceId);
        }
        childIds.Clear();

        GoComponent go = (GoComponent)GetComponent("GoComponent");
        if (go != null) go.DestroyGameObject();

        for (int i = 0; i < components.Count; i++)
        {
            components[i].OnCache();
        }
        components.Clear();
        componentNameToIdx.Clear();

        CachePool.Instance.Cache<Entity>(this);
    }

    public Component GetComponent(string v)
    {
        if (components.Count == 0) return null;
        if (!componentNameToIdx.ContainsKey(v))return null;
        return components[componentNameToIdx[v]];
    }

    public void DataSet(int cmpId)
    {
        ComponentData componentData = TableDataManager.Instance.tables.ComponentDefine[cmpId];
        for(int i = 0;i<componentData.ComponentName.Count;i++)
        {
            componentDatas.Add(componentData.ComponentName[i], componentData.Componentdataid[i]);
        }
    }
}
