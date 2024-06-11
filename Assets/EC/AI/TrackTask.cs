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
//    private Path path; // ��ǰ·��
//    private int currentWaypoint = 0; // ��ǰ·��������
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
//                Debug.Log("û��·������������·��");
//                RequestPath();
//            }
//            return TaskStatus.Failure;
//        }

//        if (currentWaypoint >= path.vectorPath.Count)
//        {
//            Debug.Log("���·��");
//            return TaskStatus.Success;
//        }

//        // ��ȡ��ǰ·�����Ŀ��λ��
//        Vector3 targetPosition = path.vectorPath[currentWaypoint];
//        // �����ƶ�����
//        Vector3 direction = (targetPosition - this.transform.position).normalized;
//        Debug.Log(direction);

//        Debug.Log("����: " + direction);
//        movepos.Value = direction;

//        // ����Ƿ�ӽ���ǰ·����
//        if (Vector3.Distance(this.transform.position, targetPosition) < 0.1f)
//        {
//            // ���µ���һ��·����
//            currentWaypoint++;
//        }

//        // ����ﵽ·�����յ㣬���¼���·��
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
//            // ���·������ɹ�����洢·��������·��������
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
    private Path path; // ��ǰ·��
    private int currentWaypoint = 0; // ��ǰ·��������
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
