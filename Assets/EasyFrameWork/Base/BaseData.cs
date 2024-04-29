using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BaseData
{
    public bool isVaild { get; set; }
    private FieldInfo[] _fieldInfos;
    public void Clear()
    {
        var FieldInfos = GetFieldInfo();
            foreach(var fieldInfo in FieldInfos)
            {
                var ins = fieldInfo.GetValue(this);
                if(ins is BaseData data)
                {
                    data.Clear();
                }
                else if(ins is IEnumerable<BaseData> datas)
                {
                    foreach(var d in datas)
                    {
                        d.Clear();
                    }
                }
                else if(ins is IEnumerable<KeyValuePair<object,BaseData>> dict)
                {
                    foreach(var dd in dict)
                    {
                        dd.Value?.Clear();
                    }
                }
            }
            MemoryPool.Release(this);
    }
    private FieldInfo[] GetFieldInfo()
        {
            return _fieldInfos ??=
                GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }
}
