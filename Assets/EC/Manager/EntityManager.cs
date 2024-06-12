using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    public int instanceId = 0;
    public List<int> entityInstances = new List<int>();
    public Dictionary<int,Entity> entities = new Dictionary<int,Entity>();
    public Dictionary<GameObject, Entity> GEList = new Dictionary<GameObject, Entity>();
    public Dictionary<int,List<Entity>> monsters = new Dictionary<int, List<Entity> >();
    public Dictionary<int,List<Entity>> activeMonsters = new Dictionary<int, List<Entity>>();
    public List<Entity> CharacterList = new List<Entity>();

    private Assembler assembler = new Assembler();

    private Queue<int> addQueue = new Queue<int>();
    private Queue<int> removeQueue = new Queue<int>();
    public bool isStop;
    public override void Init()
    {
        BehaviourCtrl.Instance.OnUpdate += UpdateEntity;
    }

    public Entity CreateEntity(int entityId)
    {

        Entity entity = assembler.CreateEntity(instanceId++, entityId);

        this.entities.Add(entity.instanceId, entity);
        //是怪物
        AddMonsterByBindBox(entity);
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

        //是怪物
        AddMonsterByBindBox(entity);
        this.entities.Add(entity.instanceId, entity);

        assembler.LateCreate(entity);

        addQueue.Enqueue(entity.instanceId);

        return entity;
    }

    
    public Entity CreateEntity(int entityId, int cmpId, bool isZhaohuan)
    {
        Entity entity = assembler.CreateEntity(instanceId++, entityId,cmpId);
        
        assembler.LateCreate(entity);
        AddMonsterByBindBox(entity);
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
        Debug.Log(CameraManager.Instance.confiner.m_BoundingShape2D.name);
        if(activeMonsters.TryGetValue(int.Parse(CameraManager.Instance.confiner.m_BoundingShape2D.name), out var monster))
        {
            if(monster.Contains(entity))
            {
                monster.Remove(entity);
                if(monster.Count == 0)
                {
                    activeMonsters.Remove(int.Parse(CameraManager.Instance.confiner.m_BoundingShape2D.name));
                    CameraManager.Instance.CloseFengsuo();
                }
            }
        }
        CharacterList.Remove(entity);
        this.entities[instanceId] = null;

        entity.OnCache();
    }
    public void RemoveAllEntity()
    {
        foreach(var entity in entities.Values)
        {
            
            if (entity == null) continue;

            entity.OnCache();
        }
        activeMonsters.Clear();
        monsters.Clear();
        CharacterList.Clear();
        entities.Clear();
    }
    public void UpdateEntity()
    {
        if (isStop) return;
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

    public List<Entity> GetEntityFromEntityId(int entityId)
    {
        List<Entity> result = new List<Entity>();
        foreach(Entity entityInstance in entities.Values)
        {
            if(entityInstance.entityId == entityId) { result.Add(entityInstance); }
        }
        return result;
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

    public void AddMonsterByBindBox(Entity entity)
    {
        if(entity.entityId != 4)return;
        var cmp = (SpawnComponent)entity.GetComponent("SpawnComponent");
        var collider = CameraManager.Instance.confiner.m_BoundingShape2D;
        var boxidx = collider==null ? 1 : int.Parse(collider.name);
        if(cmp.BindBoxIdx == boxidx)
        {
            if(!activeMonsters.TryGetValue(cmp.BindBoxIdx, out var entities))
            {
                entities = new List<Entity>();
                activeMonsters.Add(cmp.BindBoxIdx, entities);
            }
            entities.Add(entity);
        }
        else
        {
            if(!monsters.TryGetValue(cmp.BindBoxIdx, out var entities))
            {
                entities = new List<Entity>();
                monsters.Add(cmp.BindBoxIdx, entities);
            }
            entity.go.SetActive(false);
            entities.Add(entity);
        }
    }

    public void AwakeMonsterByBindBox(int idx)
    {
        if(!monsters.TryGetValue(idx, out var entities))return;
        activeMonsters.Add(idx, entities);
        foreach(var entity in entities)
        {
            entity.go.SetActive(true);
        }
        monsters.Remove(idx);
        CameraManager.Instance.AwakeFengsuo();
    }

    public void AwakeZhaohuan(Entity entity)
    {
        Debug.Log(entity.instanceId);
        this.entities.Add(entity.instanceId, entity);

        addQueue.Enqueue(entity.instanceId);
    }
}
