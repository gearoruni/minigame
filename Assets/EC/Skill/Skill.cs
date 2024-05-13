using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
    //public int typeDefine;

    public WeaponConfigs weaponConfigs;

    public string prefabName;
    public int effectId;
    public int animationId;

    public Skill(int idx, int type, float cd, int fieldType, int typeDefine, int prefabName, int effectId,int animationId)
    {
        this.idx = idx;
        this.Type = (SkillType)type;
        this.cd = cd;
        this.fieldType = (FieldType)fieldType;

        if(this.fieldType == FieldType.NONE)
        {
            weaponConfigs = TableDataManager.Instance.tables.WeaponDefine.Get(typeDefine);
        }
        
        this.prefabName = prefabName.ToString();
        this.effectId = effectId;
        this.animationId = animationId;
    }
    public void UseSkill(Entity entity)
    {
        if(this.fieldType == FieldType.NONE) {
            for(int i = 0; i < weaponConfigs.UpLimit.Count; i++)
            {
                int upv = weaponConfigs.UpLimit[i];
                int downv = weaponConfigs.DownLimit[i];
                int volleyCnt = weaponConfigs.VolleyCount[i];
                int bulletsPerVolley = weaponConfigs.BulletsPerVolley[i];
                float timeBetweenBullets = weaponConfigs.TimeBetweenBullets[i];

                WeaponComponent weaponComponent = (WeaponComponent)entity.GetComponent("WeaponComponent");
                if(weaponComponent != null)
                {
                    Vector2 baseDir = weaponComponent.GetWeaponFace();
                    float randomAngle = UnityEngine.Random.Range(downv, upv);
                    Vector2 direction = Quaternion.Euler(0f, 0f, randomAngle) * baseDir;
                    TimerManager.Instance.RegisterTimer(timeBetweenBullets, bulletsPerVolley, delegate () {
                        BaseFire(entity, weaponComponent.GetWeaponPos(), direction);
                    }, true);
                }

            }
        }
        else
        {

        }
    }
    private void BaseFire(Entity entity,Vector2 position, Vector2 direction)
    {
        Entity bulletEntity = EntityManager.Instance.ParentCreateEntity(entity, 3, 3, false);
        //设定Transform
        TransformComponent transform = (TransformComponent)bulletEntity.GetComponent("TransformComponent");
        transform.position = position;
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
        ////设置销毁
        DestroyComponent destroyComponent = (DestroyComponent)bulletEntity.GetComponent("DestroyComponent");
        destroyComponent.SetNeedColliderDestroy();
        ////设置子弹效果
        EffectComponent effectComponent = (EffectComponent)bulletEntity.GetComponent("EffectComponent");
        effectComponent.DataInit(effectId);

    }
}
