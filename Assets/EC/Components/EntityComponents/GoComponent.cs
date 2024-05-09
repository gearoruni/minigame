using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class GoComponent : Component
{
    TransformComponent transform;
    public GameObject go;
    public string goName;
    public Entity parentEntity;

    public override void Init()
    {

        transform = (TransformComponent)entity.GetComponent("TransformComponent");
    }

    public void CreateGameObject(string name)
    {
        goName = name;
        //创建物体
        go = ObjectPool.Instance.GetObjectFromPool(name);
        if(go == null) { Debug.LogError("无对应物体!");return; }
        
        //添加碰撞
        if (entity.componentNameToIdx.ContainsKey("CollisionComponent") && go.GetComponent<CollisionListener>() == null)
        {
            go.AddComponent<CollisionListener>();
        }
        go.GetComponent<CollisionListener>()?.Init(entity);

        //指定生成位置的情况
        SpawnComponent spawnComponent = (SpawnComponent)entity.GetComponent("SpawnComponent");
        if (spawnComponent != null)
        {
            transform.position = spawnComponent.spawnPointPos;
        }
    }

    public void DestroyGameObject()
    {
        ObjectPool.Instance.ReturnObjectToPool(goName,go);
    }

    //切换Go
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
