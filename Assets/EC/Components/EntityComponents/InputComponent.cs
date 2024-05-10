using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class InputComponent : Component
{
    private PlayerInputAction inputActions;
    public Vector2 keyboardMoveAxes => inputActions.keyBoard.MoveCtrl.ReadValue<Vector2>();
    public bool isFire => inputActions.keyBoard.Fire.WasPressedThisFrame();
    public bool isC1 => inputActions.keyBoard.Char1Changed.WasPressedThisFrame();
    public bool isC2 => inputActions.keyBoard.Char2Changed.WasPressedThisFrame();
    public bool isC3 => inputActions.keyBoard.Char3Changed.WasPressedThisFrame();
    public bool isReload => inputActions.keyBoard.Reload.WasPressedThisFrame();
    public bool isRightSkill => inputActions.keyBoard.RightSkill.WasPressedThisFrame();
    public bool isQSkill => inputActions.keyBoard.QSkill.WasPressedThisFrame();
    public bool isESkill => inputActions.keyBoard.ESkill.WasPressedThisFrame();
    public bool isTSkill => inputActions.keyBoard.TSkill.WasPressedThisFrame();

    public bool isHold = false;

    MoveComponent moveComponent;
    TransformComponent transform;
    WeaponComponent weapon;
    SkillComponent skill;
    public override void Init()
    {
        moveComponent = (MoveComponent)entity.GetComponent("MoveComponent");
        transform = (TransformComponent)entity.GetComponent("TransformComponent");
        weapon = (WeaponComponent)entity.GetComponent("WeaponComponent");
        skill = (SkillComponent)entity.GetComponent("SkillComponent");

        inputActions = new PlayerInputAction();
        inputActions.keyBoard.Enable();

        inputActions.keyBoard.Fire.performed += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
            {
                isHold = true;
            }
        };
        inputActions.keyBoard.Fire.canceled += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
            {
                isHold = false;
            }
        };
    }
    public override void Update()
    {
        //移动
        moveComponent.input = keyboardMoveAxes;


        //发射方向
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);


        //动画面向
        int x = transform.GetPosDir(targetPosition);
        Debug.Log(x);

        weapon.SetWeaponTransform(targetPosition);

        if(!CheckSkill() && (isHold ||isFire))
        {
            skill.UseSkill(SkillType.NORMAL);
        }
        if (isReload)
        {
            weapon.Reload();
        }
    }

    public bool CheckSkill()
    {
        if(isRightSkill)
        {
            skill.UseSkill(SkillType.RIGHTATK);
            return true;
        }
        if (isQSkill)
        {
            skill.UseSkill(SkillType.QSKILL);
            return true;
        }
        if (isESkill)
        {
            skill.UseSkill(SkillType.ESKILL);
            return true;
        }
        if (isTSkill)
        {
            skill.UseSkill(SkillType.TSKILL);
            return true;
        }
        return false;
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<InputComponent>(this);
    }
}
