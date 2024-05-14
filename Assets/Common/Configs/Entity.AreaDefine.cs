
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg.Entity
{
public partial class AreaDefine
{
    private readonly System.Collections.Generic.Dictionary<int, AreaConfigs> _dataMap;
    private readonly System.Collections.Generic.List<AreaConfigs> _dataList;
    
    public AreaDefine(JSONNode _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, AreaConfigs>();
        _dataList = new System.Collections.Generic.List<AreaConfigs>();
        
        foreach(JSONNode _ele in _buf.Children)
        {
            AreaConfigs _v;
            { if(!_ele.IsObject) { throw new SerializationException(); }  _v = AreaConfigs.DeserializeAreaConfigs(_ele);  }
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, AreaConfigs> DataMap => _dataMap;
    public System.Collections.Generic.List<AreaConfigs> DataList => _dataList;

    public AreaConfigs GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public AreaConfigs Get(int key) => _dataMap[key];
    public AreaConfigs this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}

