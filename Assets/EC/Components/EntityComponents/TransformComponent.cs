using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformComponent : Component
{
    public ControllerComponent controller;

    public Vector3 position;
    public Vector3 faceDir;
    public Quaternion rotation;

    public Vector3 lastPosition;
    public Quaternion lastRotation;

    public override void Init()
    {
        controller = (ControllerComponent)entity.GetComponent("ControllerComponent");

        if (entity.go == null) return;
        position = this.entity.go.transform.position;
        rotation = this.entity.go.transform.rotation;

        lastPosition = position;
        lastRotation = rotation;
    }
    public override void Update()
    {

        if (controller != null)
        {
            faceDir = GetPosDir(controller.facepos);
        }
    }

    public void SetPostionOffset(float x, float y)
    {
        position.x += x;
        position.y += y;
    }
    public void SetPostion(float x, float y)
    {
        lastPosition = position;

        position.x = x;
        position.y = y;

    }

    public void SetRotationLookAt(Vector3 lookPostion)
    {
        lastRotation = rotation;

        // �������Ӧ�ó���ķ���
        Vector3 directionToLook = lookPostion - position;

        // ͨ��LookRotation�����������Ҫ�������ת
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, directionToLook);

        // Ӧ����ת������
        rotation = lookRotation;

    }

    public void SetRotationLookAt(Vector3 lookPostion,Vector3 basePostion)
    {
        lastRotation = rotation;

        // �������Ӧ�ó���ķ���
        Vector3 directionToLook = lookPostion - basePostion;

        // ͨ��LookRotation�����������Ҫ�������ת
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, directionToLook);

        // Ӧ����ת������
        rotation = lookRotation;

    }

    public Vector2 GetPosDir(Vector3 position)
    {
        Vector3 dir = position - this.position;
        dir.Normalize();
        return dir;
    }

    public float GetDistance(Vector3 position)
    {
        Vector2 dir = position - this.position;
        return dir.magnitude;
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<TransformComponent>(this);
    }
}
