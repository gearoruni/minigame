//using Cysharp.Threading.Tasks;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.Interactions;

//public class PlayerInputCtrl : BaseModel
//{
//    private PlayerInputAction inputActions;

//    public Vector2 keyboardMoveAxes => inputActions.keyBoard.MoveCtrl.ReadValue<Vector2>();
//    public bool isFire => inputActions.keyBoard.Fire.WasPressedThisFrame();
//    public bool isC1 => inputActions.keyBoard.Char1Changed.WasPressedThisFrame();
//    public bool isC2 => inputActions.keyBoard.Char2Changed.WasPressedThisFrame();
//    public bool isC3 => inputActions.keyBoard.Char3Changed.WasPressedThisFrame();
//    public bool isReload => inputActions.keyBoard.Reload.WasPressedThisFrame();

//    public bool isHold = false;

//    bool BaseModel.Init()
//    {
//        return true;
//    }

//    UniTask<bool> BaseModel.InitAysnc()
//    {
//        return UniTask.FromResult(true);
//    }

//    void BaseModel.OnFixUpdate()
//    {
        
//    }

//    void BaseModel.OnLateUpdate()
//    {
        
//    }

//    void BaseModel.OnStart()
//    {
//        inputActions = new PlayerInputAction();
//        inputActions.keyBoard.Enable();

//        inputActions.keyBoard.Fire.performed += ctx =>
//        {
//            if (ctx.interaction is HoldInteraction)
//            {
//                isHold = true;
//            }
//        };
//        inputActions.keyBoard.Fire.canceled += ctx =>
//        {
//            if (ctx.interaction is HoldInteraction)
//            {
//                isHold = false;
//            }
//        };
//    }

//    void BaseModel.OnUpdate()
//    {
//        //WASD�ƶ�
//        GameCore.Instance.GetModel<PlayerMoveCtrl>().PlayerMove(keyboardMoveAxes);

//        //Weapon�������
//        //���������Ļλ��ת��Ϊ����ռ�λ��
//        Vector3 mousePosition = Input.mousePosition;
//        mousePosition.z = -Camera.main.transform.position.z;
//        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

//        GameCore.Instance.GetModel<WeaponDirCtrl>().SetDir(targetPosition);

//        //���
//        if (isFire||isHold)
//        {
//            GameCore.Instance.GetModel<WeaponFireCtrl>().Fire();
//        }

//        //����
//        if (isReload)
//        {
//            GameCore.Instance.GetModel<WeaponFireCtrl>().WeaponRelaod();
//        }
//    }

//    void BaseModel.Release()
//    {
    
//    }
//}
