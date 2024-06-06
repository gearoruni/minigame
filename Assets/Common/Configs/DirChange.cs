
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg
{
public sealed partial class DirChange : Luban.BeanBase
{
    public DirChange(JSONNode _buf) 
    {
        { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
        { var __json0 = _buf["speed"]; if(!__json0.IsArray) { throw new SerializationException(); } Speed = new System.Collections.Generic.List<float>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { float __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  Speed.Add(__v0); }   }
        { var __json0 = _buf["time"]; if(!__json0.IsArray) { throw new SerializationException(); } Time = new System.Collections.Generic.List<float>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { float __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  Time.Add(__v0); }   }
        { var __json0 = _buf["target"]; if(!__json0.IsArray) { throw new SerializationException(); } Target = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  Target.Add(__v0); }   }
        { var __json0 = _buf["dir"]; if(!__json0.IsArray) { throw new SerializationException(); } Dir = new System.Collections.Generic.List<float>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { float __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  Dir.Add(__v0); }   }
    }

    public static DirChange DeserializeDirChange(JSONNode _buf)
    {
        return new DirChange(_buf);
    }

    /// <summary>
    /// id
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// 速度变化值
    /// </summary>
    public readonly System.Collections.Generic.List<float> Speed;
    public readonly System.Collections.Generic.List<float> Time;
    /// <summary>
    /// 1 = 自身<br/>2 = 对方
    /// </summary>
    public readonly System.Collections.Generic.List<int> Target;
    /// <summary>
    /// 方向
    /// </summary>
    public readonly System.Collections.Generic.List<float> Dir;
   
    public const int __ID__ = 72042109;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "speed:" + Luban.StringUtil.CollectionToString(Speed) + ","
        + "time:" + Luban.StringUtil.CollectionToString(Time) + ","
        + "target:" + Luban.StringUtil.CollectionToString(Target) + ","
        + "dir:" + Luban.StringUtil.CollectionToString(Dir) + ","
        + "}";
    }
}

}
