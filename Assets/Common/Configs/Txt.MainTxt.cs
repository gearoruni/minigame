
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg.Txt
{
public partial class MainTxt
{
    private readonly System.Collections.Generic.Dictionary<int, DialogueConfigs> _dataMap;
    private readonly System.Collections.Generic.List<DialogueConfigs> _dataList;
    
    public MainTxt(JSONNode _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, DialogueConfigs>();
        _dataList = new System.Collections.Generic.List<DialogueConfigs>();
        
        foreach(JSONNode _ele in _buf.Children)
        {
            DialogueConfigs _v;
            { if(!_ele.IsObject) { throw new SerializationException(); }  _v = DialogueConfigs.DeserializeDialogueConfigs(_ele);  }
            _dataList.Add(_v);
            _dataMap.Add(_v.文本id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, DialogueConfigs> DataMap => _dataMap;
    public System.Collections.Generic.List<DialogueConfigs> DataList => _dataList;

    public DialogueConfigs GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public DialogueConfigs Get(int key) => _dataMap[key];
    public DialogueConfigs this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}
