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
        // ����������λ�õ�����
        Vector3 directionToMouse = dir - playerTransform.position;

        // �����������ڰ뾶��Χ��
        directionToMouse = directionToMouse.normalized * radius;

        // �������λ������Ϊ���λ�ü�������
        weaponTransform.position = playerTransform.position + directionToMouse;

        // �������Ӧ�ó���ķ���
        Vector3 directionToLook = dir - playerTransform.position;

        // ͨ��LookRotation�����������Ҫ�������ת
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, directionToLook);

        // Ӧ����ת������
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
