using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoComponent : Component
{
    TransformComponent transform;
    public GameObject go;

    public override void Init()
    {
        if (go == null)
        {
            go = ObjectPool.Instance.GetObjectFromPool(dataDefind.ToString());
        }
        if (entity.componentNameToIdx.ContainsKey("CollisionComponent"))
        {
            go.AddComponent<CollisionListener>().Init(entity);
        }
        SpawnComponent spawnComponent = (SpawnComponent)entity.GetComponent("SpawnComponent");
        if (spawnComponent != null){
            go.transform.position = spawnComponent.spawnPointPos;
        }
        transform = (TransformComponent)entity.GetComponent("TransformComponent");
    }

    public override void Update()
    {
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<GoComponent>(this);
    }
}
