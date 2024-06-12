using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class InteractiveComponent : Component
{
    /// <summary>
    /// 从0开始列举交互功能
    /// </summary>
    public enum NumToCallBack
    {
        greenIdolDialog = 0,//绿色区域神像
        caveIdolDialog = 1,//洞穴区域神像
        huixue = 2,
    }

    private bool isInteracting;
    private CollisionComponent collisionComponent;
    private GameObject interactiveGO;
    private NumToCallBack param;
    public override void Init()
    {
        collisionComponent = (CollisionComponent)entity.GetComponent("CollisionComponent");
        collisionComponent.OnBaseTriggerEnter2D += ChangeState;
        collisionComponent.OnBaseTriggerExit2D += Exit;
        interactiveGO = GameObject.Instantiate(Preloader.Instance.GetGameObject("9999"));
        interactiveGO.SetActive(false);
    }
    public override void DataInit()
    {
        if(entity.componentDatas.TryGetValue("InteractiveComponent", out int val))
        {
            param = (NumToCallBack)val;
        }
    }
    public override void OnCache()
    {
        base.OnCache();
        collisionComponent.OnBaseTriggerEnter2D -= ChangeState;
        collisionComponent.OnBaseTriggerExit2D -= Exit;
    }

    private void ChangeState(Entity entity)
    {
        if (entity == null || entity != PlayerBaseData.Instance.entity) return;
        isInteracting = true;
        
        interactiveGO.SetActive(true);
        interactiveGO.transform.position = this.entity.go.transform.position + new Vector3(0, 0.5f, 0); 
    }

    public override void Update()
    {
        if (isInteracting && Input.GetKeyDown(KeyCode.F))
            OnClick();
    }

    private void Exit(Entity entity)
    {
        isInteracting = false;
        interactiveGO.SetActive(false);
    }

    public void OnClick()
    {
        switch(param)
        {
            case NumToCallBack.greenIdolDialog:
                BattleUI.Instance?.ShowTxt(7008, OnClick0);
                break;
            case NumToCallBack.caveIdolDialog:
                BattleUI.Instance?.ShowTxt(7016, OnClick1);
                break;
            case NumToCallBack.huixue:
                var cmp = (CharacterDataComponent)PlayerBaseData.Instance.entity.GetComponent("CharacterDataComponent");
                cmp.nowHp += 100;
                EntityManager.Instance.RemoveEntity(entity.instanceId);
                break;
            default:
                break;
        }
    }

    private void OnClick0()
    {
        var cmp = (SkillComponent)PlayerBaseData.Instance.entity.GetComponent("SkillComponent");
        cmp.data[SkillType.ESKILL].isLock = false;
    }
    private void OnClick1()
    {
        var cmp = (SkillComponent)PlayerBaseData.Instance.entity.GetComponent("SkillComponent");
        cmp.data[SkillType.QSKILL].isLock = false;
    }
}
