
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkTask : Action
{
    public SharedBool isFire;//,isTanfan,isChongzhuang;
    public SharedEntity target;
    public SharedVector2 facepos;
    public SharedVector2 movepos;

    public SharedBool hasTarget;

    public override TaskStatus OnUpdate()
    {
        if (!hasTarget.Value)
        {
            return TaskStatus.Failure;
        }
        TransformComponent transform = (TransformComponent)target.Value.GetComponent("TransformComponent");
        if (transform == null)
        {
            return TaskStatus.Failure;
        }
        // foreach(var entity in EntityManager.Instance.entities.Values)
		// {
		// 	if(entity == null)continue;
		// 	if(entity.entityId==3 && entity.Tag == Tag.Player)
		// 	{
		// 		isTanfan.Value = true;
		// 		break;
		// 	}
		// }
        facepos.Value = transform.position;
        isFire.Value = true;
		// isChongzhuang.Value = true;
        movepos.Value = Vector2.zero;
        return TaskStatus.Success;
    }

}
