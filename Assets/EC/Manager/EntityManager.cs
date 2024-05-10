using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    public int instanceId = 0;
    public List<int> entityInstances = new List<int>();
    public Dictionary<int,Entity> entities = new Dictionary<int,Entity>();

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
    public void RemoveEntity(int instanceId,bool needCache = true)
    {
        Entity entity = entities[instanceId];
        if (entity == null) return;

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
}
