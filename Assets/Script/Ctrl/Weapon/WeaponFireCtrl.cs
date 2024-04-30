using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFireCtrl : BaseModel
{
    public WeaponData weaponData;
    public int nowBullets;
    public float fireCD;
    public float nowCd;

    public float reloadTime;
    public float reloadts = 0;
    public bool isReloading = false;
    public void Fire()
    {
        if (CheckFire())
        {
            FireAction.Fire(weaponData.fireDefines, GameCore.Instance.GetManager<PlayerManager>().weaponRoot);
            nowBullets--;
            nowCd = fireCD;
            Debug.Log("残弹数：" + nowBullets);
        }
        
    }
    public void WeaponRelaod()
    {
        isReloading = true;
    }

    public bool CheckFire()
    {
        if(nowBullets == 0)
        {
            Debug.Log("没子弹了");
            return false;
        }
        if(nowCd >= 0)
        {
            Debug.Log("在等待下一发");
            return false;
        }
        return true;
    }
    void BaseModel.OnStart()
    {
        weaponData = GameCore.Instance.GetManager<PlayerManager>().weaponData;
        nowBullets = weaponData.bulletsInClip;
        fireCD = weaponData.fireRate;
        nowCd = 0;
        reloadTime = weaponData.reloadTime;
    }

    void BaseModel.OnUpdate()
    {
        nowCd -= Time.deltaTime;
        if (isReloading)
        {
            reloadts += Time.deltaTime;
            Debug.Log("正在换弹");
            if (reloadts > reloadTime)
            {
                nowBullets = weaponData.bulletsInClip;
                reloadts = 0f;
                isReloading = false;
                Debug.Log("换弹结束");
            }
        }
    }

    bool BaseModel.Init()
    {
        return true;
    }

    UniTask<bool> BaseModel.InitAysnc()
    {
        return UniTask.FromResult(true);
    }

    void BaseModel.OnFixUpdate()
    {
        
    }

    void BaseModel.OnLateUpdate()
    {
        
    }
    void BaseModel.Release()
    {
        
    }
}
