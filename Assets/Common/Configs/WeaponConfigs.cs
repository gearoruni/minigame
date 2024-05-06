
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
public sealed partial class WeaponConfigs : Luban.BeanBase
{
    public WeaponConfigs(JSONNode _buf) 
    {
        { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
        { if(!_buf["name"].IsString) { throw new SerializationException(); }  Name = _buf["name"]; }
        { if(!_buf["bulletsInClip"].IsNumber) { throw new SerializationException(); }  BulletsInClip = _buf["bulletsInClip"]; }
        { if(!_buf["reloadTime"].IsNumber) { throw new SerializationException(); }  ReloadTime = _buf["reloadTime"]; }
        { if(!_buf["fireRate"].IsNumber) { throw new SerializationException(); }  FireRate = _buf["fireRate"]; }
        { var __json0 = _buf["bulletId"]; if(!__json0.IsArray) { throw new SerializationException(); } BulletId = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  BulletId.Add(__v0); }   }
        { var __json0 = _buf["upLimit"]; if(!__json0.IsArray) { throw new SerializationException(); } UpLimit = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  UpLimit.Add(__v0); }   }
        { var __json0 = _buf["downLimit"]; if(!__json0.IsArray) { throw new SerializationException(); } DownLimit = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  DownLimit.Add(__v0); }   }
        { var __json0 = _buf["volleyCount"]; if(!__json0.IsArray) { throw new SerializationException(); } VolleyCount = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  VolleyCount.Add(__v0); }   }
        { var __json0 = _buf["bulletsPerVolley"]; if(!__json0.IsArray) { throw new SerializationException(); } BulletsPerVolley = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  BulletsPerVolley.Add(__v0); }   }
        { var __json0 = _buf["timeBetweenBullets"]; if(!__json0.IsArray) { throw new SerializationException(); } TimeBetweenBullets = new System.Collections.Generic.List<float>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { float __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  TimeBetweenBullets.Add(__v0); }   }
    }

    public static WeaponConfigs DeserializeWeaponConfigs(JSONNode _buf)
    {
        return new WeaponConfigs(_buf);
    }

    /// <summary>
    /// id
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// name
    /// </summary>
    public readonly string Name;
    /// <summary>
    /// 弹夹内子弹总数
    /// </summary>
    public readonly int BulletsInClip;
    /// <summary>
    /// 换弹时间
    /// </summary>
    public readonly float ReloadTime;
    /// <summary>
    /// 发射间隔
    /// </summary>
    public readonly float FireRate;
    /// <summary>
    /// 子弹id
    /// </summary>
    public readonly System.Collections.Generic.List<int> BulletId;
    /// <summary>
    /// 范围上限
    /// </summary>
    public readonly System.Collections.Generic.List<int> UpLimit;
    /// <summary>
    /// 范围下限
    /// </summary>
    public readonly System.Collections.Generic.List<int> DownLimit;
    /// <summary>
    /// 范围内弹道数
    /// </summary>
    public readonly System.Collections.Generic.List<int> VolleyCount;
    /// <summary>
    /// 同弹道子弹数
    /// </summary>
    public readonly System.Collections.Generic.List<int> BulletsPerVolley;
    /// <summary>
    /// 连发间发射间隔
    /// </summary>
    public readonly System.Collections.Generic.List<float> TimeBetweenBullets;
   
    public const int __ID__ = -356674667;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
        
        
        
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "name:" + Name + ","
        + "bulletsInClip:" + BulletsInClip + ","
        + "reloadTime:" + ReloadTime + ","
        + "fireRate:" + FireRate + ","
        + "bulletId:" + Luban.StringUtil.CollectionToString(BulletId) + ","
        + "upLimit:" + Luban.StringUtil.CollectionToString(UpLimit) + ","
        + "downLimit:" + Luban.StringUtil.CollectionToString(DownLimit) + ","
        + "volleyCount:" + Luban.StringUtil.CollectionToString(VolleyCount) + ","
        + "bulletsPerVolley:" + Luban.StringUtil.CollectionToString(BulletsPerVolley) + ","
        + "timeBetweenBullets:" + Luban.StringUtil.CollectionToString(TimeBetweenBullets) + ","
        + "}";
    }
}

}