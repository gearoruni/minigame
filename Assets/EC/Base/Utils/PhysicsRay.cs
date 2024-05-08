using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PhysicsRay : MonoBehaviour
{
    static LayerMask baseMask = LayerMask.GetMask("Wall");
    public static Vector2 CheckCollision(Vector3 position,float x,float y,float radis,LayerMask layerMask = default(LayerMask))
    {
        Vector2 xdirection = new Vector2(x, 0);
        Vector2 ydirection = new Vector2(0, y);

        float xdisctance = xdirection.magnitude;
        float ydisctance = ydirection.magnitude;

        Vector2 res = new Vector2();

        RaycastHit2D xhit = Physics2D.Raycast(position, xdirection, xdisctance + radis, baseMask | layerMask);
        if (xhit.collider != null)
            res.x = position.x;
        else
            res.x = position.x + xdirection.normalized.x * xdisctance;

        RaycastHit2D yhit = Physics2D.Raycast(position, ydirection, ydisctance + radis, baseMask | layerMask);
        if (yhit.collider != null)
            res.y = position.y;
        else
            res.y = position.y + ydirection.normalized.y * ydisctance;

        return res;
    }
}
