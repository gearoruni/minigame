using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    WALK = 1,
    RUN = 2,
    DASH = 3,
}
public class MoveComponent : Component
{

    public TransformComponent transformComponent;
    public CollisionComponent collisionComponent;

    public MoveData moveData;

    public Dictionary<MoveType,float> speedDir = new Dictionary<MoveType,float>();  

    public MoveType moveType;
    public Vector2 input;
    public Vector2 moveTo;
    public bool forceMoveOffset = false;

    public override void Init()
    {
        transformComponent = (TransformComponent)entity.GetComponent("TransformComponent");
        collisionComponent = (CollisionComponent)entity.GetComponent("CollisionComponent");
        moveData = TableDataManager.Instance.tables.MoveDefine.Get(dataDefind);
        for (int i = 0; i < moveData.MoveType.Count; i++)
        {
            speedDir.Add((MoveType)moveData.MoveType[i], moveData.Speed[i]);
        }
        moveType = MoveType.WALK;
    }
    public override void Update()
    {
        input = input.normalized;
        DoMove(input.x, input.y);
        Vector2 needTo;
        if (moveType!= MoveType.DASH)
        {
            needTo = PhysicsRay.CheckCollision(transformComponent.position, moveTo.x, moveTo.y, collisionComponent.radis);
        }
        else
        {
            needTo = PhysicsRay.CheckCollision(transformComponent.position, moveTo.x, moveTo.y, collisionComponent.radis,LayerMask.GetMask("Enemy"));
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
