using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameCore : MonoSingleton<GameCore>
{
    private Dictionary<Type, BaseModel> modelList = new Dictionary<Type, BaseModel>();
    private Dictionary<Type, BaseManager> managerList = new Dictionary<Type, BaseManager>();
    private bool isReady = false;
    #region 注册
    /// <summary>
    /// 注册一开始用到的model与manager
    /// </summary>
    private void Register()
    {
        
    }
    public void RegisterModel(BaseModel model)
    {
        modelList.Add(model.GetType(), model);
    }

    public void RegisterManager(BaseManager manager)
    {
        managerList.Add(manager.GetType(), manager);
    }
    #endregion
    
    #region 启动
    public async UniTask<bool> Active()
    {
        Register();
        Init();
        await InitAysnc();
        isReady = true;
        return true;
    }
    private void Init()
    {
        foreach(var model in modelList.Values)
        {
            model.Init();
        }
        foreach(var manager in managerList.Values)
        {
            manager.Init();
        }
    }
    private async UniTask<bool> InitAysnc()
    {
        foreach(var model in modelList.Values)
        {
            await model.InitAysnc();
        }
        foreach(var manager in managerList.Values)
        {
            await manager.InitAysnc();
        }
        return true;
    }
    #endregion

    #region 生命函数
    private void Start() {
        if(!isReady) return;
        foreach(var model in modelList.Values)
        {
            model.OnStart();
        }
    }
    
    private void Update() {
        if(!isReady) return;
        foreach(var model in modelList.Values)
        {
            model.OnUpdate();
        }
    }

    private void FixedUpdate() {
        if(!isReady) return;
        foreach(var model in modelList.Values)
        {
            model.OnFixUpdate();
        }
    }

    private void LateUpdate() {
        if(!isReady) return;
        foreach(var model in modelList.Values)
        {
            model.OnLateUpdate();
        }
    }
    #endregion

    #region 获取
    public T GetModel<T>() where T : BaseModel
    {
        if(modelList.TryGetValue(typeof(T), out var target))
        {
            return (T)target;
        }
        return default;
    }
    public T GetManager<T>() where T : BaseManager
    {
        if(managerList.TryGetValue(typeof(T), out var target))
        {
            return (T)target;
        }
        return default;
    }
    #endregion
}
