using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction:MonoBehaviour
{
    public BulletData data;

    public GameObject bullet;

    Timer timer;

    public void Init(BulletData data, GameObject bullet)
    {
        this.data = data;
        this.bullet = bullet;
        timer = GameCore.Instance.GetModel<TimerCtrl>().RegisterTimer(data.livingTime,1,DestroyBullet);
    }

    public void DestroyBullet()
    {

        ObjectPoolManager.Instance.ReturnObjectToPool(data.id, bullet);

        timer.Release();
    }
}
