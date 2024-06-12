using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : Component
{
    public Vector3 spawnPointPos;
    public int BindBoxIdx;
    public override void Init()
    {
        int dataDefine;
        if (entity.componentDatas.TryGetValue("SpawnComponent",out dataDefine))
        {
            spawnPointPos = SpawnManager.Instance.GetSpawnPoint(dataDefine);
            BindBoxIdx = SpawnManager.Instance.GetBindBoxIdx(dataDefine);
        }

    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<SpawnComponent>(this);
    }
}
