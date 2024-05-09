using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    NORMAL = 0,
    RIGHTATK = 1,
    QSKILL = 2,
    ESKILL = 3,
    TSKILL = 4,//´ó
}

public enum FieldType
{
    NONE = 0,
    RECT = 1,
    CIRCLE = 2,
}

public class SkillBaseData
{
    public int idx;
    public SkillType Type;
    public int transmiterId;
    public int demage;
    public int heal;
    public FieldType field;
    
    public int width;
    public int height;

    public int radiu;
    public int tang;

    public float effectTime;
    public float CDTime;

    public SkillBaseData(int idx,int type, int transmiterId, int demage, int heal, int field, int width, int height, float effectTime,float cdTime)
    {
        this.idx = idx;
        Type = (SkillType)type;
        this.transmiterId = transmiterId;
        this.demage = demage;
        this.heal = heal;
        this.field = (FieldType)field;
        if(this.field == FieldType.RECT)
        {
            this.width = width;
            this.height = height;
        }
        else
        {
            this.radiu = width;
            this.tang = height;
        }
        this.effectTime = effectTime;
        this.CDTime = cdTime;   
    }
}
