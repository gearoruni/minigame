using cfg;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillComponent : Component
{
    public CharacterComponent character;
    public WeaponComponent weapon;

    public int skillId;
    public Dictionary<SkillType, SkillBaseData> data;

    public List<float> nowCdtime = new List<float>();
    public override void Init()
    {
        character = (CharacterComponent)entity.GetComponent("CharacterComponent");
        weapon = (WeaponComponent)entity.GetComponent("WeaponComponent");

        skillId = character.configs.SkillID;

        SkillData config =  TableDataManager.Instance.tables.SkillDefine.Get(skillId);
        if (config != null)
        {
            int cnt = config.SkillType.Count;

            data = new Dictionary<SkillType, SkillBaseData>(cnt);


            SkillBaseData baseData;
            for (int i = 0; i < cnt; i++)
            {
                baseData = new SkillBaseData(   i,
                                                config.SkillType[i],
                                                config.TransmiterId[i],
                                                config.Demage[i],
                                                config.Heal[i],
                                                config.FieldType[i],
                                                config.FieldWidth[i],
                                                config.FieldHeight[i],
                                                config.VaildTime[i],
                                                config.CdTime[i]);
                if(baseData.Type == SkillType.NORMAL)
                {
                    baseData.CDTime = weapon.fireRate;
                }

                data[baseData.Type] = baseData;

                nowCdtime.Add(baseData.CDTime);
            }

        }
    }

    public void UseSkill(SkillType skillType)
    {
        SkillBaseData baseData = data[skillType];

        if (baseData == null) { return; }

        if(!CheckCanUseSkill(skillType))return;
        //普攻默认从weapon打出
        if (skillType == SkillType.NORMAL)
        {
            weapon.Fire();
        }
        //其他需要由发射机打出的技能
        else if(baseData.transmiterId!=0)
        {
            WeaponConfigs transmiter = TableDataManager.Instance.tables.WeaponDefine.Get(baseData.transmiterId);
            //创建发射器Entity
            //通知发射器Entity进行发射
        }
        //其他剩余技能 大多数是AOE
        else
        {
            //创建技能Entity
        }
        SetSkillCD(skillType);
    }

    public override void Update()
    {
        for(int i = 0;i < nowCdtime.Count; i++)
        {
            nowCdtime[i] += Time.deltaTime;
        }
    }

    public bool CheckCanUseSkill(SkillType skillType)
    {
        SkillBaseData skillBaseData = data[skillType];
        if (skillBaseData.CDTime <= nowCdtime[skillBaseData.idx]) return true;
        return false;
    }
    public void SetSkillCD(SkillType skillType)
    {
        SkillBaseData skillBaseData = data[skillType];
        nowCdtime[skillBaseData.idx] = 0;
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<SkillComponent>(this);
    }
}
