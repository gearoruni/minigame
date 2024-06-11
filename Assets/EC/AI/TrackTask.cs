//using BehaviorDesigner.Runtime;
//using BehaviorDesigner.Runtime.Tasks;
//using Pathfinding;
//using UnityEngine;

//public class TrackTask : Action
//{
//    public SharedEntity target;
//    public SharedVector2 facepos;
//    public SharedVector2 movepos;
//    private Seeker ai;
//    private Path path; // 当前路径
//    private int currentWaypoint = 0; // 当前路径点索引
//    private TransformComponent transformC;
//    public SharedBool hasTarget;
//    private bool isPathPending = false;

//    public override void OnStart()
//    {
//        ai = gameObject.GetComponent<Seeker>();
//        if (ai == null)
//        {
//            ai = gameObject.AddComponent<Seeker>();
//        }

//        if (hasTarget.Value)
//        {
//            transformC = (TransformComponent)target.Value.GetComponent("TransformComponent");
//            RequestPath();
//        }
//    }

//    public override TaskStatus OnUpdate()
//    {
//        if (!hasTarget.Value)
//        {
//            return TaskStatus.Failure;
//        }

//        facepos.Value = transformC.position;

//        if (path == null || isPathPending)
//        {
//            if (!isPathPending)
//            {
//                Debug.Log("没有路径，重新请求路径");
//                RequestPath();
//            }
//            return TaskStatus.Failure;
//        }

//        if (currentWaypoint >= path.vectorPath.Count)
//        {
//            Debug.Log("完成路径");
//            return TaskStatus.Success;
//        }

//        // 获取当前路径点的目标位置
//        Vector3 targetPosition = path.vectorPath[currentWaypoint];
//        // 计算移动方向
//        Vector3 direction = (targetPosition - this.transform.position).normalized;
//        Debug.Log(direction);

//        Debug.Log("方向: " + direction);
//        movepos.Value = direction;

//        // 检查是否接近当前路径点
//        if (Vector3.Distance(this.transform.position, targetPosition) < 0.1f)
//        {
//            // 更新到下一个路径点
//            currentWaypoint++;
//        }

//        // 如果达到路径的终点，重新计算路径
//        if (currentWaypoint >= path.vectorPath.Count)
//        {
//            RequestPath();
//        }

//        return TaskStatus.Running;
//    }

//    void OnPathComplete(Path p)
//    {
//        if (!p.error)
//        {
//            // 如果路径计算成功，则存储路径并重置路径点索引
//            path = p;
//            currentWaypoint = 0;
//        }
//        isPathPending = false;
//    }

//    private void RequestPath()
//    {
//        if (!hasTarget.Value)
//        {
//            return;
//        }

//        isPathPending = true;
//        transformC = (TransformComponent)target.Value.GetComponent("TransformComponent");
//        ai.StartPath(this.transform.position, transformC.position, OnPathComplete);
//    }
//}
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Pathfinding;
using UnityEngine;

public class TrackTask : Action
{
    public SharedEntity target;
    public SharedVector2 facepos;
    public SharedVector2 movepos;
    private Seeker ai;
    private Path path; // 当前路径
    private int currentWaypoint = 0; // 当前路径点索引
    private TransformComponent transformC;
    public SharedBool hasTarget;
    private bool isPathPending = false;

    public override void OnStart()
    {
        ai = gameObject.GetComponent<Seeker>();
        if (ai == null)
        {
            ai = gameObject.AddComponent<Seeker>();
        }

        if (hasTarget.Value)
        {
            transformC = (TransformComponent)target.Value.GetComponent("TransformComponent");
        }
    }

    public override TaskStatus OnUpdate()
    {
        if(!hasTarget.Value)
        {
            return TaskStatus.Failure;
        }
        if(target != null)
        {
            transformC = (TransformComponent)target.Value.GetComponent("TransformComponent");
            if(transformC == null || this.gameObject.transform == null)return TaskStatus.Failure;
            Vector3 dir = transformC.position - this.gameObject.transform.position;
            movepos.Value = (Vector2)dir;

            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }

}
