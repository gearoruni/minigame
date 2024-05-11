using cfg.Character;
using cfg;

using UnityEngine;
using System;
using UnityEditor;
using System.Collections.Generic;

public class WeaponComponent : Component
{
    public int weaponId;
    public Entity weapon;
    CharacterComponent character;
    TransformComponent characterTransform;

    TransformComponent weaponTransform;
    GoComponent WeaponGo;

    ControllerComponent controller;
    StateComponent state;

    //默认武器信息
    public float radis;
    public string prefabName;
    public int bulletsInClip;
    public float reloadTime;
    public float fireRate;
    public List<FireDefine> fireDefines = new List<FireDefine>();

    //实时武器信息
    public int nowBulletCnt;
    public float reloadTs;
    public bool isReloading = false;
    public override void Init()
    {
        //基础数据
        character = (CharacterComponent)entity.GetComponent("CharacterComponent");
        characterTransform = (TransformComponent)entity.GetComponent("TransformComponent");
        controller = (ControllerComponent)entity.GetComponent("ControllerComponent");
        state = (StateComponent)entity.GetComponent("StateComponent");

    }

    public override void DataInit()
    {
        weaponId = character.weaponDir[character.level];
        SetWeapon();

        //创建weapon entity
        weapon = EntityManager.Instance.CreateEntity(2,2);

        weaponTransform = (TransformComponent)weapon.GetComponent("TransformComponent");
        weaponTransform.position = characterTransform.position;
        WeaponGo = (GoComponent)weapon.GetComponent("GoComponent");
        WeaponGo.CreateGameObject(weaponId.ToString());
        weapon.parentId = entity.instanceId;
        ParentComponent parent = (ParentComponent)weapon.GetComponent("ParentComponent");
        parent.SetParent(entity);
    }

    public void SetWeapon()
    {
        WeaponDefine weaponDefine = TableDataManager.Instance.tables.WeaponDefine;
        WeaponConfigs configs = weaponDefine.DataMap[weaponId];

        radis = configs.Radis;
        prefabName = configs.Name;
        nowBulletCnt = configs.BulletsInClip;
        bulletsInClip = configs.BulletsInClip;
        reloadTime = configs.ReloadTime;
        fireRate = configs.FireRate;

        for (int i = 0; i < configs.BulletId.Count; i++)
        {
            FireDefine fireDefine = new FireDefine(configs.BulletId[i],
                                                    configs.UpLimit[i],
                                                    configs.DownLimit[i],
                                                    configs.VolleyCount[i],
                                                    configs.BulletsPerVolley[i],
                                                    configs.TimeBetweenBullets[i]);
            fireDefines.Add(fireDefine);
        }
    }

    public void SetWeaponTransform(Vector3 pos)
    {
        if (weaponTransform == null) return;
        weaponTransform.SetRotationLookAt(pos, characterTransform.position);

        Vector2 dir = pos - characterTransform.position;
        dir = dir.normalized * radis;
        weaponTransform.position.x = characterTransform.position.x;
        weaponTransform.position.y = characterTransform.position.y;
        weaponTransform.SetPostionOffset(dir.x, dir.y);
    }

    public override void Update()
    {
        if (controller != null)
        {
            SetWeaponTransform(controller.facepos);
            if (controller.isReload)
            {
                Reload();
                //状态变更
                state.state = State.RELOAD;
            }
        }

        if (isReloading)
        {
            reloadTs += Time.deltaTime;
            Debug.Log("正在换弹");
            if (reloadTs > reloadTime)
            {
                nowBulletCnt = bulletsInClip;
                reloadTs = 0f;
                isReloading = false;
                Debug.Log("换弹结束");
            }
        }
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<WeaponComponent>(this);
    }

    public void Fire()
    {
        if (isReloading || nowBulletCnt <= 0) return;
        Debug.Log("Fire!");

        foreach (FireDefine fireDefine in fireDefines)
        {

            float timeBetweenBullets = fireDefine.timeBetweenBullets;

            for (int i = 0; i < fireDefine.volleyCount; i++)
            {
                Vector3 baseDir = WeaponGo.go.transform.up;

                // 随机生成一个角度
                float randomAngle = UnityEngine.Random.Range(fireDefine.downLimit, fireDefine.upLimit);

                // 计算弹道的方向
                Vector2 direction = Quaternion.Euler(0f, 0f, randomAngle) * WeaponGo.go.transform.up;

                TimerManager.Instance.RegisterTimer(fireDefine.timeBetweenBullets, fireDefine.bulletsPerVolley, delegate () { 
                    BaseFire(fireDefine, weaponTransform.position, direction);
                }, true);
            }
        }
        nowBulletCnt--;
    }

    private void BaseFire(FireDefine fireDefine, Vector3 position, Vector2 direction)
    {
        BulletConfigs bullet = TableDataManager.Instance.tables.BulletDefine.Get(fireDefine.bulletId);

        Entity bulletEntity = EntityManager.Instance.CreateEntity(3,3);
        //设定Transform
        TransformComponent transform = (TransformComponent)bulletEntity.GetComponent("TransformComponent");
        transform.position = position;
        //绑定GO
        GoComponent go = (GoComponent)bulletEntity.GetComponent("GoComponent");
        go.CreateGameObject(fireDefine.bulletId.ToString());
        //设置Tag从属
        TagComponent tag = (TagComponent)bulletEntity.GetComponent("TagComponent");
        TagComponent basetag = (TagComponent)entity.GetComponent("TagComponent");
        tag.SetParent(basetag.tag);
        //设置移动
        MoveComponent moveComponent = (MoveComponent)bulletEntity.GetComponent("MoveComponent");
        moveComponent.input = direction;
        moveComponent.SetSpeed(bullet.Speed);
        moveComponent.needRaycaster = false;
        //设置销毁
        DestroyComponent destroyComponent = (DestroyComponent)bulletEntity.GetComponent("DestroyComponent");
        destroyComponent.SetDestroyTimer(bullet.LivingTime);
        destroyComponent.SetNeedColliderDestroy();
        //设置子弹效果
        EffectComponent effectComponent = (EffectComponent)bulletEntity.GetComponent("EffectComponent");
        HealthChangeEffect healthChangeEffect = new HealthChangeEffect();
        healthChangeEffect.SetEffectData(bullet.Demage);
        effectComponent.SetEffect(healthChangeEffect, "HealthChangeEffect", true);
    }

    public void Reload()
    {
        isReloading = true;
    }
}
