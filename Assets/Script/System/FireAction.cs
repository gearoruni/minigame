using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class FireAction 
{
    /// <summary>
    /// 发射物体
    ///     创建子弹
    ///     附加效果
    ///     击出
    /// </summary>
    /// <param name="fireDefines">发射物体的基础信息</param>
    /// <param name="transmitter">发射器</param>
    public static void Fire(List<FireDefine> fireDefines,GameObject transmitter)
    {
        //foreach(FireDefine fireDefine in fireDefines)
        //{
        //    fireDefine.prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PathUtils.GetBulletPrefabFromID(fireDefine.bulletId));

        //    float timeBetweenBullets = fireDefine.timeBetweenBullets;

        //    for (int i = 0; i < fireDefine.volleyCount; i++)
        //    {
        //        Vector3 baseDir = transmitter.transform.up;
        //        // 随机生成一个角度
        //        float randomAngle = Random.Range(fireDefine.downLimit, fireDefine.upLimit);

        //        // 计算弹道的方向
        //        Vector2 direction = Quaternion.Euler(0f, 0f, randomAngle) * transmitter.transform.up;

        //        //GameCore.Instance.GetModel<TimerCtrl>().RegisterTimer(fireDefine.timeBetweenBullets, fireDefine.bulletsPerVolley, delegate()
        //        //{
        //        //    BaseFire(fireDefine, transmitter, direction);
        //        //}, true);
        //    }
        //}
    }

    /// <summary>
    /// 所有单发射击调用
    /// </summary>
    private static void BaseFire(FireDefine fireDefine, GameObject transmitter, Vector2 direction)
    {
        //创建子弹
        GameObject bullet = ObjectPoolManager.Instance.GetPrefabInstance(fireDefine.bulletId, fireDefine.prefab);

        bullet.transform.position = transmitter.transform.position;

        bullet.tag = transmitter.tag;

        BulletCmp bulletCmp = bullet.GetComponent<BulletCmp>();
        bulletCmp.Init(fireDefine.bulletData);

        bulletCmp.SetDestroyCmp(true);

        bulletCmp.SetHurtCmp("bullet");

        //发射
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        MoveAction.MoveRb(rb, (int)fireDefine.bulletData.speed, direction);
    }


    
}
