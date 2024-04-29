
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
public sealed partial class BulletConfigs : Luban.BeanBase
{
    public BulletConfigs(JSONNode _buf) 
    {
        { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
        { if(!_buf["name"].IsString) { throw new SerializationException(); }  Name = _buf["name"]; }
        { if(!_buf["speed"].IsNumber) { throw new SerializationException(); }  Speed = _buf["speed"]; }
        { if(!_buf["LivingTime"].IsNumber) { throw new SerializationException(); }  LivingTime = _buf["LivingTime"]; }
        { if(!_buf["demage"].IsNumber) { throw new SerializationException(); }  Demage = _buf["demage"]; }
    }

    public static BulletConfigs DeserializeBulletConfigs(JSONNode _buf)
    {
        return new BulletConfigs(_buf);
    }

    public readonly int Id;
    public readonly string Name;
    public readonly float Speed;
    public readonly float LivingTime;
    public readonly int Demage;
   
    public const int __ID__ = 241238511;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "name:" + Name + ","
        + "speed:" + Speed + ","
        + "LivingTime:" + LivingTime + ","
        + "demage:" + Demage + ","
        + "}";
    }
}

}
