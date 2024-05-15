using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class SearchTask : Conditional
{
    LayerMask layer = LayerMask.GetMask("Player");
    LayerMask baselayer = LayerMask.GetMask("Wall");

    public SharedFloat radius;

    public SharedEntity entity;

    public SharedEntity target;
    public SharedBool hasTarget;
    public override TaskStatus OnUpdate()
    {
        if (hasTarget.Value)
        {
            return TaskStatus.Success;
        }

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius.Value, layer);

        foreach (var hitCollider in hitColliders)
        {
            CollisionListener collisionListener = hitCollider.GetComponent<CollisionListener>();

            if (collisionListener != null)
            {
                Entity temp = collisionListener.entity;

                TransformComponent transform = (TransformComponent)temp.GetComponent("TransformComponent");

                Vector2 dir = this.transform.position - transform.position;

                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir, dir.magnitude, baselayer);

                if (hit.collider != null)
                {
                    return TaskStatus.Failure;
                }
                target.Value = temp;
                hasTarget.Value = true;
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  // 设置Gizmos颜色为红色
        if (transform != null)
        {
            Gizmos.DrawWireSphere(transform.position, radius.Value);  // 绘制线框球体
        }
        if (hasTarget.Value)
        {
            TransformComponent transform = (TransformComponent)target.Value.GetComponent("TransformComponent");
            Gizmos.DrawLine(this.transform.position, transform.position);
        }
    }
}
