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
        switch(weaponConfigs.SpecialSkill[0])
        {
            case 1:
                tanfan(entity);
                animator?.Play("attack1");
                timer = TimerManager.Instance.RegisterTimer(2,2,()=>{
                    SkillComponent cmp = (SkillComponent)entity.GetComponent("SkillComponent");
                    if(cmp != null)
                        cmp.continueCallBack = null;
                    // TimerManager.Instance.RemoveTimer(timer);
                    //TODO:检测销毁
                    weaponComponent = (WeaponComponent)entity.GetComponent("WeaponComponent");
                    if(weaponComponent == null)return;
                    animator = weaponComponent.weapon.go.GetComponentInChildren<Animator>();
                    animator?.Play("idle");
                    });
            break;
            case 2:
            Debug.Log("chongci");
                Chongci(entity);
                 timer = TimerManager.Instance.RegisterTimer(0.35f,1,()=>{
                    SkillComponent cmp = (SkillComponent)entity.GetComponent("SkillComponent");
                     if (cmp != null)
                        cmp.continueCallBack = null;
                    weaponComponent = (WeaponComponent)entity.GetComponent("WeaponComponent");
                    if(weaponComponent == null)return;
                    animator = weaponComponent.weapon.go.GetComponentInChildren<Animator>();
                    animator?.Play("idle");
                    //TimerManager.Instance.RemoveTimer(timer);
                    });
            break;
            case 3:
                animator?.Play("attack1");
                Debug.Log(animator);
                timer = TimerManager.Instance.RegisterTimer(1,1,()=>{
                    weaponComponent = (WeaponComponent)entity.GetComponent("WeaponComponent");
                    if(weaponComponent == null)return;
                    animator = weaponComponent.weapon.go.GetComponentInChildren<Animator>();
                    animator?.Play("idle");
                    xulishuiqiu(entity);
                });
            break;
            case 4:
                // animator?.Play("attack2");
                zhaohuan(entity);
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

        TagComponent tagComponent = (TagComponent)bulletEntity.GetComponent("TagComponent");
        tagComponent.SetTag(entity.Tag);

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
            if(e.entityId == 3 && e.Tag != entity.Tag)
            {
                Vector2 v = e.go.transform.position - entity.go.transform.position;
                if(v.magnitude <= 7 && math.abs(Vector2.Angle(v,weaponComponent.GetWeaponFace())) < 30f)
                {
                    var tagcmp = (TagComponent)e.GetComponent("TagComponent");
                    if(tagcmp != null)
                    {
                        tagcmp.tag = entity.Tag;
                    }
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
        List<Entity> hasDamage = new List<Entity>();
        cmp.continueCallBack = ()=>{
            foreach(var entitys in EntityManager.Instance.entities.Values)
            {
                if(entitys == null)continue;
                if(hasDamage.Contains(entitys))continue;
                if((entitys.Tag == Tag.Player || entitys.Tag == Tag.Enemy) && entitys != entity)
                {
                    float dis =  Vector3.Distance(entity.go.transform.position,entitys.go.transform.position);
                if(dis <= 1f)
                {
                    var cmp = (CharacterDataComponent)entitys.GetComponent("CharacterDataComponent");
                    var enemyMoveCmp = (MoveComponent)entitys.GetComponent("MoveComponent");
                    if(cmp ==null || enemyMoveCmp == null)continue;
                    cmp.ChangeHp(100);
                    hasDamage.Add(entitys);
                    enemyMoveCmp.SetForceMove(0.35f, 20f, (entitys.go.transform.position - entity.go.transform.position).normalized);
                    
                }
                }
                
            }
        };
    }

    public void xulishuiqiu(Entity entity)
    {
        Vector2 direction =  weaponComponent.GetWeaponFace();
        BaseFire(entity, weaponComponent.GetWeaponTopPos(), direction);
    }

    public void zhaohuan(Entity entity)
    {
        Vector3 pos = entity.go.transform.position;
        List<Vector3> dirs = new List<Vector3>(){Vector3.up, Vector3.down, Vector3.left,Vector3.right};
        int times = 0;
        Entity entity1;
        TransformComponent cmp;
        CollisionComponent collCmp;
        AnimatorComponent animator;
        Debug.Log("召唤");
        for(int i = 0;i<4;i++)
        {
            
            if(PhysicsRay.GetWall(pos,dirs[i],out var result))
            {
                
                entity1 = EntityManager.Instance.CreateEntity(4, 150, true);
                entity1 = EntityManager.Instance.CreateEntity(4, 150);
                // EntityManager.Instance.AwakeZhaohuan(entity1);
                
                var gocmp = (GoComponent)entity1.GetComponent("GoComponent");
                if(gocmp !=null && gocmp.go !=null)

                {
                    gocmp.go.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                // cmp = (TransformComponent)entity1.GetComponent("TransformComponent");
                // collCmp = (CollisionComponent)entity1.GetComponent("CollisionComponent");
                // animator = (AnimatorComponent)entity1.GetComponent("AnimatorComponent");
                // animator?.PlayerAnime("birth");
                // // collCmp?.listener?.gameObject?.SetActive(false);
                // if(cmp!=null)
                // {
                //         TimerManager.Instance.RegisterTimer(1,0,()=>{
                //         collCmp = (CollisionComponent)entity1.GetComponent("CollisionComponent");
                //         collCmp.listener.gameObject.SetActive(true);
                //         EntityManager.Instance.AwakeZhaohuan(entity1);
                //     });
                //     cmp.SetPostion(result.x,result.y);
                //     times++;
                //     if(times >= 3)break;
                // }
            }
        }
    }
    #endregion
}
