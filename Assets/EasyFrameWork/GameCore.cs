using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    private List<BaseModel> modelList = new List<BaseModel>();
    private List<BaseManager> managerList = new List<BaseManager>();
    #region 注册
    /// <summary>
    /// 注册一开始用到的model与manager
    /// </summary>
    private void Register()
    {
        
    }
    public void RegisterModel(BaseModel model)
    {
        modelList.Add(model);
    }

    public void RegisterManager(BaseManager manager)
    {
        managerList.Add(manager);
    }
    #endregion
    
    #region 启动
    public async UniTask<bool> Active()
    {
        Init();
        return await InitAysnc();
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
        foreach(var model in modelList)
        {
            model.OnStart();
        }
    }
    
    private void Update() {
        foreach(var model in modelList)
        {
            model.OnUpdate();
        }
    }

    private void FixedUpdate() {
        foreach(var model in modelList)
        {
            model.OnFixUpdate();
        }
    }

    private void LateUpdate() {
        foreach(var model in modelList)
        {
            model.OnLateUpdate();
        }
    }
    #endregion
}
