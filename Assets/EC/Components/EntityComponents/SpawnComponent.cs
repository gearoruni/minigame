using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : Component
{
    public Vector3 spawnPointPos;
    public override void Init()
    {
        spawnPointPos = SpawnManager.Instance.GetSpawnPoint(dataDefind);
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<SpawnComponent>(this);
    }
}
