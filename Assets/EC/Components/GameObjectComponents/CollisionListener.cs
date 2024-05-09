using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{
    public Entity entity;
    private CollisionComponent collisionComponent;
    public void Init(Entity entity )
    {
        this.entity = entity;
        collisionComponent = (CollisionComponent)entity.GetComponent("CollisionComponent");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionComponent.OnBaseCollisionEnter2D?.Invoke();

        Entity collisionEntity;
        if(CheckCollisionEntity(collision, out collisionEntity))
        {
            collisionComponent.OnCollisionEnter2D?.Invoke(collisionEntity);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionComponent.OnBaseCollisionExit2D?.Invoke();

        Entity collisionEntity;
        if (CheckCollisionEntity(collision, out collisionEntity))
        {
            collisionComponent.OnCollisionExit2D?.Invoke(collisionEntity);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionComponent.OnBaseTriggerEnter2D?.Invoke();

        Entity collisionEntity;
        if (CheckCollisionEntity(collision, out collisionEntity))
        {
            collisionComponent.OnTriggerEnter2D?.Invoke(collisionEntity);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionComponent.OnBaseTriggerExit2D?.Invoke();

        Entity collisionEntity;
        if (CheckCollisionEntity(collision, out collisionEntity))
        {
            collisionComponent.OnTriggerExit2D?.Invoke(collisionEntity);
        }
    }
    private bool CheckCollisionEntity(Collider2D collision, out Entity entity)
    {
        entity = null;
        CollisionListener temp = collision.gameObject.GetComponent<CollisionListener>();
        if (temp)
        {
            entity = temp.entity;
            return true;
        }
        return false;
    }
    private bool CheckCollisionEntity(Collision2D collision,out Entity entity)
    {
        entity = null;
        CollisionListener temp = collision.gameObject.GetComponent<CollisionListener>();
        if (temp)
        {
            entity = temp.entity;
            return true;
        }
        return false;
    }
}
