
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
public sealed partial class AnimationData : Luban.BeanBase
{
    public AnimationData(JSONNode _buf) 
    {
        { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
        { if(!_buf["animationName"].IsString) { throw new SerializationException(); }  AnimationName = _buf["animationName"]; }
    }

    public static AnimationData DeserializeAnimationData(JSONNode _buf)
    {
        return new AnimationData(_buf);
    }

    /// <summary>
    /// id
    /// </summary>
    public readonly int Id;
    public readonly string AnimationName;
   
    public const int __ID__ = -1465299666;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "animationName:" + AnimationName + ","
        + "}";
    }
}

}
