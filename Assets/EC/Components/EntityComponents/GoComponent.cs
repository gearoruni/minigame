using cfg;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class GoComponent : Component
{
    TransformComponent transform;
    CharacterComponent character;
    public GameObject go;
    public string goName;
    public Entity parentEntity;
    private bool InitFinish;

    public override void Init()
    {

        transform = (TransformComponent)entity.GetComponent("TransformComponent");
        character = (CharacterComponent)entity.GetComponent("CharacterComponent");
    }

    public override void DataInit()
    {
        int datadefine;
        if(character != null) {
            datadefine = character.configs.Id;
            CreateGameObject(datadefine.ToString());
        }
        else if(entity.componentDatas.TryGetValue("GoComponent", out datadefine))
        {
            CreateGameObject(datadefine.ToString());
        }
        InitFinish = true;
    }
    public void CreateGameObject(string name)
    {

        goName = name;
        if (go != null)
        {
            EntityManager.Instance.GEList.Remove(go);
            GameObject.Destroy(go);
            go = null;
        }
        //��������
        go = ObjectPool.Instance.GetObjectFromPool(name);
        if(go == null) { 
            Debug.LogError("�޶�Ӧ����!");return; 
        }

        entity.go = go;

        ////�����ײ
        if (entity.componentNameToIdx.ContainsKey("CollisionComponent"))
        {
            CollisionListener collisionListener = go.GetComponentInChildren<CollisionListener>();
            if (collisionListener == null)
            {
                Debug.Log("��Ԥ���� [" + name + "] ���CollisionListener!");
            }
            else
            {
                collisionListener.Init(entity);
            }
        }


        //ָ������λ�õ����
        SpawnComponent spawnComponent = (SpawnComponent)entity.GetComponent("SpawnComponent");
        if (spawnComponent != null && !InitFinish)
        {
            transform.position = spawnComponent.spawnPointPos;
        }
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;

        if (EntityManager.Instance.GEList.ContainsKey(go))
        {
            EntityManager.Instance.GEList[go] = entity;
        }
        else
        {
            EntityManager.Instance.GEList.Add(go, entity);
        }
        EntityManager.Instance.CharacterList.Add(entity);
    }

    public void DestroyGameObject()
    {
        ObjectPool.Instance.ReturnObjectToPool(goName,go);
    }

    //�л�Go
    public void ChangerGameObject(string name)
    {
        DestroyGameObject();
        CreateGameObject(name);
    }
    public override void Update()
    {
        if(go == null)return;
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
    }

    public override void OnCache()
    {
        go = null;
        CachePool.Instance.Cache<GoComponent>(this);
    }
}
