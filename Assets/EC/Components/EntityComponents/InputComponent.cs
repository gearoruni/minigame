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

    public bool isHold = false;

    MoveComponent moveComponent;

    public override void Init()
    {
        moveComponent = (MoveComponent)entity.GetComponent("MoveComponent");

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
        moveComponent.input = keyboardMoveAxes;
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<InputComponent>(this);
    }
}
