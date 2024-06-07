using cfg;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum SkillType
{
    NORMAL = 0,
    RIGHTATK = 1,
    QSKILL = 2,
    ESKILL = 3,
    TSKILL = 4,//大
}

public enum FieldType
{
    t = 0,
    NONE = 1,
    RECT = 2,
}

public class Skill
{
    //初始数据
    public int idx;
    public SkillType Type;
    public float cd;
    public FieldType fieldType;

    public WeaponConfigs weaponConfigs;
    public AreaConfigs areaConfigs;

    public string prefabName;
    public int effectId;
    public int animationId;
    public bool isLock;
    public Timer timer;
    WeaponComponent weaponComponent;

    public Skill(int idx, int type, float cd, int fieldType, int typeDefine, int prefabName, int effectId,int animationId, bool isLock)
    {
        this.isLock = isLock;
        this.idx = idx;
        this.Type = (SkillType)type;
        this.cd = cd;
        this.fieldType = (FieldType)fieldType;
        if(fieldType!=0)
            weaponConfigs = TableDataManager.Instance.tables.WeaponDefine.Get(typeDefine);
        
        this.prefabName = prefabName.ToString();
        this.effectId = effectId;
        this.animationId = animationId;
    }
    public void UseSkill(Entity entity)
    {
        weaponComponent = (WeaponComponent)entity.GetComponent("WeaponComponent");
        Animator animator;
        NoneFieldSkill(entity);
        animator = weaponComponent.weapon.go.GetComponentInChildren<Animator>();
        Debug.Log(animator);
        switch(weaponConfigs.SpecialSkill[0])
        {
            case 1:
                tanfan(entity);
                animator?.Play("attack1");
                timer = TimerManager.Instance.RegisterTimer(2,2,()=>{
                    SkillComponent cmp = (SkillComponent)entity.GetComponent("SkillComponent");
                    cmp.continueCallBack = null;
                    TimerManager.Instance.RemoveTimer(timer);
                    animator?.Play("idle");
                    });
            break;
            case 2:
                Chongci(entity);
                 timer = TimerManager.Instance.RegisterTimer(0.35f,1,()=>{
                    SkillComponent cmp = (SkillComponent)entity.GetComponent("SkillComponent");
                    cmp.continueCallBack = null;
                    TimerManager.Instance.RemoveTimer(timer);
                    });
            break;
        }
    }

    #region 标准技能
    public void NoneFieldSkill(Entity entity)
    {
        if (weaponConfigs == null || weaponConfigs.UpLimit == null) return;
        for (int i = 0; i < weaponConfigs.UpLimit.Count; i++)
        {
            int upv = weaponConfigs.UpLimit[i];
            int downv = weaponConfigs.DownLimit[i];
            int volleyCnt = weaponConfigs.VolleyCount[i];
            int bulletsPerVolley = weaponConfigs.BulletsPerVolley[i];
            float timeBetweenBullets = weaponConfigs.TimeBetweenBullets[i];
            for (int j = 0; j < volleyCnt; j++)
            {
                if (weaponComponent != null)
                {
                    Vector2 baseDir = weaponComponent.GetWeaponFace();
                    float randomAngle = (upv - downv) / volleyCnt * j + downv;
                    Vector2 direction = Quaternion.Euler(0f, 0f, randomAngle) * baseDir;
                    BaseFire(entity, weaponComponent.GetWeaponTopPos(), direction);
                    // TimerManager.Instance.RegisterTimer(timeBetweenBullets, bulletsPerVolley, delegate () {
                        
                    // }, true);
                }
            }
        }
    }

    private void BaseFire(Entity entity,Vector2 position, Vector2 direction)
    {
        Entity bulletEntity;
        if (fieldType == FieldType.NONE)
        {
            bulletEntity = EntityManager.Instance.ParentCreateEntity(entity, 3, 3, false);
        }
        else if (fieldType == FieldType.RECT)
        {
            bulletEntity = EntityManager.Instance.ParentCreateEntity(entity, 7, 3, false);
        }
        else
        {
            return;
        }

        //设定Transform
        TransformComponent transform = (TransformComponent)bulletEntity.GetComponent("TransformComponent");
        transform.position = position;
        transform.rotation = weaponComponent.weaponTransform.rotation;
        transform.faceDir = weaponComponent.GetWeaponFace();
        //绑定GO
        GoComponent go = (GoComponent)bulletEntity.GetComponent("GoComponent");
        go.CreateGameObject(prefabName);
        //设置移动
        MoveComponent moveComponent = (MoveComponent)bulletEntity.GetComponent("MoveComponent");
        moveComponent.input = direction;
        moveComponent.needRaycaster = false;
        //动画
        BulletComponent bulletComponent = (BulletComponent)bulletEntity.GetComponent("BulletComponent");
        bulletComponent.DataInit(animationId);
        ////设置子弹效果
        EffectComponent effectComponent = (EffectComponent)bulletEntity.GetComponent("EffectComponent");
        effectComponent.DataInit(effectId);
    }
    #endregion

    #region 特殊技能
    public void tanfan(Entity entity)
    {
        var cmp = (SkillComponent)entity.GetComponent("SkillComponent");
        if(cmp == null)return;
        Vector2 baseDir = weaponComponent.GetWeaponFace();
        int upv = weaponConfigs.UpLimit[0];
        int downv = weaponConfigs.DownLimit[0];
        cmp.continueCallBack = () => {
            
        foreach(var e in EntityManager.Instance.entities.Values)
        {
            if(e == null)continue;
            if(e.entityId == 3 && e.Tag != Tag.Player)
            {
                Vector2 v = e.go.transform.position - entity.go.transform.position;
                if(v.magnitude <= 10 && math.abs(Vector2.Angle(v,weaponComponent.GetWeaponFace())) < 30f)
                {
                    e.Tag = Tag.Player;
                    MoveComponent mcmp=  (MoveComponent)e.GetComponent("MoveComponent");
                    mcmp.input = weaponComponent.GetWeaponFace();
                }
            }
        }
        };
    }
    public void Chongci(Entity entity)
    {
        var cmp = (SkillComponent)entity.GetComponent("SkillComponent");
        var movecmp = (MoveComponent)entity.GetComponent("MoveComponent");
        movecmp.SetForceMove(0.35f, 20f, weaponComponent.GetWeaponFace());
        if(cmp == null)return;
        cmp.continueCallBack = ()=>{};
    }
    #endregion
}
