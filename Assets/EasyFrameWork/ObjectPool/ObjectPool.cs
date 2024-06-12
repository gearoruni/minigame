using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
    public GameObject prefab;
    public int poolSize = 10;

    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string,Queue<GameObject>>();
    private Queue<GameObject> objectPool = new Queue<GameObject>();

    //废弃
    // public void Init(int count)
    // {
    //     // ����������еĶ���?
    //     for (int i = 0; i < count; i++)
    //     {
    //         GameObject obj = Instantiate(prefab, transform);
    //         obj.SetActive(false);
    //         objectPool.Enqueue(obj);
    //     }
    // }

    public GameObject GetObjectFromPool(string name, Vector3 transform,Quaternion rotation)
    {
        if(!pools.TryGetValue(name, out var queue))
        {
            queue = new Queue<GameObject>();
            pools.Add(name, queue);
        }
        if (queue.Count == 0)
        {
            var go = Preloader.Instance.GetGameObject(name);
            if(go != null)
                return GameObject.Instantiate(go);
            else
                return null;
        }

        GameObject obj = queue.Dequeue();
        obj.transform.position = transform;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }
    public GameObject GetObjectFromPool(string name)
    {
        if (!pools.TryGetValue(name, out var queue))
        {
            queue = new Queue<GameObject>();
            pools.Add(name, queue);
        }
        if (queue.Count == 0)
        {
            var go = Preloader.Instance.GetGameObject(name);
            if (go != null)
            {
                GameObject instance = GameObject.Instantiate(go);
                instance.transform.SetParent(transform);
                return instance;
            }
            else
                return null;
        }

        GameObject obj = queue.Dequeue();
        obj.SetActive(true);
        return obj;
    }
    public GameObject GetObjectFromPool()
    {
        if (objectPool.Count == 0)
        {
            GameObject newObj = Instantiate(prefab);
            return newObj;
        }

        GameObject obj = objectPool.Dequeue();
        obj.transform.SetParent(this.transform);
        obj.SetActive(true);
        return obj;
    }
    public void ReturnObjectToPool(string name, GameObject obj)
    {
        if(obj == null)return;
        obj.SetActive(false);
        if(!pools.TryGetValue(name, out var queue))
        {
            queue = new Queue<GameObject>();
            pools.Add(name, queue);
        }
        queue.Enqueue(obj);
    }
}
