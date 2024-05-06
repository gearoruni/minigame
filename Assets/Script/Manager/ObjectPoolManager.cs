using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    /// <summary>
    /// key: prefab id
    /// value: prefab pool
    /// </summary>
    [ShowInInspector]
    public Dictionary<int,ObjectPool> objectPoolDir = new Dictionary<int,ObjectPool>();

    public int minPoolSize = 5;

    /// <summary>
    /// ��ȡprefabʵ��
    /// </summary>    
    /// /// <param name="id">id</param>
    /// <param name="prefab">����prefab</param>
    /// <param name="cnt">��ʼ���Ӵ�С</param>
    public GameObject GetPrefabInstance(int id,GameObject prefab = null)
    {
        ObjectPool objectPool;
        if(!objectPoolDir.TryGetValue(id, out objectPool))
        {
            GameObject basePool = new GameObject(id.ToString() + " prefab pool");
            basePool.transform.parent = transform;
            
            objectPool = basePool.AddComponent<ObjectPool>();
            objectPool.prefab = prefab;

            objectPoolDir.Add(id, objectPool);
        }

        return objectPool.GetObjectFromPool(id.ToString());
    }

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="id"></param>
    /// <param name="obj"></param>
    public void ReturnObjectToPool(int id, GameObject obj)
    {
        ObjectPool objectPool;
        if (objectPoolDir.TryGetValue(id, out objectPool))
        {
            objectPoolDir[id].ReturnObjectToPool(id.ToString(),obj);
        }
        else
        {
            Destroy(obj);
            Debug.LogError("�Ƿ�Prefab����,����GameObject���٣�");
        }
    }

}
