using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CanAtkTask : Conditional
{
    LayerMask layer = LayerMask.GetMask("Player");
    LayerMask baselayer = LayerMask.GetMask("Wall");
    public SharedBool isFire;

    public SharedFloat radius;

    public SharedEntity entity;

    public SharedEntity target;

    public SharedBool hasTarget;

    public float distance;
    public SharedVector2 facepos;
    public override void OnStart()
    {
        CharacterComponent character = (CharacterComponent)entity.Value.GetComponent("CharacterComponent");
        distance = character.range;
        radius.Value = distance;
    }
    bool canFire = false;
    public override TaskStatus OnUpdate()
    {

        #region ·ÏÆú
        //Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, radius.Value, layer);

        //foreach (var hitCollider in hitColliders)
        //{
        //    CollisionListener collisionListener = hitCollider.GetComponent<CollisionListener>();

        //    if (collisionListener != null)
        //    {
        //        Entity temp = collisionListener.entity;

        //        TransformComponent transform2 = (TransformComponent)temp.GetComponent("TransformComponent");

        //        Vector2 dir1 = this.transform.position - transform2.position;

        //        RaycastHit2D hit2 = Physics2D.Raycast(this.transform.position, dir1, dir1.magnitude, baselayer);

        //        if (hit2.collider != null)
        //        {
        //            return TaskStatus.Failure;
        //        }
        //        target.Value = temp;
        //    }
        //}
        #endregion

        if (!hasTarget.Value)
        {
            return TaskStatus.Failure;
        }

        isFire.Value = false;
        canFire = true;
        TransformComponent transform = (TransformComponent)target.Value.GetComponent("TransformComponent");
        if (transform == null)
        {
            return TaskStatus.Failure;
        }

        facepos.Value = transform.position;
        float td = Vector2.Distance( transform.position , this.transform.position);
        if (td > distance)
        {
            canFire = false;
            return TaskStatus.Failure;
        }
        Vector2 dir = this.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir, dir.magnitude, baselayer);
        if (hit.collider != null)
        {
            canFire = false;
            return TaskStatus.Failure;

        }
        return TaskStatus.Success;
    }
    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; 
        if (transform != null)
        {
            Gizmos.DrawWireSphere(transform.position, radius.Value); 
        }
    }
}
