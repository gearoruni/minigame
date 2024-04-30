using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction
{
    public BulletData data;

    public GameObject bullet;

    public BulletAction(BulletData data, GameObject bullet)
    {
        this.data = data;
        this.bullet = bullet;
    }

    public void Init()
    {
        GameCore.Instance.GetModel<TimerCtrl>().RegisterTimer(data.livingTime,1,DestroyBullet);
    }

    public void DestroyBullet()
    {
        ObjectPoolManager.Instance.ReturnObjectToPool(data.id, bullet);
    }
}
