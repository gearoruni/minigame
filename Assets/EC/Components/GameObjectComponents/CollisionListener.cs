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
    /// 非子弹碰撞
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Entity collisionEntity;
        bool hasEntity = CheckCollisionEntity(collision, out collisionEntity);
        bool isListener = CheckListner(collision);
        if(isListener) { return; }
        if(CheckTrap(collision)){
            return;
            };
        //如果碰到建筑物了
        if (!hasEntity)
        {
            collisionComponent.OnBaseTriggerEnter2D?.Invoke(null); 
            return;
        }
        //否则自监听
        TagComponent collisionTag = (TagComponent)collisionEntity.GetComponent("TagComponent");
        if(collisionTag.tag != selfTag.tag)
        {
            collisionComponent.OnBaseTriggerEnter2D?.Invoke(collisionEntity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Entity collisionEntity;
        bool hasEntity = CheckCollisionEntity(collision, out collisionEntity);
        bool isListener = CheckListner(collision);
        if (isListener) { return; }
        //如果碰到建筑物了
        if (!hasEntity)
        {
            collisionComponent.OnBaseTriggerExit2D?.Invoke(null);
            return;
        }
        //否则自监听
        TagComponent collisionTag = (TagComponent)collisionEntity.GetComponent("TagComponent");
        if (collisionTag.tag != selfTag.tag)
        {
            collisionComponent.OnBaseTriggerExit2D?.Invoke(collisionEntity);
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

    private bool CheckTrap(Collider2D collision)
    {
        CollisionListener temp = collision.gameObject.GetComponent<CollisionListener>();
        if(temp == null)return false;
        
        Debug.Log($"{entity.entityId},{temp.entity.Tag}");
        if(entity.entityId == 3 && temp.entity.Tag == Tag.Trap)return true;
        return false;
    }
}
