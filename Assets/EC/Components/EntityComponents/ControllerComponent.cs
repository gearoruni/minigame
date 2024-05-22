using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerComponent : Component{
    public InputComponent Input;
    public FSMComponent FSM;
    public Vector2 movepos = new Vector2();
    public Vector2 facepos = new Vector2();

    public bool isFire;
    public bool isReload;
    public bool isRightSkill;
    public bool isQSkill ;
    public bool isESkill;
    public bool isTSkill;
    public bool isHold = false;

    public bool isActive = true;
    public override void Init()
    {
        Input = (InputComponent)entity.GetComponent("InputComponent");
        FSM = (FSMComponent)entity.GetComponent("FSMComponent");
    }
    public override void Update()
    {
        if (!isActive) return;
        if (CheckInput()) return;
        if (CheckFSM()) return;
    }

    public bool CheckInput()
    {
        if(Input == null)return false;
        movepos = Input.movepos;
        facepos = Input.facepos;
        isFire = Input.isFire;
        isReload = Input.isReload;
        isRightSkill = Input.isRightSkill;
        isQSkill = Input.isQSkill;
        isESkill = Input.isESkill;
        isTSkill = Input.isTSkill;
        isHold = Input.isHold;

        return true;
    }
    public bool CheckFSM()
    {
        if (FSM == null) return false;
        movepos = FSM.movepos;
        facepos = FSM.facepos;
        isFire = FSM.isFire;
        isReload = FSM.isReload;
        isRightSkill = FSM.isRightSkill;
        isQSkill = FSM.isQSkill;
        isESkill = FSM.isESkill;
        isTSkill = FSM.isTSkill;

        return true;
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<ControllerComponent>(this);
    }
}
