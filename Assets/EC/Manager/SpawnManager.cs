using cfg.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    SpawnDefine spawnDefine;
    public override void Init()
    {
        spawnDefine = TableDataManager.Instance.tables.SpawnDefine;
    }
    public Vector3 GetSpawnPoint(int pointId)
    {
        float x = spawnDefine.Get(pointId).X;
        float y = spawnDefine.Get(pointId).Y;
        float z = spawnDefine.Get(pointId).Z;
        return new Vector3(x, y, z);
    }
}
