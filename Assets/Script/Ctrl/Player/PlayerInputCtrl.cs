using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputCtrl : BaseModel
{
    private PlayerInputAction inputActions;

    public Vector2 keyboardMoveAxes => inputActions.keyBoard.MoveCtrl.ReadValue<Vector2>();
    public bool isFire => inputActions.keyBoard.Fire.ReadValue<bool>();
    public bool isC1 => inputActions.keyBoard.Char1Changed.ReadValue<bool>();
    public bool isC2 => inputActions.keyBoard.Char2Changed.ReadValue<bool>();
    public bool isC3 => inputActions.keyBoard.Char3Changed.ReadValue<bool>();


    bool BaseModel.Init()
    {
        throw new System.NotImplementedException();
    }

    UniTask<bool> BaseModel.InitAysnc()
    {
        throw new System.NotImplementedException();
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
    }

    void BaseModel.OnUpdate()
    {
        if (keyboardMoveAxes != Vector2.zero)
        {
            
        }
    }

    void BaseModel.Release()
    {
    
    }
}
