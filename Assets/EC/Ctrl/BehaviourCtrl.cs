using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BehaviourCtrl : MonoSingleton<BehaviourCtrl>
{
    public Action OnAwake;
    public Action CustomEnable;
    public Action OnStart;
    public Action OnUpdate;
    public Action OnFixUpdate;
    public Action OnLateUpdate;
    public Action CustomDisable;
    public Action CustomDestroy;

    private void Awake()
    {
        if (OnAwake != null) OnAwake();
    }

    private void OnEnable()
    {
        if(CustomEnable != null) CustomEnable();
    }

    void Start()
    {
        if (OnStart != null) OnStart();
    }
    void Update()
    {   
        if(OnUpdate != null) OnUpdate();
    }

    private void FixedUpdate()
    {
        if(OnFixUpdate != null) OnFixUpdate();
    }
    private void LateUpdate()
    {
        if(OnLateUpdate != null) OnLateUpdate();
    }
    private void OnDisable()
    {
        if(CustomDisable != null) CustomDisable();
    }
    private void OnDestroy()
    {
        if(CustomDestroy != null) CustomDestroy();
    }
}
