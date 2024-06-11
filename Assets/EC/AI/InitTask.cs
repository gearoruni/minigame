using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitTask : Action
{
    public SharedEntity entity;
    public bool hasInit = false;

    public override void OnStart()
    {
        
    }

    public override TaskStatus OnUpdate()
    {
        if (hasInit) { return TaskStatus.Success; }
        CollisionListener collisionListener = this.gameObject.GetComponentInChildren<CollisionListener>();
        if (collisionListener != null)
        {
            entity.Value = collisionListener.entity;
            hasInit = true;
        }
        return TaskStatus.Success;
    }
}
