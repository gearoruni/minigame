using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction
{
    /// <summary>
    /// transform移动
    /// 所有使用deltatime移动的使用这个
    /// </summary>
    /// <param name="go"></param>
    /// <param name="speed"></param>
    /// <param name="direction"></param>
    public static void MoveGo(GameObject go, float speed,Vector2 direction)
    {
        go.transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    /// <summary>
    /// 刚体移动
    /// 不使用deltatime
    /// </summary>
    /// <param name="rb"></param>
    /// <param name="speed"></param>
    /// <param name="direction"></param>
    public static Vector2 MoveRb(Rigidbody2D rb, float speed, Vector2 direction)
    {
        rb.velocity  = direction.normalized * speed;
        return rb.transform.position;
    }

}
