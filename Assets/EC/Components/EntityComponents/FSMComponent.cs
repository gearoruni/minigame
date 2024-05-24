using BehaviorDesigner.Runtime;
using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FSMComponent : Component
{
    BehaviorTree bt;

    public Vector2 movepos = new Vector2();
    public Vector2 facepos = new Vector2();

    public bool isFire = false;
    public bool isReload = false;
    public bool isRightSkill = false;
    public bool isQSkill = false;
    public bool isESkill = false;
    public bool isTSkill = false;
    TransformComponent transform;


    public override void Init()
    {
        transform = (TransformComponent)entity.GetComponent("TransformComponent");
    }
    public override void DataInit()
    {
        if (this.entity.go != null)
        {
            bt = this.entity.go.GetComponent<BehaviorTree>();
        }
        if (bt == null)
        {
            bt = this.entity.go.AddComponent<BehaviorTree>();
        }
        int dataDefine;
        if (entity.componentDatas.TryGetValue("FSMComponent", out dataDefine))
        {
            bt.ExternalBehavior = Resources.Load<ExternalBehaviorTree>(PathUtils.GetResAIController(dataDefine.ToString()));

#if UNITY_EDITOR
            bt.ExternalBehavior = AssetDatabase.LoadAssetAtPath<ExternalBehaviorTree>(PathUtils.GetAIController(dataDefine.ToString()));
#endif
            bt.StartWhenEnabled = true;
        }
        if (transform!=null)
        {
            movepos = transform.position;
            facepos = transform.GetPosDir(facepos);
        }
    }

    public override void Update()
    {
        if (bt == null) return;

        movepos = ((SharedVector2)bt.GetVariable("movepos")).Value;
        facepos = ((SharedVector2)bt.GetVariable("facepos")).Value;
        Debug.Log(movepos);
        isFire = ((SharedBool)bt.GetVariable("isFire")).Value;
        isReload = ((SharedBool)bt.GetVariable("isReload")).Value;
        isRightSkill = ((SharedBool)bt.GetVariable("isRightSkill")).Value;
        isQSkill = ((SharedBool)bt.GetVariable("isQSkill")).Value;
        isESkill = ((SharedBool)bt.GetVariable("isESkill")).Value;
        isTSkill = ((SharedBool)bt.GetVariable("isTSkill")).Value;

    }
    public override void OnCache()
    {

        CachePool.Instance.Cache<FSMComponent>(this);
    }
}
