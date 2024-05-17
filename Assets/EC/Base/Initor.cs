using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initor : MonoBehaviour
{
    private void Awake()
    {
        //ϵͳ��ع�����
        EventManager.Instance.Init();
        ObjectPoolManager.Instance.Init();
        TableDataManager.Instance.Init();
        ObjectPool.Instance.Init();
        CachePool.Instance.Init();
        SpawnManager.Instance.Init();
        EffectManager.Instance.Init();

        //������������ͳһ
        BehaviourCtrl.Instance.Init();

        //�����������ں�����Manager��BehaviourCtrl��ע��
        EntityManager.Instance.Init();
        TimerManager.Instance.Init();

        //�������������
        PlayerBaseData.Instance.Init();
    }
}
