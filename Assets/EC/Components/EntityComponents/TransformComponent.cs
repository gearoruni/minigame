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

        // 计算对象应该朝向的方向
        Vector3 directionToLook = lookPostion - position;

        // 通过LookRotation函数计算出需要朝向的旋转
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, directionToLook);

        // 应用旋转到对象
        rotation = lookRotation;

    }

    public void SetRotationLookAt(Vector3 lookPostion,Vector3 basePostion)
    {
        lastRotation = rotation;

        // 计算对象应该朝向的方向
        Vector3 directionToLook = lookPostion - basePostion;

        // 通过LookRotation函数计算出需要朝向的旋转
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, directionToLook);

        // 应用旋转到对象
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
