using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PhysicsRay : MonoBehaviour
{
    static LayerMask baseMask = LayerMask.GetMask("Wall");
    
    public static Vector2 CheckCollision(Vector3 position, float x, float y, float radis, LayerMask layerMask = default(LayerMask))
    {
        Vector2 direction = new Vector2(x, y);

        float disctance = direction.magnitude;
        

        Vector2 res = new Vector2();

        RaycastHit2D hit = Physics2D.Raycast(position, direction, disctance + radis, baseMask | layerMask);
        if (hit.collider != null)
        {
            Vector2 normal = hit.normal.normalized;

            Vector3 projectForwardDirection = Vector3.ProjectOnPlane(-normal, Vector3.forward);
            Vector3 projectLeftDirection = Vector3.ProjectOnPlane(-normal, Vector3.left);

            Vector3 subForwardEnd = Vector2.Dot(hit.point - (Vector2)position, projectForwardDirection) * projectForwardDirection;
            Vector3 subLeftEnd = Vector2.Dot(hit.point - (Vector2)position, projectLeftDirection) * projectLeftDirection;

            position = (Vector2)position - (Vector2)(subForwardEnd + subLeftEnd).normalized * disctance / 2;
            res = position;
        }
        else
        {
            res = (Vector2)position + direction.normalized * disctance;
        }

        return res;
    }

    public static Collider2D GetBindBox(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector3.up,0.1f, LayerMask.GetMask("BindBox"));
        return hit.collider;

    }
}
