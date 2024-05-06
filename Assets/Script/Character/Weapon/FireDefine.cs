using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireDefine
{
    public int bulletId;
    public int upLimit;
    public int downLimit;
    public int volleyCount;

    public int bulletsPerVolley;
    public float timeBetweenBullets;
    public BulletData bulletData;

    public GameObject prefab;
    public FireDefine(int bulletId, int upLimit, int downLimit, int volleyCount,int bulletsPerVolley,float timeBetweenBullets)
    {
        this.bulletId = bulletId;
        this.upLimit = upLimit;
        this.downLimit = downLimit;
        this.volleyCount = volleyCount;
        this.bulletsPerVolley = bulletsPerVolley;
        this.timeBetweenBullets = timeBetweenBullets;

        bulletData = AssetDatabase.LoadAssetAtPath<BulletData>(PathUtils.GetBulletConfigsFromId(bulletId));
    }
}
