using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformComponent : Component
{
    public Vector3 position;
    public Quaternion rotation;

    public Vector3 lastPosition;
    public Quaternion lastRotation;

    public override void Init()
    {
        if(entity.go == null) return;
        position = this.entity.go.transform.position;
        rotation = this.entity.go.transform.rotation;

        lastPosition = position;
        lastRotation = rotation;
    }
    public void SetPostionOffset(float x, float y)
    {
        position.x += x;
        position.y += y;
    }
    public void SetPostion(float x, float y)
    {
        position.x = x;
        position.y = y;
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<TransformComponent>(this);
    }
}
