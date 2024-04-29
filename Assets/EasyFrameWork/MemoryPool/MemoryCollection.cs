using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCollection
{
    
    private readonly Stack<BaseData> _memoryStack;
    private readonly Type _memoyType;

    private int _usingCount;
    private int _addCount;

    public MemoryCollection(Type memoryType)
    {
        _memoryStack = new Stack<BaseData>();
        _memoyType = memoryType;
        _usingCount = 0;
        _addCount = 0;
    }

    public T Acquire<T>() where T : BaseData, new()
    {
        if (typeof(T) != _memoyType)
        {
            throw new Exception("Type is invalid.");
        }

        _usingCount++;
        lock(_memoryStack)
        {
            if (_memoryStack.Count > 0)
            {
                return (T)_memoryStack.Pop();
            }
        }

        _addCount++;
        return new T();
   }

    public void Release(BaseData memory)
    {

        _usingCount--;
        lock(_memoryStack)
        {
            _memoryStack.Push(memory);
        }
    }
}