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
