using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Initor : MonoBehaviour
{
    private void Awake()
    {
        Preloader.Instance.Init();

        

        Restart();
        UIManager.Instance.Init();
    }

    private void Start()
    {
        UIManager.Instance.ShowUI("MainUI");
    }

    public void Restart()
    {
        //系统相关管理器
        EventManager.Instance.Init();
        ObjectPoolManager.Instance.Init();
        TableDataManager.Instance.Init();
        ObjectPool.Instance.Init();
        CachePool.Instance.Init();
        SpawnManager.Instance.Init();
        EffectManager.Instance.Init();
        SoundManager.Instance.Init();

        //基础生命周期统一
        BehaviourCtrl.Instance.Init();

        //依赖生命周期函数的Manager在BehaviourCtrl后注册
        EntityManager.Instance.Init();
        TimerManager.Instance.Init();

        //玩家数据最后更新
        PlayerBaseData.Instance.Init();
    }
}
