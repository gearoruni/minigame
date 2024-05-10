using cfg;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum MoveType
{
    WALK = 1,
    RUN = 2,
    DASH = 3,
}
public class MoveComponent : Component
{

    public TransformComponent transformComponent;

    public MoveData moveData;

    public Dictionary<MoveType,float> speedDir = new Dictionary<MoveType,float>();  

    public MoveType moveType;
    public Vector2 input;
    public Vector2 moveTo;
    public bool forceMoveOffset = false;
    public bool needRaycaster = true;
    public override void Init()
    {
        transformComponent = (TransformComponent)entity.GetComponent("TransformComponent");

        moveType = MoveType.WALK;
        if (dataDefind == 0) return;

        moveData = TableDataManager.Instance.tables.MoveDefine.Get(dataDefind);
        for (int i = 0; i < moveData.MoveType.Count; i++)
        {
            speedDir.Add((MoveType)moveData.MoveType[i], moveData.Speed[i]);
        }

    }

    public void SetSpeed(float speed)
    {
        this.speedDir[moveType] = speed;
    }
    public override void Update()
    {
        input = input.normalized;
        DoMove(input.x, input.y);
        Vector2 needTo = (Vector2)transformComponent.position + new Vector2(moveTo.x, moveTo.y).normalized * moveTo.magnitude;
        if(needRaycaster)
        {
            if (moveType != MoveType.DASH)
            {
                needTo = PhysicsRay.CheckCollision(transformComponent.position, moveTo.x, moveTo.y, moveData.Radis[(int)moveType - 1]);
            }
            else
            {
                needTo = PhysicsRay.CheckCollision(transformComponent.position, moveTo.x, moveTo.y, moveData.Radis[(int)moveType - 1], LayerMask.GetMask("Enemy"));
            }
        }
        transformComponent.SetPostion(needTo.x,needTo.y);
    }

    public void DoMove(float x, float y)
    {
        if (forceMoveOffset) { return; }
        moveTo.x =  x * speedDir[moveType] * Time.deltaTime;
        moveTo.y =  y * speedDir[moveType] * Time.deltaTime;
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<MoveComponent>(this);
    }
}
