using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;

public class TrackListener : MonoBehaviour
{
    public Entity entity;
    public float radius;
    public TrackEffect effect;
    public void Init(Entity entity, TrackEffect effect)
    {
        this.entity = entity;
        this.effect = effect;
        this.radius = effect.radius;
        CircleCollider2D c = this.gameObject.GetComponent<CircleCollider2D>();
        c.radius = effect.radius;
        c.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (effect == null ||effect.time != 0) return;
        Debug.Log("进入碰撞");

        CollisionListener collisionListener = collision.GetComponent<CollisionListener>();

        if (collisionListener != null)
        {
            Entity temp = collisionListener.entity;
            if (temp.Tag == entity.Tag || temp.go.layer == entity.go.layer || temp.Tag == Tag.Trap)
            {
                return;
            }
            TransformComponent transform = (TransformComponent)temp.GetComponent("TransformComponent");
            effect.target = transform;
            effect.time = 1;
            Debug.Log("进入追击");
        }

    }

    void OnDrawGizmos()
    {
        if (effect == null || effect.selTransform == null) return;
        Gizmos.DrawSphere(effect.selTransform.position, effect.radius);
    }
}
