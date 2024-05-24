using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    public int instanceId = 0;
    public List<int> entityInstances = new List<int>();
    public Dictionary<int,Entity> entities = new Dictionary<int,Entity>();
    public Dictionary<GameObject, Entity> GEList = new Dictionary<GameObject, Entity>();
    public List<Entity> CharacterList = new List<Entity>();

    private Assembler assembler = new Assembler();

    private Queue<int> addQueue = new Queue<int>();
    private Queue<int> removeQueue = new Queue<int>();  
    public override void Init()
    {
        BehaviourCtrl.Instance.OnUpdate += UpdateEntity;
    }

    public Entity CreateEntity(int entityId)
    {

        Entity entity = assembler.CreateEntity(instanceId++, entityId);

        this.entities.Add(entity.instanceId, entity);

        assembler.LateCreate(entity);

        addQueue.Enqueue(entity.instanceId);

        return entity;
    }
    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="entityId">实体模板ID</param>
    /// <param name="cmpId">组件模板ID</param>
    /// <returns></returns>
    public Entity CreateEntity(int entityId,int cmpId)
    {

        Entity entity = assembler.CreateEntity(instanceId++, entityId,cmpId);

        this.entities.Add(entity.instanceId, entity);

        assembler.LateCreate(entity);

        addQueue.Enqueue(entity.instanceId);

        return entity;
    }
    /// <summary>
    /// 父Entity创建子Entity
    /// </summary>
    /// <param name="parent">父Entity</param>
    /// <param name="entityId">模板id</param>
    /// <param name="cmpId">cmp模板id</param>
    /// <returns></returns>
    public Entity ParentCreateEntity(Entity parent,int entityId, int cmpId,bool needUpdateFollow = true)
    {

        Entity entity = assembler.CreateEntity(instanceId++, entityId, cmpId);

        this.entities.Add(entity.instanceId, entity);

        assembler.LateCreate(entity);

        addQueue.Enqueue(entity.instanceId);

        TagComponent ptag = (TagComponent)parent.GetComponent("TagComponent");
        ((TagComponent)entity.GetComponent("TagComponent")).tag = ptag.tag;
        entity.parentId = parent.instanceId;
        if (needUpdateFollow)
        {
            parent.childIds.Add(entity.instanceId);
        }
        return entity;
    }
    public void RemoveEntity(int instanceId,bool needCache = true)
    {
        Entity entity = entities[instanceId];
        if (entity == null) return;
        CharacterList.Remove(entity);
        this.entities[instanceId] = null;

        entity.OnCache();
    }
    public void UpdateEntity()
    {
        Entity entity;
        for (int i = 0;i<entityInstances.Count;i++)
        {
            entity = entities[entityInstances[i]];
            if (entity == null)
            {
                removeQueue.Enqueue(entityInstances[i]);
            }
            else
            {
                entity.UpdateComponents();
            }
        }

        while (addQueue.Count > 0) 
        {
            int instanceId = addQueue.Dequeue();
            entity = entities[instanceId];
            if (entity != null)
            {
                entityInstances.Add(instanceId);

                entity.UpdateComponents();
            }
        }

        while(removeQueue.Count > 0)
        {
            int instanceId = removeQueue.Dequeue();
            entityInstances.Remove(instanceId);
        }

    }

    public Entity GetEntityFromEntityId(int entityId)
    {
        foreach(Entity entityInstance in entities.Values)
        {
            if(entityInstance.entityId == entityId) { return entityInstance; }
        }
        return null;
    }
    public Entity GetEntityFromInstanceId(int entityInstanceId)
    {
        return entities[entityInstanceId];
    }

    public void SetEntityController(bool isactive)
    {
        foreach(var  entity in entities.Values)
        {
            if (entity == null) continue;
            ControllerComponent controller = (ControllerComponent)entity.GetComponent("ControllerComponent");
            if(controller != null)
            {
                controller.isActive = isactive;
            }
        }
    }
}
