using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponDirCtrl : BaseModel
{
    private Transform weaponTransform;
    private Transform playerTransform;
    private float radius = 1f;
    public bool Init()
    {
        
        return true;
    }

    public void SetDir(Vector3 dir)
    {
        // 计算对象到鼠标位置的向量
        Vector3 directionToMouse = dir - playerTransform.position;

        // 将向量限制在半径范围内
        directionToMouse = directionToMouse.normalized * radius;

        // 将对象的位置设置为玩家位置加上向量
        weaponTransform.position = playerTransform.position + directionToMouse;

        // 计算对象应该朝向的方向
        Vector3 directionToLook = dir - playerTransform.position;

        // 通过LookRotation函数计算出需要朝向的旋转
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, directionToLook);

        // 应用旋转到对象
        weaponTransform.rotation = lookRotation;
    }
    public Vector2 GetTransmitterDir()
    {
        return weaponTransform.position - playerTransform.position;
    }

    public UniTask<bool> InitAysnc()
    {
        return UniTask.FromResult(true);
    }

    public void OnFixUpdate()
    {
        
    }

    public void OnLateUpdate()
    {
        
    }

    public void OnStart()
    {
        //weaponTransform = GameCore.Instance.GetManager<PlayerManager>().weaponRoot.transform;
        //playerTransform = GameCore.Instance.GetManager<PlayerManager>().playerRoot.transform;
        //this.weaponTransform.position = new Vector3(radius, radius, 0);
    }

    public void OnUpdate()
    {
        
    }

    public void Release()
    {
        
    }
}
