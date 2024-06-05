using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

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
    public bool interaction => inputActions.keyBoard.Interaction.WasPressedThisFrame();

    public bool isHold = false;

    public Vector2 movepos = new Vector2();
    public Vector2 facepos = new Vector2();


    //MoveComponent moveComponent;
    //TransformComponent transform;
    
    //WeaponComponent weapon;
    //SkillComponent skill;
    public override void Init()
    {
        //moveComponent = (MoveComponent)entity.GetComponent("MoveComponent");
        //transform = (TransformComponent)entity.GetComponent("TransformComponent");
        //weapon = (WeaponComponent)entity.GetComponent("WeaponComponent");
        //skill = (SkillComponent)entity.GetComponent("SkillComponent");

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
        //移动方向
        movepos = keyboardMoveAxes;

        //发射方向
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        facepos = Camera.main.ScreenToWorldPoint(mousePosition);

        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            PlayerBaseData.Instance.ChangePlayer(0);
        }
        else if(Input.GetKeyUp(KeyCode.Alpha2))
        {
            PlayerBaseData.Instance.ChangePlayer(1);
        }
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<InputComponent>(this);
    }
}
