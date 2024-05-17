using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirChangeEffect : EffectBase
{
    public DirChange dirChange;
    public float selfSpeeds ;
    public float targetSpeeds;
    public float selfTime;
    public float targetTime;
    public float  targetDir;
    public float selfDir;

    public bool haveSet = false;
    public Vector2 dir;
    public override void Init(int define, Entity entity)
    {
        this.entity = entity;
        dirChange = TableDataManager.Instance.tables.DirEffectDefine.Get(define);
        for(int i = 0;i<dirChange.Target.Count;i++)
        {
            if (dirChange.Target[i] == 1)
            {
                selfSpeeds=dirChange.Speed[i];
                selfTime=dirChange.Time[i];
                selfDir = dirChange.Dir[i];
            }
            else
            {
                targetSpeeds=dirChange.Speed[i]; 
                targetTime=dirChange.Time[i];
                targetDir=dirChange.Dir[i];
            }
        }
        dir = ((TransformComponent)this.entity.GetComponent("TransformComponent")).faceDir.normalized;
    }
    public override void Invoke(Entity entity)
    {
        dir = ((TransformComponent)this.entity.GetComponent("TransformComponent")).faceDir.normalized;
        if (haveSet) { return; }
        Entity parent = EntityManager.Instance.GetEntityFromInstanceId(this.entity.parentId);
        if(parent != null)
        {
            MoveComponent move = (MoveComponent)parent.GetComponent("MoveComponent");
            dir = ((TransformComponent)parent.GetComponent("TransformComponent")).faceDir.normalized;
            if (entity == null || entity == this.entity)
            {
                dir *= selfDir;
                move.SetForceMove(this.selfTime, this.selfSpeeds, this.dir);
            }
            haveSet = true;
        }

    }
    public override void OnCache()
    {

    }
}
