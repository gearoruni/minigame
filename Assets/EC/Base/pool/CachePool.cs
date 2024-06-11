using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CachePool:Singleton<CachePool>
{

    public Dictionary<string, PoolData> pools;

    public override void Init()
    {
        pools = new Dictionary<string, PoolData>();
    }
    public T Get<T>() where T : new()
    {
        string typename = typeof(T).ToString();
        if (!pools.ContainsKey(typename))
        {
            pools[typename] = new PoolData(new T());
            //Debug.Log(typename + " : 出池 type1");
            return new T();
        }
        T obj = (T)pools[typename].GetObj();
        if (obj == null) obj = new T();
        //Debug.Log(typename + " : 出池 type2");
        return obj;
    }
    public T Get<T>(string name) where T :new()
    {
        string typename = name;
        if (!pools.ContainsKey(typename))
        {
            pools[typename] = new PoolData(new T());
            return new T();
        }
        T obj = (T)pools[typename].GetObj() ;
        if (obj == null) obj = new T();
        //Debug.Log(typename + " : 出池");
        return obj;
    }

    public void Cache<T>(PoolBaseClass obj, string name = null) where T : PoolBaseClass, new()
    {
        string typename = name != null ? name : typeof(T).ToString();
        obj.Cache();
        pools[typename].PushObj(obj);
        //Debug.Log(typename + " : 入池");
    }
}
