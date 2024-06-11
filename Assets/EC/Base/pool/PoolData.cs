using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolData
{

    public Stack<object> pool = new Stack<object>();
    public PoolData(object obj)
    {
        PushObj(obj);
    }

    public void PushObj(object obj)
    {
        pool.Push(obj);
    }
    public object GetObj()
    {
        if(pool.Count == 0)return null;
        return pool.Pop();
    }
}
