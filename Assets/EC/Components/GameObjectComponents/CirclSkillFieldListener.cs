using cfg;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CircleSkillFieldListener
{
    public Entity entity;
    public TransformComponent transformComponent;
    public AreaConfigs areaConfigs;
    public Vector2 pos;
    public List<Entity> targets;
    public void Init(AreaConfigs areaConfigs, Entity entity,Vector2 pos,List<Entity> targets)
    {
        this.entity = entity;
        this.areaConfigs = areaConfigs;
        transformComponent = (TransformComponent)entity.GetComponent("TransformComponent");
        this.pos = (Vector2)transformComponent.position + pos;
        this.targets = targets;
    }
    public void CheckColliders(LayerMask baseMask = default(LayerMask))
    {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, areaConfigs.Define1, LayerMask.GetMask("Player") | LayerMask.GetMask("Enemy")|baseMask);

        foreach(var  hitCollider in hitColliders)
        {
            CollisionListener collisionListener = hitCollider.GetComponent<CollisionListener>();

            if (collisionListener != null)
            {
                Entity temp = collisionListener.entity;
                if (temp.Tag == entity.Tag || temp.go.layer == entity.go.layer)
                {
                    continue;
                }
                TransformComponent transform = (TransformComponent)temp.GetComponent("TransformComponent");

                Vector2 baseDir =  ((TransformComponent)entity.GetComponent("TransformComponent")).faceDir.normalized;

                Vector2 checkDir = (transform.position - transformComponent.position).normalized;

                float x = Vector2.Dot(baseDir, checkDir);
                float t = math.acos(x) * 180 / math.PI;
                if (t <= areaConfigs.Define2)
                {
                    targets.Add(temp);
                }
            }
        }

    }
    
}
