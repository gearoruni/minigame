using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerComponent : Component{
    public InputComponent Input;

    public Vector2 movepos = new Vector2();
    public Vector2 facepos = new Vector2();

    public bool isFire;
    public bool isReload;
    public bool isRightSkill;
    public bool isQSkill ;
    public bool isESkill;
    public bool isTSkill;
    public bool isHold = false;


    public override void Init()
    {
        Input = (InputComponent)entity.GetComponent("InputComponent");
    }
    public override void Update()
    {
        if (CheckInput()) return;
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

    public override void OnCache()
    {
        CachePool.Instance.Cache<ControllerComponent>(this);
    }
}
