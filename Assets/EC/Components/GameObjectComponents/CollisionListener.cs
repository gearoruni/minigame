using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{
    public Entity entity;
    public TagComponent selfTag;
    private CollisionComponent collisionComponent;
    public void Init(Entity entity )
    {
        this.entity = entity;
        selfTag = (TagComponent)entity.GetComponent("TagComponent");
        collisionComponent = (CollisionComponent)entity.GetComponent("CollisionComponent");
        collisionComponent.SetListener(this);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    collisionComponent.OnBaseCollisionEnter2D?.Invoke();

    //    Entity collisionEntity;
    //    if(CheckCollisionEntity(collision, out collisionEntity))
    //    {
    //        collisionComponent.OnCollisionEnter2D?.Invoke(collisionEntity);
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    collisionComponent.OnBaseCollisionExit2D?.Invoke();

    //    Entity collisionEntity;
    //    if (CheckCollisionEntity(collision, out collisionEntity))
    //    {
    //        collisionComponent.OnCollisionExit2D?.Invoke(collisionEntity);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collisionComponent.needListen) return;

        Entity collisionEntity;
        bool hasEntity = CheckCollisionEntity(collision, out collisionEntity);

        if (!hasEntity)
        {
            collisionComponent.OnBaseTriggerEnter2D?.Invoke();
            return;
        }

        TagComponent collisionTag = (TagComponent)collisionEntity.GetComponent("TagComponent");
        if(collisionTag.tag != selfTag.tag &&  collisionTag.tag != selfTag.parent && collisionTag.parent != selfTag.parent)
        {
            

            HitComponent hit = (HitComponent)collisionEntity.GetComponent("HitComponent");
            if(hit != null)
            {
                hit.Invoke(entity);
            }

            collisionComponent.OnBaseTriggerEnter2D?.Invoke();
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    collisionComponent.OnBaseTriggerExit2D?.Invoke();

    //    Entity collisionEntity;
    //    if (CheckCollisionEntity(collision, out collisionEntity))
    //    {
    //        collisionComponent.OnTriggerExit2D?.Invoke(collisionEntity);
    //    }
    //}
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
}
