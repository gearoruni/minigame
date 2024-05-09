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
    //instance id
    public List<int> childIds;

    public EntityManager selfManager;
    public List<Component> components;
    public Dictionary<string,int> componentNameToIdx;

    public EntityConfigs entityConfig;

    public GameObject go;
    public Entity() {}

    public void Init(int instanceId,int entityId)
    {

        this.entityId = entityId;
        this.instanceId = instanceId;

        this.childIds = new List<int>();
        this.selfManager = EntityManager.Instance;
        this.components = new List<Component>();
        this.componentNameToIdx = new Dictionary<string, int>();

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
        CachePool.Instance.Cache<Entity>(this);
        for (int i = 0; i < components.Count; i++)
        {
            components[i].OnCache();
        }

        components.Clear();
    }

    internal Component GetComponent(string v)
    {
        if (components.Count == 0) return null;
        if (!componentNameToIdx.ContainsKey(v))return null;
        return components[componentNameToIdx[v]];
    }
}
