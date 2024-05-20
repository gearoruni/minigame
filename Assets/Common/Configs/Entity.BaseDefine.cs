
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;
using System.Diagnostics;

namespace cfg.Entity
{
public partial class BaseDefine
{
    private readonly System.Collections.Generic.Dictionary<int, EntityConfigs> _dataMap;
    private readonly System.Collections.Generic.List<EntityConfigs> _dataList;
    
    public BaseDefine(JSONNode _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, EntityConfigs>();
        _dataList = new System.Collections.Generic.List<EntityConfigs>();
        
        foreach(JSONNode _ele in _buf.Children)
        {
            EntityConfigs _v;
            { if(!_ele.IsObject) { throw new SerializationException(); }  _v = EntityConfigs.DeserializeEntityConfigs(_ele);  }
            _dataList.Add(_v);
            _dataMap.Add(_v.EntityId, _v);
                UnityEngine.Debug.Log(_v);
        }
    }

    public System.Collections.Generic.Dictionary<int, EntityConfigs> DataMap => _dataMap;
    public System.Collections.Generic.List<EntityConfigs> DataList => _dataList;

    public EntityConfigs GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public EntityConfigs Get(int key) => _dataMap[key];
    public EntityConfigs this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}

