using cfg;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SkillComponent : Component
{
    public CharacterComponent character;
    public WeaponComponent weapon;
    public ControllerComponent controller;
    public AnimatorComponent anime;

    public int skillId;
    public Dictionary<SkillType, Skill> data;

    public List<float> nowCdtime = new List<float>();

    public bool[,] skillAnim = new bool[5,3];

    public override void Init()
    {
        character = (CharacterComponent)entity.GetComponent("CharacterComponent");
        weapon = (WeaponComponent)entity.GetComponent("WeaponComponent");
        controller = (ControllerComponent)entity.GetComponent("ControllerComponent");
        anime = (AnimatorComponent)entity.GetComponent("AnimatorComponent");
    }

    public override void DataInit()
    {
        skillId = character.configs.SkillID;

        SkillData config = TableDataManager.Instance.tables.SkillDefine.Get(skillId);
        if (config != null)
        {
            int cnt = config.SkillType.Count;

            data = new Dictionary<SkillType, Skill>(cnt);

            Skill baseData;
            for (int i = 0; i < cnt; i++)
            {
                baseData = new Skill(i,
                                                config.SkillType[i],
                                                config.CdTime[i],
                                                config.EffectType[i],
                                                config.TypeDefine[i],
                                                config.PrefabId[i],
                                                config.EffectId[i],
                                                config.AnimationId[i],
                                                config.Lock[i]);

                data[baseData.Type] = baseData;

                nowCdtime.Add(baseData.cd);
                int idx = (int)baseData.Type;
                skillAnim[idx,0] = config.Windup[i] == 1;
                skillAnim[idx,1] = config.Atking[i] == 1;
                skillAnim[idx,2] = config.Winddown[i] == 1;

            }

        }
    }

    public void UseSkill(SkillType skillType)
    {
        Skill baseData = data[skillType];

        if (baseData == null) { return; }

        if(!CheckCanUseSkill(skillType))return;

        baseData.UseSkill(entity);

        SetSkillCD(skillType);
    }

    public override void Update()
    {
        if (controller != null) CheckSkill();

        for (int i = 0;i < nowCdtime.Count; i++)
        {
            nowCdtime[i] += Time.deltaTime;
        }
    }

    public bool CheckCanUseSkill(SkillType skillType)
    {
        Skill skillBaseData = data[skillType];
        if (skillBaseData.isLock) return false;
        if (skillBaseData.cd <= nowCdtime[skillBaseData.idx]) return true;
        return false;
    }
    public void SetSkillCD(SkillType skillType)
    {
        Skill skillBaseData = data[skillType];
        nowCdtime[skillBaseData.idx] = 0;
    }
    public void CheckSkill()
    {
        if (controller.isRightSkill)
        {
            UseSkill(SkillType.RIGHTATK);
            return;
        }
        if (controller.isQSkill)
        {
            UseSkill(SkillType.QSKILL);
            return;
        }
        if (controller.isESkill)
        {
            UseSkill(SkillType.ESKILL);
            return;
        }
        if (controller.isTSkill)
        {
            UseSkill(SkillType.TSKILL);
            return;
        }
        if (controller.isHold || controller.isFire)
        {
            if (PlayAnimation(SkillType.NORMAL))
            {
                anime.isAtk = true;
                anime.skillidx = 0;
                anime.SetSkill(() =>
                {
                    UseSkill(SkillType.NORMAL);
                });
            }
            else
            {
                UseSkill(SkillType.NORMAL);
            }
        }
    }

    public bool PlayAnimation(SkillType type)
    {
        int idx = (int)type;
        if (skillAnim[idx,0] == true || skillAnim[idx,1] == true || skillAnim[idx,2] == true) return true;
        return false;
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<SkillComponent>(this);
    }
}
