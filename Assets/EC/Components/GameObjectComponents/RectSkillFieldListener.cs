using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RectSkillFieldListener : MonoBehaviour
{
    public Entity entity;
    
    public AreaConfigs areaConfigs;

    public void Init(AreaConfigs areaConfigs)
    {
        this.areaConfigs = areaConfigs;
        BoxCollider2D collider =  this.gameObject.GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(areaConfigs.Define1,areaConfigs.Define2);
    }
    public void SetPosition(Vector2 pos)
    {
        this.gameObject.transform.position = pos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionListener collisionListener = collision.GetComponent<CollisionListener>();

        if (collisionListener != null)
        {
            Entity temp = collisionListener.entity;
            if (temp.Tag == entity.Tag || temp.go.layer == entity.go.layer)
            {
                return;
            }
            TransformComponent transform = (TransformComponent)temp.GetComponent("TransformComponent");

        }

    }
}
