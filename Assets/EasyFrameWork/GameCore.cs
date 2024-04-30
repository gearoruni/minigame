using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameCore : MonoBehaviour
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
        foreach(var model in modelList)
        {
            model.Init();
        }
        foreach(var manager in managerList)
        {
            manager.Init();
        }
    }
    private async UniTask<bool> InitAysnc()
    {
        foreach(var model in modelList)
        {
            await model.InitAysnc();
        }
        foreach(var manager in managerList)
        {
            await manager.InitAysnc();
        }
        return true;
    }
    #endregion

    #region 生命函数
    private void Start() {
        if(!isReady) return;
        foreach(var model in modelList)
        {
            model.OnStart();
        }
    }
    
    private void Update() {
        if(!isReady) return;
        foreach(var model in modelList)
        {
            model.OnUpdate();
        }
    }

    private void FixedUpdate() {
        if(!isReady) return;
        foreach(var model in modelList)
        {
            model.OnFixUpdate();
        }
    }

    private void LateUpdate() {
        if(!isReady) return;
        foreach(var model in modelList)
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
    public T GetManager<T>() where T : BaseModel
    {
        if(managerList.TryGetValue(typeof(T), out var target))
        {
            return (T)target;
        }
        return default;
    }
    #endregion
}
