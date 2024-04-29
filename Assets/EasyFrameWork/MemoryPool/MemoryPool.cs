using System;
using System.Collections.Generic;

public static class MemoryPool
{
    private static Dictionary<Type, MemoryCollection> _memoryCollection = new Dictionary<Type, MemoryCollection>();

    public static T Acquire<T>() where T : BaseData, new()
    {
        T data = GetMemoryCollection(typeof(T)).Acquire<T>();
        data.isVaild = true;
        return data;
    }

    public static void Release(BaseData memory)
    {
        if (memory == null)
        {
            throw new Exception("Memory is invalid.");
        }
        memory.isVaild  = false;
        Type memoryType = memory.GetType();
        GetMemoryCollection(memoryType).Release(memory);
    }

        
    private static MemoryCollection GetMemoryCollection(Type memoryType)
    {
        if (memoryType == null)
        {
            throw new Exception("MemoryType is invalid.");
        }

        MemoryCollection memoryCollection = null;
        lock (_memoryCollection)
        {
            if (!_memoryCollection.TryGetValue(memoryType, out memoryCollection))
            {
                memoryCollection = new MemoryCollection(memoryType);
                _memoryCollection.Add(memoryType, memoryCollection);
            }
        }

        return memoryCollection;
    }

}
