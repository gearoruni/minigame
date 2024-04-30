using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputCtrl : BaseModel
{
    private PlayerInputAction inputActions;

    public Vector2 keyboardMoveAxes => inputActions.keyBoard.MoveCtrl.ReadValue<Vector2>();
    public bool isFire => inputActions.keyBoard.Fire.WasPressedThisFrame();
    public bool isC1 => inputActions.keyBoard.Char1Changed.WasPressedThisFrame();
    public bool isC2 => inputActions.keyBoard.Char2Changed.WasPressedThisFrame();
    public bool isC3 => inputActions.keyBoard.Char3Changed.WasPressedThisFrame();
    public bool isReload => inputActions.keyBoard.Reload.WasPressedThisFrame();

    public bool isHold = false;

    bool BaseModel.Init()
    {
        return true;
    }

    UniTask<bool> BaseModel.InitAysnc()
    {
        return UniTask.FromResult(true);
    }

    void BaseModel.OnFixUpdate()
    {
        
    }

    void BaseModel.OnLateUpdate()
    {
        
    }

    void BaseModel.OnStart()
    {
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

    void BaseModel.OnUpdate()
    {
        //WASD移动
        GameCore.Instance.GetModel<PlayerMoveCtrl>().PlayerMove(keyboardMoveAxes);

        //Weapon面向鼠标
        //并将鼠标屏幕位置转换为世界空间位置
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        GameCore.Instance.GetModel<WeaponDirCtrl>().SetDir(targetPosition);

        //射击
        if (isFire||isHold)
        {
            GameCore.Instance.GetModel<WeaponFireCtrl>().Fire();
        }

        //换弹
        if (isReload)
        {
            GameCore.Instance.GetModel<WeaponFireCtrl>().WeaponRelaod();
        }
    }

    void BaseModel.Release()
    {
    
    }
}
