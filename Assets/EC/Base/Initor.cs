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
        //ϵͳ��ع�����
        EventManager.Instance.Init();
        ObjectPoolManager.Instance.Init();
        TableDataManager.Instance.Init();
        ObjectPool.Instance.Init();
        CachePool.Instance.Init();
        SpawnManager.Instance.Init();
        EffectManager.Instance.Init();
        SoundManager.Instance.Init();

        //������������ͳһ
        BehaviourCtrl.Instance.Init();

        //�����������ں�����Manager��BehaviourCtrl��ע��
        EntityManager.Instance.Init();
        TimerManager.Instance.Init();

        //�������������
        PlayerBaseData.Instance.Init();
    }
}
