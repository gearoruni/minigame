using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTask : Action
{
    public float moveRadius = 1f;
    private Vector2 targetPosition;
    public SharedVector2 movepos;
    float distance;
    public override void OnStart()
    {
        Vector2 randomDirection = Random.insideUnitCircle * moveRadius;
        targetPosition = (Vector2)transform.position + randomDirection;
        movepos.Value = randomDirection;
        distance = Vector2.Distance(transform.position, targetPosition);
    }

    public override TaskStatus OnUpdate()
    {
        
        if (Vector2.Distance(transform.position, targetPosition) > distance)
        {
            movepos.Value = Vector2.zero;
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
    public override void OnEnd()
    {
        movepos.Value = Vector2.zero;
    }
}
