using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
    // public GameObject prefab;
    public int poolSize = 10;

    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string,Queue<GameObject>>();
    private Queue<GameObject> objectPool = new Queue<GameObject>();

    //åºŸå¼ƒ
    // public void Init(int count)
    // {
    //     // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ÐµÄ¶ï¿½ï¿½ï¿½
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
    public GameObject GetObjectFromPool()
    {
        // Èç¹û¶ÔÏó³ØÎª¿Õ£¬Ôò´´½¨ÐÂ¶ÔÏó²¢·µ»Ø
        if (objectPool.Count == 0)
        {
            GameObject newObj = Instantiate(prefab);
            return newObj;
        }

        // ´Ó¶ÔÏó³ØÖÐÈ¡³öÒ»¸ö¶ÔÏó²¢·µ»Ø
        GameObject obj = objectPool.Dequeue();
        
        obj.SetActive(true);
        return obj;
    }
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        if(!pools.TryGetValue(name, out var queue))
        {
            queue = new Queue<GameObject>();
            pools.Add(name, queue);
        }
        queue.Enqueue(obj);
    }
}
