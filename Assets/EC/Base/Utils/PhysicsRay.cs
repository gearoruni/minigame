using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PhysicsRay : MonoBehaviour
{
    static LayerMask baseMask = LayerMask.GetMask("Wall");

    //public static Vector2 CheckCollision(Vector3 position, float x, float y, float radis, GameObject go,LayerMask layerMask = default(LayerMask))
    //{
    //    Vector2 direction = new Vector2(x, y);

    //    float disctance = direction.magnitude;


    //    Vector2 res = new Vector2();

    //    RaycastHit2D hit = Physics2D.Raycast(position, direction, disctance + radis, baseMask | layerMask);
    //    if (hit.collider != null && hit.collider.gameObject != go)
    //    {
    //        Vector2 normal = hit.normal.normalized;

    //        Vector3 projectForwardDirection = Vector3.ProjectOnPlane(-normal, Vector3.forward);
    //        Vector3 projectLeftDirection = Vector3.ProjectOnPlane(-normal, Vector3.left);

    //        Vector3 subForwardEnd = Vector2.Dot(hit.point - (Vector2)position, projectForwardDirection) * projectForwardDirection;
    //        Vector3 subLeftEnd = Vector2.Dot(hit.point - (Vector2)position, projectLeftDirection) * projectLeftDirection;

    //        position = (Vector2)position - (Vector2)(subForwardEnd + subLeftEnd).normalized * disctance / 2;
    //        res = position;
    //    }
    //    else
    //    {
    //        res = (Vector2)position + direction.normalized * disctance;
    //    }

    //    return res;
    //}
    public static Vector2 CheckCollision(Vector3 position, float x, float y, float radius, Entity entity, LayerMask layerMask = default(LayerMask))
    {
        Vector2 direction = new Vector2(x, y);
        float distance = direction.magnitude;
        Vector2 res = Vector2.zero;

        // 发射射线并获取所有碰撞体信息
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, direction, distance + radius, baseMask | layerMask);

        // 遍历所有检测到的碰撞体
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                CollisionListener collision = hit.collider.gameObject.GetComponent<CollisionListener>();
                if (collision != null)
                {
                    if (collision.entity == entity) continue;
                }

                Vector2 normal = hit.normal.normalized;

                Vector3 projectForwardDirection = Vector3.ProjectOnPlane(-normal, Vector3.forward);
                Vector3 projectLeftDirection = Vector3.ProjectOnPlane(-normal, Vector3.left);

                Vector3 subForwardEnd = Vector2.Dot(hit.point - (Vector2)position, projectForwardDirection) * projectForwardDirection;
                Vector3 subLeftEnd = Vector2.Dot(hit.point - (Vector2)position, projectLeftDirection) * projectLeftDirection;

                position = (Vector2)position - (Vector2)(subForwardEnd + subLeftEnd).normalized * distance  / 2;
                res = position;
                break; // 只处理第一个有效的碰撞体
            }
        }

        // 如果没有碰撞体，返回一个未碰撞的方向
        if (res == Vector2.zero)
        {
            res = (Vector2)position + direction.normalized * distance;
        }

        return res;
    }

    public static Collider2D GetBindBox(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector3.up,0.1f, LayerMask.GetMask("BindBox"));
        return hit.collider;

    }

    public static bool GetWall(Vector3 position, Vector3 dir,out Vector3 result)
    {
        float distance;
        int times= 0 ;
        result = Vector3.zero;
        while(times<3)
        {
            distance = Random.Range(1, 6);
            RaycastHit2D hit = Physics2D.Raycast(position, dir,distance, LayerMask.GetMask("Wall"));
            if(hit.collider==null)
            {
                result = position + dir * distance;
                return true;
            }
            times++;
        }
        return false;
    }
}
