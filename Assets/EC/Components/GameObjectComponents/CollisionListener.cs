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
        this.gameObject.tag = entity.go.tag;
        this.gameObject.layer = entity.go.layer;
        collisionComponent = (CollisionComponent)entity.GetComponent("CollisionComponent");
        collisionComponent.SetListener(this);
    }

    /// <summary>
    /// ·Ç×Óµ¯Åö×²
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collisionComponent.needListen) return;

        Entity collisionEntity;
        bool hasEntity = CheckCollisionEntity(collision, out collisionEntity);
        bool isListener = CheckListner(collision);
        if(isListener) { return; }
        if (!hasEntity)
        {

            collisionComponent.OnBaseTriggerEnter2D?.Invoke(); 

            return;
        }

        BulletComponent bulletComponent = (BulletComponent)collisionEntity.GetComponent("BulletComponent");
        TagComponent collisionTag = (TagComponent)collisionEntity.GetComponent("TagComponent");
        if(collisionTag.tag != selfTag.tag)
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
    private bool CheckListner(Collider2D collision)
    {
        TrackListener temp = collision.gameObject.GetComponent<TrackListener>();
        if(temp == null)
        {
            temp = collision.gameObject.GetComponentInChildren<TrackListener>();
        }

        if (temp)
        {
            return true;
        }
        return false;
    }
}
