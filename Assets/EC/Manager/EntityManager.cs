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
        //ï¿½Ç¹ï¿½ï¿½ï¿½
        AddMonsterByBindBox(entity);
        assembler.LateCreate(entity);

        addQueue.Enqueue(entity.instanceId);

        return entity;
    }
    /// <summary>
    /// ï¿½ï¿½ï¿½ï¿½Êµï¿½ï¿½
    /// </summary>
    /// <param name="entityId">Êµï¿½ï¿½Ä£ï¿½ï¿½ID</param>
    /// <param name="cmpId">ï¿½ï¿½ï¿½Ä£ï¿½ï¿½ID</param>
    /// <returns></returns>
    public Entity CreateEntity(int entityId,int cmpId)
    {

        Entity entity = assembler.CreateEntity(instanceId++, entityId,cmpId);

        //ï¿½Ç¹ï¿½ï¿½ï¿½
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
    /// ï¿½ï¿½Entityï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Entity
    /// </summary>
    /// <param name="parent">ï¿½ï¿½Entity</param>
    /// <param name="entityId">Ä£ï¿½ï¿½id</param>
    /// <param name="cmpId">cmpÄ£ï¿½ï¿½id</param>
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
        if(activeMonsters.TryGetValue(int.Parse(CameraManager.Instance.confiner.m_BoundingShape2D.name), out var monster))
        {
            if(monster.Contains(entity))
            {
                monster.Remove(entity);
                //boss»÷É±ºó´¥·¢¶Ô»°
                if(monster.Count == 0)
                {
                    activeMonsters.Remove(int.Parse(CameraManager.Instance.confiner.m_BoundingShape2D.name));
                    CameraManager.Instance.CloseFengsuo();
                    if(CameraManager.Instance.confiner!=null && CameraManager.Instance.confiner.m_BoundingShape2D.name == "8")
                    {
                        BattleUI.Instance.ShowTxt(7023, ()=>{PlayerBaseData.Instance.selectedCharacterList.Add(1002);});
                        // BattleUI.Instance.BossHp.transform.parent.gameObject.SetActive(false);
                        GameObject.Find("Item/lingshi").SetActive(false);
                    }
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
            if(entityInstance == null)continue;
            if(entityInstance.entityId == entityId) { result.Add(entityInstance); }
        }
        return result;
    }
    public Entity GetEntityFromInstanceId(int entityInstanceId)
    {
        if(entities.TryGetValue(entityInstanceId,out var entity))
        {
            return entity;
        }
        return null;
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
            BattleUI.Instance.ChangeHp(entity.instanceId, true);
        }
        monsters.Remove(idx);
        CameraManager.Instance.AwakeFengsuo();
    }

    public void AwakeZhaohuan(Entity entity)
    {
        this.entities.Add(entity.instanceId, entity);

        addQueue.Enqueue(entity.instanceId);
    }

    public void SleepMonster()
    {
        foreach(var monster in activeMonsters)
        {
            if(monsters.ContainsKey(monster.Key))monsters.Remove(monster.Key);
            monsters.Add(monster.Key,monster.Value);
            foreach(var entity in monster.Value)
            {
                entity.go.SetActive(false);
                BattleUI.Instance.ChangeHp(entity.instanceId, false);
            }
        }
        activeMonsters.Clear();
    }
}
